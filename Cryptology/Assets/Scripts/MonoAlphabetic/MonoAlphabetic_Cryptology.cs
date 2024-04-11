using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(MonoAlphabetic_Encryption), typeof(MonoAlphabetic_Decryption))]
public class MonoAlphabetic_Cryptology : Cryptology_Base
{
    #region Open_Private_Fields
    [Tooltip("���� �õ� ��")]
    [SerializeField]
    private int monoAlphabeticSeed
    {
        get
        {
            if (!PlayerPrefs.HasKey("monoAlphabeticSeed"))
                return 0;
            return PlayerPrefs.GetInt("monoAlphabeticSeed");
        }
        set
        {
            // �� ����
            PlayerPrefs.SetInt("monoAlphabeticSeed", value);
            // ġȯǥ ����
            SetEncryptionWord();
            // �õ尪 �ؽ�Ʈ ����
            seedInputField.text = monoAlphabeticSeed.ToString();
        }
    }

    [Space(10), Header("MonoAlphabetic Fields")]
    [Tooltip("���� �õ� �� ��ǲ �ʵ�")]
    [SerializeField]
    private TMP_InputField seedInputField;

    [Tooltip("���� �õ� �� ���� ���� ��ư")]
    [SerializeField]
    private Button seedRandomBtn;
    #endregion;

    #region Private_Fields
    private MonoAlphabetic_Encryption encryption;
    private MonoAlphabetic_Decryption decryption;
    #endregion

    #region MonoBehaviour_Callbacks
    private void OnEnable()
    {
        // ġȯǥ ����
        SetEncryptionWord();
    }
    #endregion

    #region Custom_Methods
    public override void Init()
    {
        // ��ȣȭ ��� ȹ��
        GetCryptologyWay();
        // UI ����
        UISetting();
    }

    private void GetCryptologyWay()
    {
        encryption = GetComponent<MonoAlphabetic_Encryption>();
        decryption = GetComponent<MonoAlphabetic_Decryption>();
    }

    /// <summary>
    /// UI ����
    /// </summary>
    public void UISetting()
    {
        // ���� ���ĺ� ����
        for (int i = 0; i < 26; i++)
        {
            original.Add((char)('a' + i));
        }

        // ���� �Է� �Ϸ� �̺�Ʈ �߰�
        inputField.onEndEdit.AddListener(
            (text) =>
            {
                // ��ȣȭ
                if (isEncryption)
                {
                    //Encryption(text);
                    outputField.text = encryption.Encryption(text, encryptionWord);
                }
                // �ص�
                else
                {
                    //Decryption(text);
                    outputField.text = decryption.Decryption(text, encryptionWord);
                }
            });

        // ��ȣȭ ���� �ص����� Ȯ�� ��� �̺�Ʈ �߰�
        toggle.onValueChanged.AddListener(
            (value) =>
            {
                isEncryption = value;
                if (value)
                {
                    toggle.GetComponentInChildren<TextMeshProUGUI>().text = "Encryption";
                    // ������ �Է��� ���� �ִٸ� ��ȣȭ
                    if (!string.IsNullOrEmpty(inputField.text))
                    {
                        outputField.text = encryption.Encryption(inputField.text, encryptionWord);
                    }
                }
                else
                {
                    toggle.GetComponentInChildren<TextMeshProUGUI>().text = "Decryption";
                    // ������ �Է��� ���� �ִٸ� ��ȣȭ
                    if (!string.IsNullOrEmpty(inputField.text))
                    {
                        outputField.text = decryption.Decryption(inputField.text, encryptionWord);
                    }
                }
            });

        // ���� �õ� �� ������ ������ ����
        Random.InitState(monoAlphabeticSeed);

        // ��ǲ �ʵ� �Է� �Ϸ� �̺�Ʈ �߰�
        seedInputField.onEndEdit.AddListener(
            (text) =>
            {
                monoAlphabeticSeed = Mathf.Clamp(int.Parse(text), int.MinValue, int.MaxValue);
            });

        // �õ� �� ���� ��ư �̺�Ʈ �߰�
        seedRandomBtn.onClick.AddListener(
            () =>
            {
                monoAlphabeticSeed = Random.Range(int.MinValue, int.MaxValue);
            });
    }

    /// <summary>
    /// UI ġȯǥ ����
    /// </summary>
    private void SetEncryptionWord()
    {
        // ��ųʸ� �ʱ�ȭ
        encryptionWord.Clear();
        for (int i = 0; i < 26;)
        {
            // �ƽ�Ű �ڵ� ���� �̿��ؼ� ������ ���ĺ� ȹ��
            // Random�� �õ尪�� �����Ͽ��� ������ ���� ����ؼ� ���ð�
            int code = Random.Range(97, 123);
            // �߰��� �����ϸ�
            if (encryptionWord.TryAdd((char)('a' + i), (char)(code)))
            {
                // ġȯǥ�� ���� ����
                wordList[i].Word = (Word)code;
                // i�� ����
                i++;
            }
        }

        // �Է°� �ִ��� Ȯ��
        if (!string.IsNullOrEmpty(inputField.text))
        {
            // �Է°� ����
            if (isEncryption)
            {
                outputField.text = encryption.Encryption(inputField.text, encryptionWord);
            }
            else
            {
                outputField.text = decryption.Decryption(inputField.text, encryptionWord);
            }
        }
    }
    #endregion

}

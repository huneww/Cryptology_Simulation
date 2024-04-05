using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class MonoAlphabetic_Encryption : Encryption_Base
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

    #region MonoBehaviour_Callbacks
    private void Start()
    {
        // UI ����
        UISetting();
        // BaseŬ���� UI �ʱ�ȭ �޼��� ȣ��
        base.UISetting();
    }

    private void OnEnable()
    {
        // ġȯǥ ����
        SetEncryptionWord();
    }
    #endregion

    #region Custom_Methods
    /// <summary>
    /// UI ����
    /// </summary>
    public override void UISetting()
    {
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
    override public void SetEncryptionWord()
    {
        // ��ųʸ� �ʱ�ȭ
        encryption.Clear();
        for (int i = 0; i < 26;)
        {
            // �ƽ�Ű �ڵ� ���� �̿��ؼ� ������ ���ĺ� ȹ��
            // Random�� �õ尪�� �����Ͽ��� ������ ���� ����ؼ� ���ð�
            int code = Random.Range(97, 123);
            // �߰��� �����ϸ�
            if (encryption.TryAdd((char)('a' + i), (char)(code)))
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
                Encryption(inputField.text);
            }
            else
            {
                Decryption(inputField.text);
            }
        }
    }
    #endregion

}

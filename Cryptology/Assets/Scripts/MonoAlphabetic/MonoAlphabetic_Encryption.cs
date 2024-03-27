using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class MonoAlphabetic_Encryption : Encryption_Base
{
    #region Open_Private_Fields
    [Space(10), Header("MonoAlphabetic Fields")]
    [Tooltip("���� �õ� ��")]
    [SerializeField]
    private int seed = 0;

    private int Seed
    {
        get
        {
            return seed;
        }
        set
        {
            int changeValue = Mathf.Clamp(value, int.MinValue, int.MaxValue);
            SetEncryptionWord();
            if (!string.IsNullOrEmpty(inputField.text))
            {
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
    }

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
        UISetting();
    }

    private void OnEnable()
    {
        // ġȯǥ ����
        SetEncryptionWord();
    }

    private void OnDisable()
    {
        // ġȯǥ ����
        SetEncryptionWord();
    }
    #endregion

    #region Custom_Methods
    /// <summary>
    /// UI ����
    /// </summary>
    private void UISetting()
    {
        // ���� �õ� �� ������ ������ ����
        Random.InitState(seed);

        // ��ǲ �ʵ� �Է� �Ϸ� �̺�Ʈ �߰�
        seedInputField.onEndEdit.AddListener(
            (text) =>
            {
                Seed = Mathf.Clamp(int.Parse(text), int.MinValue, int.MaxValue);
            });

        // �õ� �� ���� ��ư �̺�Ʈ �߰�
        seedRandomBtn.onClick.AddListener(
            () =>
            {
                Seed = Random.Range(int.MinValue, int.MaxValue);
                seedInputField.text = Seed.ToString();
            });
    }

    /// <summary>
    /// UI ġȯǥ ����
    /// </summary>
    private void SetEncryptionWord()
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
    }
    #endregion

}

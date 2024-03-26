using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;
using System.Linq;

public class Caesare_Encryption : MonoBehaviour
{
    #region Open_Private_Field
    [SerializeField]
    private int caesareKeyValue
    {
        get
        {
            if (!PlayerPrefs.HasKey("caesareKeyValue"))
                return 1;
            return PlayerPrefs.GetInt("caesareKeyValue");
        }
        set
        {
            int changeValue = Mathf.Clamp(value, 1, 25);
            PlayerPrefs.SetInt("caesareKeyValue", changeValue);
            SetEncryptionWord();
            if (!string.IsNullOrEmpty(inputField.text))
            {
                if (isEncryption)
                {
                    Encryption(inputField.text);
                }
                else
                {
                    DeCryption(inputField.text);
                }
            }
        }
    }

    [Tooltip("Ű �� �Է� �ʵ�")]
    [SerializeField]
    private TMP_InputField keyValueInputField;

    [Tooltip("Ű �� ���� ���� ��ư")]
    [SerializeField]
    private Button keyValueRandomBtn;

    [Tooltip("ġȯ�� ���ĺ� ǥ��")]
    [SerializeField]
    private List<WordNode> wordList = new List<WordNode>();

    [Tooltip("��ȯ�� ���ڿ� �Է� �ʵ�")]
    [SerializeField]
    private TMP_InputField inputField;

    [Tooltip("��ȯ�� ���ڿ� ��� �ʵ�")]
    [SerializeField]
    private TMP_InputField outputField;

    [Tooltip("���� ���� ��ư")]
    [SerializeField]
    private Button explainOpenBtn;

    [Tooltip("���� �ݴ� ��ư")]
    [SerializeField]
    private Button explainCloseBtn;

    [Tooltip("����")]
    [SerializeField]
    private GameObject explain;

    [Tooltip("����Ī ���")]
    [SerializeField]
    private Toggle toggle;
    #endregion

    #region Private_Field
    // ���ĺ� ����
    private List<char> original = new List<char>();
    // ġȯ�� ���ĺ�
    private Dictionary<char, char> encryption = new Dictionary<char, char>();
    // ��ȣȭ���� Ȯ�� ����
    private bool isEncryption = true;
    #endregion

#region MonoBehaviour_CallBacks
    private void Awake()
    {
        // UI �ʱ�ȭ
        UISetting();

        // ���� ���ĺ� ����
        for (int i = 0; i < 26; i++)
        {
            original.Add((char)('a' + i));
        }
    }

    private void OnEnable()
    {
        // ġȯǥ ����
        SetEncryptionWord();
    }

    private void OnDisable()
    {
        // ��ųʸ� �ʱ�ȭ
        encryption.Clear();
    }
    #endregion

    #region Custom_Methods
    /// <summary>
    /// UI �̺�Ʈ ����
    /// </summary>
    private void UISetting()
    {
        // Ű �� ��ǲ �ʵ��� �ؽ�Ʈ�� ����
        keyValueInputField.text = caesareKeyValue.ToString();

        // ��ǲ �ʵ� �Է� �Ϸ� �̺�Ʈ �߰�
        keyValueInputField.onEndEdit.AddListener(
            (text) =>
            {
                // Ű ���� 1 ~ 25������ ���� �������� ����
                caesareKeyValue = Mathf.Clamp(int.Parse(text), 1, 25);

            });

        // Ű �� ���� ��ư �̺�Ʈ �߰�
        keyValueRandomBtn.onClick.AddListener(
        () =>
        {
            // Ű ���� 1 ~ 25������ ���� �������� ����
            caesareKeyValue = Random.Range(1, 25);
            // ��ǲ �ʵ� �ؽ�Ʈ�� ����
            keyValueInputField.text = caesareKeyValue.ToString();
        });

        // ���� �Է� �Ϸ� �̺�Ʈ �߰�
        inputField.onEndEdit.AddListener(
            (text) =>
            {
                // ��ȣȭ
                if (isEncryption)
                {
                    Encryption(text);
                }
                // �ص�
                else
                {

                }
            });

        // ���� ���� ��ư �̺�Ʈ �߰�
        explainOpenBtn.onClick.AddListener(
            () =>
            {
                explain.SetActive(true);
            });

        // ���� �ݴ� ��ư �̺�Ʈ �߰�
        explainCloseBtn.onClick.AddListener(
            () =>
            {
                explain.SetActive(false);
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
                        Encryption(inputField.text);
                    }
                }
                else
                {
                    toggle.GetComponentInChildren<TextMeshProUGUI>().text = "Decryption";
                    // ������ �Է��� ���� �ִٸ� ��ȣȭ
                    if (!string.IsNullOrEmpty(inputField.text))
                    {
                        DeCryption(inputField.text);
                    }
                }
            });
    }

    /// <summary>
    /// UI ġȯǥ ����
    /// </summary>
    private void SetEncryptionWord()
    {
        encryption.Clear();
        for (int i = 0; i < 26; i++)
        {
            int addCode = ('a' - caesareKeyValue + i);
            if (addCode < 'a')
            {
                addCode = 'z' - (96 - addCode);
            }
            wordList[i].Word = (Word)addCode;
            encryption.Add(original[i], (char)addCode);
        }
    }

    /// <summary>
    /// �� ��ȣȭ
    /// </summary>
    /// <param name="originalText">��ȣȭ�� ��</param>
    private void Encryption(string originalText)
    {
        StringBuilder sb = new StringBuilder();

        foreach(char text in originalText)
        {
            // �빮��
            if (text >= 65 && text <= 90)
            {
                // �ҹ��ڷ� ����
                char lowerText = (char)(text + 32);
                // �ҹ��� �� ȹ��
                char encryptionText = encryption[lowerText];
                // ȹ���� �ҹ��� �빮�ڷ� ����
                encryptionText = (char)(encryptionText - 32);
                // sb�� �߰�
                sb.Append(encryptionText);
            }
            // �ҹ���
            else if (text >= 97 && text <= 122)
            {
                sb.Append(encryption[text]);
            }
            // �׿��� Ư�� ����
            else
            {
                sb.Append(text);
            }
        }
        outputField.text = sb.ToString();
    }

    private void DeCryption(string enCryptionText)
    {
        StringBuilder sb = new StringBuilder();

        foreach (char text in enCryptionText)
        {
            // �빮��
            if (text >= 65 && text <= 90)
            {
                // �ҹ��ڷ� ����
                char lowerText = (char)(text + 32);
                // �ҹ��ڸ� ���������� ������ �ִ� Ű�� ȹ��
                char deCryptionText = encryption.FirstOrDefault(value => value.Value == lowerText).Key;
                // ȹ���� ���� �빮�ڷ� ����
                deCryptionText = (char)(deCryptionText - 32);
                // sb�� �߰�
                sb.Append(deCryptionText);
            }
            // �ҹ���
            else if (text >= 97 && text <= 122)
            {
                // �ҹ��ڸ� ���������� ������ �ִ� Ű�� ȹ��
                char deCryptionText = encryption.FirstOrDefault(value => value.Value == text).Key;
                // sb�� �߰�
                sb.Append(deCryptionText);
            }
            // �׿��� Ư�� ����
            else
            {
                sb.Append(text);
            }
        }

        outputField.text = sb.ToString();
    }
    #endregion

}

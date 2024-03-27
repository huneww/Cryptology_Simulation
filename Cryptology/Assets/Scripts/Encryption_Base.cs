using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using System.Linq;

public class Encryption_Base : MonoBehaviour
{
    #region Open_Private_Field
    [Header("Base Fields")]
    [Tooltip("ġȯ�� ���ĺ� ǥ��")]
    [SerializeField]
    protected List<WordNode> wordList = new List<WordNode>();

    [Tooltip("��ȯ�� ���ڿ� �Է� �ʵ�")]
    [SerializeField]
    protected TMP_InputField inputField;

    [Tooltip("��ȯ�� ���ڿ� ��� �ʵ�")]
    [SerializeField]
    protected TMP_InputField outputField;

    [Tooltip("���� ���� ��ư")]
    [SerializeField]
    protected Button explainOpenBtn;

    [Tooltip("���� �ݴ� ��ư")]
    [SerializeField]
    protected Button explainCloseBtn;

    [Tooltip("����")]
    [SerializeField]
    protected GameObject explain;

    [Tooltip("����Ī ���")]
    [SerializeField]
    protected Toggle toggle;
    #endregion

    #region Private_Field
    // ���ĺ� ����
    protected List<char> original = new List<char>();
    // ġȯ�� ���ĺ�
    protected Dictionary<char, char> encryption = new Dictionary<char, char>();
    // ��ȣȭ�Ұ����� Ȯ�� ����
    protected bool isEncryption = true;
    #endregion

    #region MonoBehaviour_Callbacks
    private void Awake()
    {
        // ���� ���ĺ� ����
        for (int i = 0; i < 26; i++)
        {
            original.Add((char)('a' + i));
        }

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
                    Decryption(text);
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
                        Encryption(inputField.text);
                    }
                }
                else
                {
                    toggle.GetComponentInChildren<TextMeshProUGUI>().text = "Decryption";
                    // ������ �Է��� ���� �ִٸ� ��ȣȭ
                    if (!string.IsNullOrEmpty(inputField.text))
                    {
                        Decryption(inputField.text);
                    }
                }
            });
    }
    private void OnEnable()
    {
        if (!string.IsNullOrEmpty(inputField.text))
        {
            if (isEncryption)
            {
                Encryption(inputField.text);
            }
            else;
            {
                Decryption(inputField.text);
            }
        }
    }
    #endregion

    #region Custom_Methods
    /// <summary>
    /// ���� ��ȣ������ ����
    /// </summary>
    /// <param name="originalText">��ȣȭ�� ��</param>
    protected void Encryption(string originalText)
    {
        StringBuilder sb = new StringBuilder();

        foreach (char text in originalText)
        {
            // �빮��
            if (text >= 65 && text <= 97)
            {
                // �ҹ��ڷ� ����
                char lowerText = (char)(text + 32);
                // �ҹ��� �� ȹ��
                char encryptionText = encryption[lowerText];
                // ȹ���� �ҹ��ڸ� �빮�ڷ� ����
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

    /// <summary>
    /// ��ȣ���� ������ ����
    /// </summary>
    /// <param name="encryptionText">��ȣȭ�� ��ȣ��</param>
    protected void Decryption(string encryptionText)
    {
        StringBuilder sb = new StringBuilder();

        foreach (char text in encryptionText)
        {
            // �빮��
            if (text >= 65 && text <= 97)
            {
                // �ҹ��ڷ� ����
                char lowerText = (char)(text + 32);
                // �ҹ��ڸ� ���������� ������ �ִ� Ű�� ȹ��
                char decryptionText = encryption.FirstOrDefault(value => value.Value == lowerText).Key;
                // ȹ���� ���� �빮�ڷ� ����
                decryptionText = (char)(decryptionText - 32);
                // sb�� �߰�
                sb.Append(decryptionText);
            }
            // �ҹ���
            else if (text >= 97 && text <= 122)
            {
                // �ҹ��ڸ� ���������� ������ �ִ� Ű�� ȹ��
                char decryptionText = encryption.FirstOrDefault(value => value.Value == text).Key;
                // sb�� �߰�
                sb.Append(decryptionText);
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

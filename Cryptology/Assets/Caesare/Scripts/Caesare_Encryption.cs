using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;

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
            int changeValue = Mathf.Clamp(value, 0, 25);
            PlayerPrefs.SetInt("caesareKeyValue", changeValue);
            SetEncryptionWord();
            if (!string.IsNullOrEmpty(originalInputField.text))
            {
                Encryption(originalInputField.text);
            }
        }
    }
    [SerializeField]
    private TMP_InputField keyValueInputField;
    [SerializeField]
    private Button keyValueRandomBtn;
    [SerializeField]
    private List<WordScript> worldList = new List<WordScript>();
    [SerializeField]
    private TMP_InputField originalInputField;
    [SerializeField]
    private TMP_InputField outputField;
    [SerializeField]
    private Button explainOpenBtn;
    [SerializeField]
    private Button explainCloseBtn;
    [SerializeField]
    private GameObject explain;
    #endregion

    #region Private_Field
    private List<char> original = new List<char>();
    private Dictionary<char, char> encryption = new Dictionary<char, char>();
    #endregion

#region MonoBehaviour_CallBacks
    private void Awake()
    {
        UISetting();

        for (int i = 0; i < 26; i++)
        {
            original.Add((char)('a' + i));
        }
    }

    private void OnEnable()
    {
        SetEncryptionWord();
    }

    private void OnDisable()
    {
        encryption.Clear();
    }
    #endregion

    #region Custom_Methods
    private void UISetting()
    {
        keyValueInputField.text = caesareKeyValue.ToString();

        keyValueRandomBtn.onClick.AddListener(
            () =>
            {
                caesareKeyValue = Random.Range(1, 25);
                keyValueInputField.text = caesareKeyValue.ToString();
            });
        keyValueInputField.onEndEdit.AddListener(
            (text) =>
            {
                caesareKeyValue = Mathf.Clamp(int.Parse(text), 1, 25);

            });
        originalInputField.onEndEdit.AddListener(
            (text) =>
            {
                Encryption(text);
            });
        explainOpenBtn.onClick.AddListener(
            () =>
            {
                explain.SetActive(true);
            });
        explainCloseBtn.onClick.AddListener(
            () =>
            {
                explain.SetActive(false);
            });
    }

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
            worldList[i].World = (World)addCode;
            encryption.Add(original[i], (char)addCode);
        }
    }

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
    #endregion

}

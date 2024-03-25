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
            // 대문자
            if (text >= 65 && text <= 90)
            {
                // 소문자로 변경
                char lowerText = (char)(text + 32);
                // 소문자 값 획득
                char encryptionText = encryption[lowerText];
                // 획득한 소문자 대문자로 변경
                encryptionText = (char)(encryptionText - 32);
                // sb에 추가
                sb.Append(encryptionText);
            }
            // 소문자
            else if (text >= 97 && text <= 122)
            {
                sb.Append(encryption[text]);
            }
            // 그외의 특수 문자
            else
            {
                sb.Append(text);
            }
        }
        outputField.text = sb.ToString();
    }
    #endregion

}

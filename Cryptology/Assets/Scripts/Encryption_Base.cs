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
    [Tooltip("치환된 알파벳 표시")]
    [SerializeField]
    protected List<WordNode> wordList = new List<WordNode>();

    [Tooltip("변환할 문자열 입력 필드")]
    [SerializeField]
    protected TMP_InputField inputField;

    [Tooltip("변환된 문자열 출력 필드")]
    [SerializeField]
    protected TMP_InputField outputField;

    [Tooltip("도움말 여는 버튼")]
    [SerializeField]
    protected Button explainOpenBtn;

    [Tooltip("도움말 닫는 버튼")]
    [SerializeField]
    protected Button explainCloseBtn;

    [Tooltip("도움말")]
    [SerializeField]
    protected GameObject explain;

    [Tooltip("스위칭 토글")]
    [SerializeField]
    protected Toggle toggle;
    #endregion

    #region Private_Field
    // 알파벳 원본
    protected List<char> original = new List<char>();
    // 치환된 알파벳
    protected Dictionary<char, char> encryption = new Dictionary<char, char>();
    // 암호화할것인지 확인 변수
    protected bool isEncryption = true;
    #endregion

    #region MonoBehaviour_Callbacks
    private void Awake()
    {
        // 원본 알파벳 저장
        for (int i = 0; i < 26; i++)
        {
            original.Add((char)('a' + i));
        }

        // 도움말 여는 버튼 이벤트 추가
        explainOpenBtn.onClick.AddListener(
            () =>
            {
                explain.SetActive(true);
            });

        // 도움말 닫는 버튼 이벤트 추가
        explainCloseBtn.onClick.AddListener(
            () =>
            {
                explain.SetActive(false);
            });

        // 유저 입력 완료 이벤트 추가
        inputField.onEndEdit.AddListener(
            (text) =>
            {
                // 암호화
                if (isEncryption)
                {
                    Encryption(text);
                }
                // 해독
                else
                {
                    Decryption(text);
                }
            });

        // 암호화 인지 해독인지 확인 토글 이벤트 추가
        toggle.onValueChanged.AddListener(
            (value) =>
            {
                isEncryption = value;
                if (value)
                {
                    toggle.GetComponentInChildren<TextMeshProUGUI>().text = "Encryption";
                    // 기존에 입력한 값이 있다면 암호화
                    if (!string.IsNullOrEmpty(inputField.text))
                    {
                        Encryption(inputField.text);
                    }
                }
                else
                {
                    toggle.GetComponentInChildren<TextMeshProUGUI>().text = "Decryption";
                    // 기존에 입력한 값이 있다면 복호화
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
    /// 평문을 암호문으로 변경
    /// </summary>
    /// <param name="originalText">암호화할 평문</param>
    protected void Encryption(string originalText)
    {
        StringBuilder sb = new StringBuilder();

        foreach (char text in originalText)
        {
            // 대문자
            if (text >= 65 && text <= 97)
            {
                // 소문자로 변경
                char lowerText = (char)(text + 32);
                // 소문자 값 획득
                char encryptionText = encryption[lowerText];
                // 획득한 소문자를 대문자로 변경
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

    /// <summary>
    /// 암호문을 평문으로 변경
    /// </summary>
    /// <param name="encryptionText">복호화할 암호문</param>
    protected void Decryption(string encryptionText)
    {
        StringBuilder sb = new StringBuilder();

        foreach (char text in encryptionText)
        {
            // 대문자
            if (text >= 65 && text <= 97)
            {
                // 소문자로 변경
                char lowerText = (char)(text + 32);
                // 소문자를 벨류값으로 가지고 있는 키를 획득
                char decryptionText = encryption.FirstOrDefault(value => value.Value == lowerText).Key;
                // 획득한 값을 대문자로 변경
                decryptionText = (char)(decryptionText - 32);
                // sb에 추가
                sb.Append(decryptionText);
            }
            // 소문자
            else if (text >= 97 && text <= 122)
            {
                // 소문자를 벨류값으로 가지고 있는 키를 획득
                char decryptionText = encryption.FirstOrDefault(value => value.Value == text).Key;
                // sb에 추가
                sb.Append(decryptionText);
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

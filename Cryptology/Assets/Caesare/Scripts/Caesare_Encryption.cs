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

    [Tooltip("키 값 입력 필드")]
    [SerializeField]
    private TMP_InputField keyValueInputField;

    [Tooltip("키 값 랜던 변경 버튼")]
    [SerializeField]
    private Button keyValueRandomBtn;

    [Tooltip("치환된 알파벳 표시")]
    [SerializeField]
    private List<WordNode> wordList = new List<WordNode>();

    [Tooltip("변환할 문자열 입력 필드")]
    [SerializeField]
    private TMP_InputField inputField;

    [Tooltip("변환된 문자열 출력 필드")]
    [SerializeField]
    private TMP_InputField outputField;

    [Tooltip("도움말 여는 버튼")]
    [SerializeField]
    private Button explainOpenBtn;

    [Tooltip("도움말 닫는 버튼")]
    [SerializeField]
    private Button explainCloseBtn;

    [Tooltip("도움말")]
    [SerializeField]
    private GameObject explain;

    [Tooltip("스위칭 토글")]
    [SerializeField]
    private Toggle toggle;
    #endregion

    #region Private_Field
    // 알파벳 원본
    private List<char> original = new List<char>();
    // 치환된 알파벳
    private Dictionary<char, char> encryption = new Dictionary<char, char>();
    // 암호화인지 확인 변수
    private bool isEncryption = true;
    #endregion

#region MonoBehaviour_CallBacks
    private void Awake()
    {
        // UI 초기화
        UISetting();

        // 원본 알파벳 저장
        for (int i = 0; i < 26; i++)
        {
            original.Add((char)('a' + i));
        }
    }

    private void OnEnable()
    {
        // 치환표 설정
        SetEncryptionWord();
    }

    private void OnDisable()
    {
        // 딕셔너리 초기화
        encryption.Clear();
    }
    #endregion

    #region Custom_Methods
    /// <summary>
    /// UI 이벤트 설정
    /// </summary>
    private void UISetting()
    {
        // 키 값 인풋 필드의 텍스트로 설정
        keyValueInputField.text = caesareKeyValue.ToString();

        // 인풋 필드 입력 완료 이벤트 추가
        keyValueInputField.onEndEdit.AddListener(
            (text) =>
            {
                // 키 값이 1 ~ 25사이의 값만 가지도록 설정
                caesareKeyValue = Mathf.Clamp(int.Parse(text), 1, 25);

            });

        // 키 값 랜던 버튼 이벤트 추가
        keyValueRandomBtn.onClick.AddListener(
        () =>
        {
            // 키 값이 1 ~ 25사이의 값만 가지도록 설정
            caesareKeyValue = Random.Range(1, 25);
            // 인풋 필드 텍스트값 갱신
            keyValueInputField.text = caesareKeyValue.ToString();
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

                }
            });

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
                        DeCryption(inputField.text);
                    }
                }
            });
    }

    /// <summary>
    /// UI 치환표 설정
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
    /// 평문 암호화
    /// </summary>
    /// <param name="originalText">암호화할 평문</param>
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

    private void DeCryption(string enCryptionText)
    {
        StringBuilder sb = new StringBuilder();

        foreach (char text in enCryptionText)
        {
            // 대문자
            if (text >= 65 && text <= 90)
            {
                // 소문자로 변경
                char lowerText = (char)(text + 32);
                // 소문자를 벨류값으로 가지고 있는 키를 획득
                char deCryptionText = encryption.FirstOrDefault(value => value.Value == lowerText).Key;
                // 획득한 값을 대문자로 변경
                deCryptionText = (char)(deCryptionText - 32);
                // sb에 추가
                sb.Append(deCryptionText);
            }
            // 소문자
            else if (text >= 97 && text <= 122)
            {
                // 소문자를 벨류값으로 가지고 있는 키를 획득
                char deCryptionText = encryption.FirstOrDefault(value => value.Value == text).Key;
                // sb에 추가
                sb.Append(deCryptionText);
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

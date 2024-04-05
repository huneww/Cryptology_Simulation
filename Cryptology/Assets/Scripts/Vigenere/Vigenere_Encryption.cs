using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;

public class Vigenere_Encryption : Encryption_Base
{
    #region Open_Private_Fields
    [Space(10), Header("Vigenere Fields")]
    [SerializeField]
    [Tooltip("키 값으로할 단어 혹은 알파벳 모음")]
    private TMP_InputField keyInputField;
    [SerializeField]
    [Tooltip("키 값 랜덤 변환 버튼")]
    private Button keyRandomBtn;
    [SerializeField]
    [Tooltip("예시 키값 텍스트")]
    private TextMeshProUGUI sampleText;
    [Min(3)]
    [Tooltip("단어 길이 최소 값")]
    private int wordLengthMin = 3;
    [SerializeField]
    [Min(3)]
    [Tooltip("단어 길이 최대 값")]
    private int wordLengthMax = 15;
    #endregion

    #region Private_Fields
    // 키 값 문자열
    private string VigenereKey
    {
        get
        {
            if (!PlayerPrefs.HasKey("VigenereKey"))
                return "love";
            return PlayerPrefs.GetString("VigenereKey");
        }
        set
        {
            PlayerPrefs.SetString("VigenereKey", value);
            // 텍스트 값 변경
            keyInputField.text = value;
            // 치환표 갱신
            SetEncryptionWord();
            // 키 값 변경시 벨류값 변경
            ChangeKeyTextValue(value);
        }
    }

    // 영어 단어 저장 딕셔너리
    // 키 값 : 문자의 길이
    private Dictionary<int, List<string>> words = new Dictionary<int, List<string>>();

    // 키 값의 벨류
    private List<int> keyTextValue = new List<int>();

    #endregion

    // 샘플 텍스트 업데이트 대리자
    public Action<string> sampleTextUpdate;

    #region MonoBehaviour_Callbacks
    private void Awake()
    {
        LoadEnglishWords();
    }

    private void LoadEnglishWords()
    {
        // 텍스트 파일을 읽어서 영단어 저장
        TextAsset text = Resources.Load("Words") as TextAsset;

        // 읽어온 텍스트를 엔터문자로 구분후 배열로 저장
        string[] dic = text.text.Split("\n").ToArray();

        // 최소, 최대 길이의 문자열의 리스트만 초기화
        for (int i = wordLengthMin; i <= wordLengthMax; i++)
        {
            words[i] = new List<string>();
        }

        // 읽어온 영단어를 순회
        foreach (string word in dic)
        {
            // 영단어 길이를 획득
            int length = word.Length;
            // 영단어 길이가 짧은지 큰지 확인
            if (length >= wordLengthMin && length <= wordLengthMax)
            {
                // 딕셔너리에 길이에 맞는 리스트에 저장
                words[length].Add(word);
            }
        }
    }

    private void Start()
    {
        // UI 셋팅
        UISetting();
        base.UISetting();
        ChangeKeyTextValue(VigenereKey);
        // 치환표 갱신
        SetEncryptionWord();
    }

    private void OnEnable()
    {
        // 원본 알파벳 리스트 비활성화
        foreach (WordNode obj in wordList)
        {
            obj.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        // 원본 알파벳 리스트 활성화
        foreach (WordNode obj in wordList)
        {
            obj.gameObject.SetActive(true);
        }
    }
    #endregion

    #region Custom_Methods
    private new void UISetting()
    {
        // 초기 인풋 텍스트 설정
        keyInputField.text = VigenereKey;

        // 키 입력 완료 이벤트 추가
        keyInputField.onEndEdit.AddListener(
            (text) =>
            {
                // 텍스트 값이 없는지 확인
                if (string.IsNullOrEmpty(text))
                {
                    VigenereKey = "love";
                }
                else
                {
                    // 입력값들을 저장
                    VigenereKey = keyInputField.text;
                }
            });

        // 키 값 랜덤 버튼 클리 이벤트 추가
        keyRandomBtn.onClick.AddListener(
            () =>
            {
                while (true)
                {
                    // 문자열 길이 지정
                    int length = UnityEngine.Random.Range(wordLengthMin, wordLengthMax);
                    // 길이의 단어가 있는지 확인
                    if (words[length].Count > 0)
                    {
                        // 리스트의 인덱스 획득
                        int strIndex = UnityEngine.Random.Range(0, words[length].Count);
                        // 문자열 저장
                        string randomStr = words[length][strIndex];
                        // 키 값 변경
                        VigenereKey = randomStr;
                        // 반복문 종료
                        break;
                    }
                }
            });
    }

    /// <summary>
    /// UI 치환표 설정
    /// </summary>
    public override void SetEncryptionWord()
    {
        // Vigenere_Sample의 SampleTextUpdate 메서드와 연결되어있음
        sampleTextUpdate(VigenereKey);

        // 입력값 있는지 확인
        if (!string.IsNullOrEmpty(inputField.text))
        {
            // 입력값 갱신
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

    private void ChangeKeyTextValue(string changeText)
    {
        // 리스트 초기화
        keyTextValue.Clear();
        // 변경된 키 값 소문자로 변환
        string lowerText = changeText.ToLower();
        // 키 값의 맞게 벨류값 리스트에 저장
        for (int i = 0; i < lowerText.Length; i++)
        {
            int charToInt = lowerText[i] - 96;
            keyTextValue.Add(charToInt);
        }
    }

    public override void Encryption(string originalText)
    {
        Debug.Log("override Encryption");
        // 입력 값의 벨류값 저장
        int[] originalTextValue = new int[originalText.Length];
        // 암호화한 문자열 제작 StringBuilder
        StringBuilder sb = new StringBuilder();

        // 입력값 소문자로 변환
        string lowerText = originalText.ToLower();

        // 입력값의 벨류값 저장
        for (int i = 0; i < originalText.Length; i++)
        {
            int charToInt = lowerText[i] - 96;
            originalTextValue[i] = charToInt;
        }

        // 암호화
        for (int i = 0; i < originalText.Length; i++)
        {
            int index = i % VigenereKey.Length;
            int sum = originalTextValue[i] + keyTextValue[index];
            if (sum > 26)
            {
                sum -= 26;
            }
            // 합값 알파벳으로 변경
            char ch = (char)(sum + 96);
            Debug.Log($"{sum}, {ch}");
            sb.Append(ch.ToString());
        }

        // 아웃풋 필드의 텍스트 적용
        outputField.text = sb.ToString();

    }

    public override void Decryption(string encryptionText)
    {
        Debug.Log("override Decryption");
        // 입력 값의 벨류값 저장
        int[] encryptionTextValue = new int[encryptionText.Length];
        // 암호화한 문자열 제작 StringBuilder
        StringBuilder sb = new StringBuilder();

        // 입력 값 소문자로 변환
        string lowerText = encryptionText.ToLower();
        for (int i = 0; i < encryptionText.Length; i++)
        {
            int charToInt = lowerText[i] - 96;
            encryptionTextValue[i] = charToInt;
        }

        // 복호화
        for (int i = 0; i < encryptionText.Length; i++)
        {
            int index = i % VigenereKey.Length;
            Debug.Log(index);
            int minus = encryptionTextValue[i] - keyTextValue[index];
            if (minus <= 0)
            {
                minus += 26;
            }
            char ch = (char)(minus + 96);
            sb.Append(ch.ToString());
        }

        outputField.text = sb.ToString();

    }
    #endregion
}

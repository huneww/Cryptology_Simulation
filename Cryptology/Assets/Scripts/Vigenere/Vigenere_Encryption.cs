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
    private string vigenereKey
    {
        get
        {
            if (!PlayerPrefs.HasKey("vigenereKey"))
                return "Love";
            return PlayerPrefs.GetString(vigenereKey);
        }
        set
        {
            PlayerPrefs.SetString(vigenereKey, value);
            // 텍스트 값 변경
            keyInputField.text = value;
            // 치환표 갱신
            SetEncryptionWord();
        }
    }

    // 키 값 문자열 벨류 저장 변수
    private List<int> keyIndex = new List<int>();

    // 영어 단어 저장 딕셔너리
    private Dictionary<int, List<string>> words = new Dictionary<int, List<string>>();

    #endregion

    // 샘플 텍스트 업데이트 대리자
    public Action<string, List<int>> sampleTextUpdate;

    #region MonoBehaviour_Callbacks
    private void Awake()
    {
        // 텍스트 파일을 읽어서 영단어 저장
        TextAsset text = Resources.Load("Words") as TextAsset;
        // 키 벨류 설정
        for (int i = 1; i <= 26; i++)
        {
            keyIndex.Add(i);
        }

        string[] dic = text.text.Split("\n").ToArray();

        for (int i = wordLengthMin; i <= wordLengthMax; i++)
        {
            words[i] = new List<string>();
        }

        foreach (string word in dic)
        {
            int length = word.Length;
            if (length > wordLengthMin && length <= wordLengthMax)
            {
                words[length].Add(word);
            }
        }
    }

    private void Start()
    {
        // UI 셋팅
        UISetting();
        base.UISetting();
    }

    private void OnEnable()
    {
        // 치환표 갱신
        SetEncryptionWord();
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
        keyInputField.text = vigenereKey;

        // 키 입력 완료 이벤트 추가
        keyInputField.onEndEdit.AddListener(
            (text) =>
            {
                // 텍스트 값이 없는지 확인
                if (string.IsNullOrEmpty(text))
                {
                    vigenereKey = "Love";
                }
                else
                {
                    // 입력값들을 저장
                    vigenereKey = keyInputField.text;
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
                        vigenereKey = randomStr;
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
        sampleTextUpdate(vigenereKey, keyIndex);

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
    #endregion
}

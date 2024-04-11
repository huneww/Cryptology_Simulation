using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using System;

[RequireComponent(typeof(Vigenere_Encryption), typeof(Vigenere_Decryption))]
public class Vigenere_Cryptology : Cryptology_Base
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
    [Min(3)]
    [Tooltip("단어 길이 최소 값")]
    private int wordLengthMin = 3;
    [SerializeField]
    [Min(3)]
    [Tooltip("단어 길이 최대 값")]
    private int wordLengthMax = 15;
    #endregion

    #region Private_Fields
    private Vigenere_Encryption encryption;
    private Vigenere_Decryption decryption;

    private string vigenereKey = "love";
    // 키 값 문자열
    private string VigenereKey
    {
        get
        {
            if (string.IsNullOrEmpty(vigenereKey))
            {
                vigenereKey = "love";
                return vigenereKey;
            }
            return vigenereKey;
        }
        set
        {
            vigenereKey = value;
            // 텍스트 값 변경
            keyInputField.text = value;
            // 샘플 텍스트 갱신
            SetSampleText();
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
    public override void Init()
    {
        // 대리자 연결
        GetComponentInChildren<Vigenere_Sample>().Init();
        // 암호화 방법 획득
        GetCryptologyWay();
        // 영단어 저장
        LoadEnglishWords();
        // UI 셋팅
        UISetting();
        // 키 값의 벨류값 설정
        ChangeKeyTextValue(VigenereKey);
        // 치환표 갱신
        SetSampleText();
    }

    private void GetCryptologyWay()
    {
        encryption = GetComponent<Vigenere_Encryption>();
        decryption = GetComponent<Vigenere_Decryption>();
    }

    /// <summary>
    /// 영단어 획득(Resources/Words.txt에서 영단어 획득)
    /// </summary>
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

    private void UISetting()
    {
        // 원본 알파벳 저장
        for (int i = 0; i < 26; i++)
        {
            original.Add((char)('a' + i));
        }

        // 유저 입력 완료 이벤트 추가
        inputField.onEndEdit.AddListener(
            (text) =>
            {
                // 암호화
                if (isEncryption)
                {
                    //Encryption(text);
                    outputField.text = encryption.Encryption(text, VigenereKey);
                }
                // 해독
                else
                {
                    //Decryption(text);
                    outputField.text = decryption.Decryption(text, VigenereKey);
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
                        outputField.text = encryption.Encryption(inputField.text, VigenereKey);
                    }
                }
                else
                {
                    toggle.GetComponentInChildren<TextMeshProUGUI>().text = "Decryption";
                    // 기존에 입력한 값이 있다면 복호화
                    if (!string.IsNullOrEmpty(inputField.text))
                    {
                        outputField.text = decryption.Decryption(inputField.text, VigenereKey);
                    }
                }
            });

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
                    // 입력값들을 소문자로 저장
                    VigenereKey = keyInputField.text.ToLower();
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
    /// 샘플 텍스트 설정
    /// </summary>
    public void SetSampleText()
    {
        // Vigenere_Sample의 SampleTextUpdate 메서드와 연결되어있음
        sampleTextUpdate(VigenereKey);

        // 입력값 있는지 확인
        if (!string.IsNullOrEmpty(inputField.text))
        {
            // 입력값 갱신
            if (isEncryption)
            {
                outputField.text = encryption.Encryption(inputField.text, VigenereKey);
            }
            else
            {
                outputField.text = decryption.Decryption(inputField.text, VigenereKey);
            }
        }
    }

    /// <summary>
    /// 변경된 키값의 벨류값 변경
    /// </summary>
    /// <param name="changeText">변경된 키 값</param>
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
    #endregion
}

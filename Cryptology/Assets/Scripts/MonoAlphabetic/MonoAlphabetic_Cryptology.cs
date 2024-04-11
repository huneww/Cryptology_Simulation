using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(MonoAlphabetic_Encryption), typeof(MonoAlphabetic_Decryption))]
public class MonoAlphabetic_Cryptology : Cryptology_Base
{
    #region Open_Private_Fields
    [Tooltip("랜덤 시드 값")]
    [SerializeField]
    private int monoAlphabeticSeed
    {
        get
        {
            if (!PlayerPrefs.HasKey("monoAlphabeticSeed"))
                return 0;
            return PlayerPrefs.GetInt("monoAlphabeticSeed");
        }
        set
        {
            // 값 저장
            PlayerPrefs.SetInt("monoAlphabeticSeed", value);
            // 치환표 갱신
            SetEncryptionWord();
            // 시드값 텍스트 갱신
            seedInputField.text = monoAlphabeticSeed.ToString();
        }
    }

    [Space(10), Header("MonoAlphabetic Fields")]
    [Tooltip("랜덤 시드 값 인풋 필드")]
    [SerializeField]
    private TMP_InputField seedInputField;

    [Tooltip("랜덤 시드 값 랜덤 변경 버튼")]
    [SerializeField]
    private Button seedRandomBtn;
    #endregion;

    #region Private_Fields
    private MonoAlphabetic_Encryption encryption;
    private MonoAlphabetic_Decryption decryption;
    #endregion

    #region MonoBehaviour_Callbacks
    private void OnEnable()
    {
        // 치환표 설정
        SetEncryptionWord();
    }
    #endregion

    #region Custom_Methods
    public override void Init()
    {
        // 암호화 방법 획득
        GetCryptologyWay();
        // UI 셋팅
        UISetting();
    }

    private void GetCryptologyWay()
    {
        encryption = GetComponent<MonoAlphabetic_Encryption>();
        decryption = GetComponent<MonoAlphabetic_Decryption>();
    }

    /// <summary>
    /// UI 셋팅
    /// </summary>
    public void UISetting()
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
                    outputField.text = encryption.Encryption(text, encryptionWord);
                }
                // 해독
                else
                {
                    //Decryption(text);
                    outputField.text = decryption.Decryption(text, encryptionWord);
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
                        outputField.text = encryption.Encryption(inputField.text, encryptionWord);
                    }
                }
                else
                {
                    toggle.GetComponentInChildren<TextMeshProUGUI>().text = "Decryption";
                    // 기존에 입력한 값이 있다면 복호화
                    if (!string.IsNullOrEmpty(inputField.text))
                    {
                        outputField.text = decryption.Decryption(inputField.text, encryptionWord);
                    }
                }
            });

        // 랜덤 시드 값 임이의 값으로 설정
        Random.InitState(monoAlphabeticSeed);

        // 인풋 필드 입력 완료 이벤트 추가
        seedInputField.onEndEdit.AddListener(
            (text) =>
            {
                monoAlphabeticSeed = Mathf.Clamp(int.Parse(text), int.MinValue, int.MaxValue);
            });

        // 시드 값 랜덤 버튼 이벤트 추가
        seedRandomBtn.onClick.AddListener(
            () =>
            {
                monoAlphabeticSeed = Random.Range(int.MinValue, int.MaxValue);
            });
    }

    /// <summary>
    /// UI 치환표 설정
    /// </summary>
    private void SetEncryptionWord()
    {
        // 딕셔너리 초기화
        encryptionWord.Clear();
        for (int i = 0; i < 26;)
        {
            // 아스키 코드 값을 이용해서 랜덤한 알파벳 획득
            // Random의 시드값을 지정하여서 동일한 값이 계속해서 나올것
            int code = Random.Range(97, 123);
            // 추가에 성공하면
            if (encryptionWord.TryAdd((char)('a' + i), (char)(code)))
            {
                // 치환표의 글자 변경
                wordList[i].Word = (Word)code;
                // i값 증가
                i++;
            }
        }

        // 입력값 있는지 확인
        if (!string.IsNullOrEmpty(inputField.text))
        {
            // 입력값 갱신
            if (isEncryption)
            {
                outputField.text = encryption.Encryption(inputField.text, encryptionWord);
            }
            else
            {
                outputField.text = decryption.Decryption(inputField.text, encryptionWord);
            }
        }
    }
    #endregion

}

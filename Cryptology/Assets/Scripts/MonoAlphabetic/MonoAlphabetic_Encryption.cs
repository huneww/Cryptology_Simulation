using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class MonoAlphabetic_Encryption : Encryption_Base
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

    #region MonoBehaviour_Callbacks
    private void Start()
    {
        // UI 셋팅
        UISetting();
        // Base클래스 UI 초기화 메서드 호출
        base.UISetting();
    }

    private void OnEnable()
    {
        // 치환표 설정
        SetEncryptionWord();
    }
    #endregion

    #region Custom_Methods
    /// <summary>
    /// UI 셋팅
    /// </summary>
    public override void UISetting()
    {
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
    override public void SetEncryptionWord()
    {
        // 딕셔너리 초기화
        encryption.Clear();
        for (int i = 0; i < 26;)
        {
            // 아스키 코드 값을 이용해서 랜덤한 알파벳 획득
            // Random의 시드값을 지정하여서 동일한 값이 계속해서 나올것
            int code = Random.Range(97, 123);
            // 추가에 성공하면
            if (encryption.TryAdd((char)('a' + i), (char)(code)))
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

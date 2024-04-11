using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

[RequireComponent(typeof(Caesare_Encryption), typeof(Caesare_Decryption))]
public class Caesare_Cryptology : Cryptology_Base
{
    #region Open_Private_Field
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
            // 값 1 ~ 25 값 사이에 있도록 설정
            int changeValue = Mathf.Clamp(value, 1, 25);
            // 값 저장
            PlayerPrefs.SetInt("caesareKeyValue", changeValue);
            // 치환표 갱신
            SetSubstitution();
        }
    }

    [Space(10), Header("Caesare Fields")]
    [Tooltip("키 값 인풋 필드")]
    [SerializeField]
    private TMP_InputField keyValueInputField;

    [Tooltip("키 값 랜던 변경 버튼")]
    [SerializeField]
    private Button keyValueRandomBtn;
    #endregion

    #region Private_Fields
    private Caesare_Encryption encryption;
    private Caesare_Decryption decryption;
    #endregion

    #region Custom_Methods
    public override void Init()
    {
        // 암호화 방법 획득
        GetCryptologyWay();
        // UI 초기화
        UISetting();
        // 치환표 설정
        SetSubstitution();
    }
    private void GetCryptologyWay()
    {
        encryption = GetComponent<Caesare_Encryption>();
        decryption = GetComponent<Caesare_Decryption>();
    }
    /// <summary>
    /// UI 이벤트 설정
    /// </summary>
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
    }

    /// <summary>
    /// UI 치환표 설정
    /// </summary>
    public void SetSubstitution()
    {
        // 딕셔너리 초기화
        encryptionWord.Clear();
        for (int i = 0; i < 26; i++)
        {
            int addCode = ('a' - caesareKeyValue + i);
            if (addCode < 'a')
            {
                addCode = 'z' - (96 - addCode);
            }
            wordList[i].Word = (Word)addCode;
            encryptionWord.Add(original[i], (char)addCode);
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

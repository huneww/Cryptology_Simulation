using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;
using System.Linq;

public class Caesare_Encryption : Encryption_Base
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
                    Decryption(inputField.text);
                }
            }
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

#region MonoBehaviour_CallBacks
    private void Start()
    {
        // UI 초기화
        UISetting();
    }

    private void OnEnable()
    {
        // 치환표 설정
        SetEncryptionWord();
    }

    private void OnDisable()
    {
        // 치환표 설정
        SetEncryptionWord();
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
    }

    /// <summary>
    /// UI 치환표 설정
    /// </summary>
    private void SetEncryptionWord()
    {
        // 딕셔너리 초기화
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
    #endregion

}

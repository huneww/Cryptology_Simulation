using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enigma_Encryption : Encryption_Base
{
    #region Open_Private_Fields
    [Header("설정 창"), Space(5)]
    [Tooltip("플러그 보드 설정 창")]
    [SerializeField]
    private GameObject plugBoardObj;
    [Tooltip("플러그 보드 버튼")]
    [SerializeField]
    private Button plugBoardBtn;
    [Tooltip("로터 보드 설정 창")]
    [SerializeField]
    private GameObject rotorObj;
    [Tooltip("로터 보드 버튼")]
    [SerializeField]
    private Button rotorBtn;
    #endregion

    #region Private_Fields
    // 플러그 보드 오브젝트
    private PlugBoard plugBoard;
    // 로터 오브젝트
    private Rotor_Group rotor;
    #endregion

    #region Monobehaviour_Callbacks
    private void Awake()
    {
        UISetting();
        // 플러그 보드, 로터의 스크립트 획득
        // 초기화 메서드 호출
        plugBoard = plugBoardObj.GetComponent<PlugBoard>();
        plugBoard.Init();
        rotor = rotorObj.GetComponent<Rotor_Group>();
        rotor.Init();
    }

    private void OnEnable()
    {
        // 원본 알파벳 오브젝트 비활성화
        foreach (WordNode node in wordList)
        {
            node.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        // 원본 알파벳 오브젝트 활성화
        foreach (WordNode node in wordList)
        {
            node.gameObject.SetActive(true);
        }
    }
    #endregion


    #region Custom_Methods
    public override void UISetting()
    {
        // 플러그 보드 활성화 버튼 이벤트
        plugBoardBtn.onClick.AddListener(
            () =>
            {
                rotorObj.SetActive(false);
                plugBoardObj.SetActive(true);
            });

        // 로터 활성화 버튼 이벤트
        rotorBtn.onClick.AddListener(
            () =>
            {
                rotorObj.SetActive(true);
                plugBoardObj.SetActive(false);
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
        // 인풋값 변경 이벤트 추가
        inputField.onValueChanged.AddListener(
            (text) =>
            {
                InputFieldEvent(text);
            });
    }

    /// <summary>
    /// 입력값 암호화
    /// </summary>
    /// <param name="text">입력값</param>
    private void InputFieldEvent(string text)
    {
        // 입력값이 비었는지 확인
        if (!string.IsNullOrEmpty(text))
        {
            StringBuilder sb = new StringBuilder();

            // 텍스트 전체 대문자로 변경
            string newText = text.ToUpper();

            // 입력 텍스트의 맨마지막 인덱스 값 획득
            int lastIndex = text.Length - 1;

            // 입력 문자가 알파벳인지 확인
            if (newText[lastIndex] >= 'A' && newText[lastIndex] <= 'Z')
            {
                // 입력 텍스트의 길이가 아웃풋의 텍스트의 길이보다 길때
                // 짧으면 유저가 입력 텍스트를 1개 지운것
                if (text.Length > outputField.text.Length)
                {

                    // 마지막 텍스트 대문자로 변경
                    char upperText = newText[lastIndex];

                    // 얻은 텍스트를 플러그보드를 통해 아웃풋 값을 획득
                    char plugText = plugBoard.GetText(upperText);

                    // 로터 순회
                    char rotorText = rotor.RoterAction(plugText);

                    // 다시 한번 플러그 보드 확인
                    char returnText = plugBoard.GetText(rotorText);

                    // 아웃풋에 있는 텍스트를 추가
                    sb.Append(outputField.text);

                    // 입력 텍스트의 마지막 값을 소문자로 추가
                    sb.Append(returnText.ToString());
                }
                else
                {
                    // 입력 텍스트의 길이만큼 반복
                    for (int i = 0; i < text.Length; i++)
                    {
                        // 변경된 아웃풋필드의 글자를 추가
                        sb.Append(outputField.text[i]);
                    }
                }
            }
            // 입력 문자가 특수문자, 띄어쓰기 라면
            else
            {
                // 변경없이 텍스트 추가
                sb.Append(outputField.text);
                sb.Append(newText[lastIndex].ToString());
            }
            outputField.text = sb.ToString();
        }
    }
    public override void Decryption(string encryptionText)
    {
        
    }
    public override void Encryption(string originalText)
    {
        
    }
    #endregion

}

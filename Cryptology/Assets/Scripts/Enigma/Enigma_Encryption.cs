using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enigma_Encryption : Encryption_Base
{
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

    private PlugBoard plugBoard;
    private Rotor_Group rotor;

    private void Awake()
    {
        UISetting();
        plugBoard = plugBoardObj.GetComponent<PlugBoard>();
        rotor = rotorObj.GetComponent<Rotor_Group>();
    }

    private void OnEnable()
    {
        foreach (WordNode node in wordList)
        {
            node.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        foreach (WordNode node in wordList)
        {
            node.gameObject.SetActive(true);
        }
    }

    public override void UISetting()
    {
        plugBoardBtn.onClick.AddListener(
            () =>
            {
                rotorObj.SetActive(false);
                plugBoardObj.SetActive(true);
            });

        rotorBtn.onClick.AddListener(
            () =>
            {
                rotorObj.SetActive(true);
                plugBoardObj.SetActive(false);
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
                        Decryption(inputField.text);
                    }
                }
            });

        inputField.onValueChanged.AddListener(
            (text) =>
            {
                InputFieldEvent(text);
            });
    }

    public override void Encryption(string originalText)
    {
        base.Encryption(originalText);
    }

    public override void Decryption(string encryptionText)
    {
        base.Decryption(encryptionText);
    }

    private void InputFieldEvent(string text)
    {
        if (!string.IsNullOrEmpty(text))
        {
            StringBuilder sb = new StringBuilder();

            // 입력 텍스트의 길이가 아웃풋의 텍스트의 길이보다 길때
            // 짧으면 유저가 입력 텍스트를 1개 지운것
            if (text.Length > outputField.text.Length)
            {
                // 입력 텍스트의 맨마지막 인덱스 값 획득
                int lastIndex = text.Length - 1;
                // 마지막 텍스트 대문자로 변경
                string upperText = text[lastIndex].ToString().ToUpper();
                // 얻은 텍스트를 플러그보드를 통해 아웃풋 값을 획득
                string plugText = plugBoard.GetText(upperText);
                // 로터 순회
                char roterText = rotor.RoterAction(plugText);
                // 다시 한번 플러그 보드 확인
                string returnText = plugBoard.GetText(roterText.ToString());
                // 아웃풋에 있는 텍스트를 추가
                sb.Append(outputField.text);
                // 입력 텍스트의 마지막 값을 소문자로 추가
                sb.Append(returnText.ToLower());
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
            outputField.text = sb.ToString();
        }
    }

}

using UnityEngine;
using TMPro;
using System.Collections.Generic;
using TMP_Text = TMPro.TextMeshProUGUI;
using Unity.VisualScripting;
using System.Collections;
using System;

public class Plug : MonoBehaviour
{
    [Tooltip("아웃풋 선택 드랍다운")]
    [SerializeField]
    private TMP_Dropdown outText;
    [Tooltip("인풋 확인")]
    [SerializeField]
    private string inText;
    [Tooltip("연결된 Plug")]
    [SerializeField]
    private Plug connectedText;
    public string InText
    {
        get
        {
            return inText;
        }
    }
    [SerializeField]
    PlugBoard plugBoard;


    public void Init(char inText)
    {
        // 플러그 보드를 획득
        plugBoard = GetComponentInParent<PlugBoard>();

        // 인풋 텍스트 초기화
        this.inText = inText.ToString();
        // 초기 연결 Plug는 자기자신으로 초기화
        connectedText = this;

        // 드랍다운 메뉴 옵션 전부 제거
        outText.ClearOptions();

        // 드랍다운 메뉴에 추가할 리스트
        List<string> words = new List<string>();
        for (int i = 'A'; i <= 'Z'; i++)
        {
            char intToChar = (char)i;
            words.Add(intToChar.ToString());
        }
        outText.AddOptions(words);

        // 인풋 텍스트 초기화
        TMP_Text text = GetComponentInChildren<TMP_Text>();
        text.text = inText.ToString();

        // 드랍다운 벨류값 설정
        outText.value = inText - 'A';
        // 드랍다운 벨류값 게임씬에 적용
        // 메소드 호출하지 않으면 벨류값이 미반영
        outText.RefreshShownValue();
        // 드랍다운 벨류값 변경시 이벤트 추가
        outText.onValueChanged.AddListener(
            (value) =>
            {
                if (!plugBoard.isChaing)
                {
                    plugBoard.isChaing = true;
                    StartCoroutine(ConnectedTextChange(value));
                }
            });
    }

    /// <summary>
    /// 연결된 플러그 변경
    /// </summary>
    /// <param name="value">연결할 플로거의 드랍다운 벨류</param>
    /// <param name="isConnected">연결되어있는지 확인, false = 연결되어있지않음</param>
    public IEnumerator ConnectedTextChange(int value, bool isConnected = false)
    {
        string text = outText.options[value].text;
        connectedText = plugBoard.GetPlug(text);
        outText.value = value;
        outText.RefreshShownValue();

        if (!isConnected)
        {
            char ch = char.Parse(inText);
            int charToInt = ch - 'A';
            StartCoroutine(connectedText.ConnectedTextChange(charToInt, true));
        }

        // 스택 오버플로어 발생
        //if (connectedText == this)
        //{
        //    // 연결할 플러그의 인풋값 획득
        //    string text = outText.options[value].text;
        //    // 플러그 변경
        //    connectedText = plugBoard.GetPlug(text);
        //    if (connectedText == null)
        //    {
        //        Debug.LogError("ConnectedText is Null");
        //        yield break;
        //    }

        //    // 연결되어있는지 않으면
        //    if (!isConnected)
        //    {
        //        // 현재 스크립트의 인풋값 숫자로 변환
        //        char ch = char.Parse(inText);
        //        int charToInt = ch - 'A';
        //        // 연결된 플러그의 드랍다운메뉴 획득
        //        TMP_Dropdown drop = connectedText.GetComponentInChildren<TMP_Dropdown>();
        //        // 드랍다운의 벨류값 변경
        //        drop.value = charToInt;
        //        // 게임씬에 적용
        //        drop.RefreshShownValue();
        //        // 인풋값의 숫자와 재귀함수란걸 보여주면서 메서드 호출
        //        StartCoroutine(connectedText.ConnectedTextChange(charToInt, true));
        //    }
        //}
        //else
        //{
        //    if (!isConnected)
        //    {
        //        // 기존에 연결되있던 플로거의 연결을 변경
        //        char ch = char.Parse(connectedText.InText);
        //        int charToInt = ch - 'A';
        //        TMP_Dropdown dropdown2 = connectedText.GetComponentInChildren<TMP_Dropdown>();
        //        dropdown2.value = charToInt;
        //        dropdown2.RefreshShownValue();
        //        StartCoroutine(connectedText.ConnectedTextChange(charToInt, true));
        //    }

        //    // 연결할 플러그의 인풋값 획득
        //    string text = outText.options[value].text;
        //    // 플러그 변경
        //    connectedText = plugBoard.GetPlug(text);
        //    TMP_Dropdown dropdown = connectedText.GetComponentInChildren<TMP_Dropdown>();
        //    dropdown.value = value;
        //    dropdown.RefreshShownValue();

        //    // 연결되어있는지 않으면
        //    if (!isConnected)
        //    {
        //        char ch = char.Parse(inText);
        //        int charToInt = ch - 'A';
        //        // 변경된 플러그의 연결 변경
        //        StartCoroutine(connectedText.ConnectedTextChange(charToInt, true));
        //    }
        //}
        Debug.Log(gameObject.name);
        plugBoard.isChaing = false;
        yield return null;
    }

}

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

    [HideInInspector]
    public string OutString
    {
        get
        {
            return outText.options[outText.value].text;
        }
    }

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
        // outText.RefreshShownValue(); // 이 메서드로 하면 지정한 이벤트가 호출됨
        // 지정한 이벤트가 호출되지 않음
        outText.SetValueWithoutNotify(outText.value);
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
        if (connectedText == this)
        {
            string text = outText.options[value].text;
            connectedText = plugBoard.GetPlug(text);
            outText.SetValueWithoutNotify(value);

            if (!isConnected)
            {
                char ch = char.Parse(inText);
                int charToInt = ch - 'A';
                StartCoroutine(connectedText.ConnectedTextChange(charToInt, true));
            }
        }
        else
        {
            // 연결할 플러그 획득
            string text = outText.options[value].text;
            Plug newcon = plugBoard.GetPlug(text);

            // 새롭게 연결할 플로그가 자기자신이라면
            if (newcon == this)
            {
                // 자기자신과 연결된 플러그 연결상태만 초기화
                ConnectedTextReset(connectedText);
                ConnectedTextReset(this);
            }
            else
            {
                // 연결할 플러그의 연결상태 확인
                // 자기자신과 연결되어있지 않으면 연결된 플러그와 자기자신의 연결상태 초기화
                if (newcon.connectedText != newcon)
                {
                    ConnectedTextReset(newcon.connectedText);
                    ConnectedTextReset(newcon);
                }

                // 자기자신과 연결된 플러그 연결상태 초기화
                ConnectedTextReset(connectedText);
                ConnectedTextReset(this);

                // 플러그 연결
                connectedText = newcon;
                // UI 적용, 이벤트가 발생하지 안도록
                outText.SetValueWithoutNotify(value);
                // newcon의 연결상태도 갱신
                if (!isConnected)
                {
                    char ch = char.Parse(inText);
                    int charToInt = ch - 'A';
                    StartCoroutine(connectedText.ConnectedTextChange(charToInt, true));
                }
            }
        }

        plugBoard.isChaing = false;
        yield return null;
    }

    /// <summary>
    /// 연결된 플로그를 자기자신으로 변경
    /// </summary>
    /// <param name="resetPlug">연결된 플로그를 변경할 플러그</param>
    private void ConnectedTextReset(Plug resetPlug)
    {
        resetPlug.connectedText = resetPlug;
        char ch = char.Parse(resetPlug.InText);
        int charToInt = ch - 'A';
        TMP_Dropdown dropdown = resetPlug.GetComponentInChildren<TMP_Dropdown>();
        dropdown.SetValueWithoutNotify(charToInt);
    }

}

using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Collections;
using System;

using TMP_Text = TMPro.TextMeshProUGUI;

public class Plug : MonoBehaviour
{
    #region Open_Private_Fields
    [Tooltip("아웃풋 선택 드랍다운")]
    [SerializeField]
    private TMP_Dropdown outText;
    [Tooltip("인풋 확인")]
    [SerializeField]
    private char inText;
    [Tooltip("연결된 Plug")]
    [SerializeField]
    private Plug connectedText;
    [SerializeField]
    private PlugBoard plugBoard;
    #endregion

    #region Property_Fields
    public char InText
    {
        get
        {
            return inText;
        }
    }
    public char OutText
    {
        get
        {
            string text = outText.options[outText.value].text;
            char returnText = char.Parse(text);
            return returnText;
        }
    }
    #endregion

    #region Custom_Methods
    /// <summary>
    ///  플러그 초기화 메서드
    /// </summary>
    /// <param name="inText">플러그의 초기화할 알파벳</param>
    public void Init(char inText)
    {

        // 인풋 텍스트 초기화
        this.inText = inText;
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
        // 지정한 이벤트가 호출되지 않음, 벨류값도 같이 바뀌게 됨
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
            char text = char.Parse(outText.options[value].text);
            connectedText = plugBoard.GetPlug(text);
            outText.SetValueWithoutNotify(value);

            if (!isConnected)
            {
                int charToInt = inText - 'A';
                StartCoroutine(connectedText.ConnectedTextChange(charToInt, true));
            }
        }
        else
        {
            // 연결할 플러그 획득
            char text = char.Parse(outText.options[value].text);
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
                    int charToInt = inText - 'A';
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
        char ch = resetPlug.InText;
        int charToInt = ch - 'A';
        TMP_Dropdown dropdown = resetPlug.GetComponentInChildren<TMP_Dropdown>();
        dropdown.SetValueWithoutNotify(charToInt);
    }
    #endregion

}

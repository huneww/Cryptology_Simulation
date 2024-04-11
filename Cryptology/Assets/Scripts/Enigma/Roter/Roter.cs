using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Roter : MonoBehaviour
{
    [SerializeField]
    [Range(1, 26)]
    [Tooltip("라쳇 위치, 다음 로터가 이동될 위치")]
    private int ratchetTrigger = 1;
    [SerializeField]
    private Button ratchetTriggerUpBtn;
    [SerializeField]
    private Button ratchetTriggerDownBtn;
    [SerializeField]
    private TextMeshProUGUI triggerText;

    [SerializeField]
    [Tooltip("현재 라쳇의 위치")]
    private int currentRatchet = 1;
    public int CurrentRatchet
    {
        get
        {
            return currentRatchet;
        }
    }

    [SerializeField]
    private Button ratchetUpBtn;
    [SerializeField]
    private Button ratchetDownBtn;
    [SerializeField]
    private TextMeshProUGUI currentText;

    [SerializeField]
    [Tooltip("알파벳 연결 리시트")]
    private List<char> roterConnectList = new List<char>();

    private void Awake()
    {
        UISetting();
    }

    private void UISetting()
    {
        triggerText.text = ratchetTrigger.ToString();
        ratchetTriggerUpBtn.onClick.AddListener(
            () =>
            {
                ratchetTrigger++;
                ratchetTrigger %= 26;
                triggerText.text = ratchetTrigger.ToString();
            });
        ratchetTriggerDownBtn.onClick.AddListener(
            () =>
            {
                if (ratchetTrigger == 1)
                {
                    ratchetTrigger = 26;
                }
                else
                {
                    ratchetTrigger--;
                }
                triggerText.text = ratchetTrigger.ToString();
            });

        ratchetUpBtn.onClick.AddListener(
            () =>
            {
                currentRatchet++;
                currentRatchet %= 26;
                currentText.text = currentRatchet.ToString();
                RatchetButtonEvent(true);
            });
        ratchetDownBtn.onClick.AddListener(
            () =>
            {
                currentRatchet--;
                currentRatchet = currentRatchet <= 0 ? 26 : currentRatchet;
                currentText.text = currentRatchet.ToString();
                RatchetButtonEvent(false);
            });
    }

    private void RatchetButtonEvent(bool isUp)
    {
        if (isUp)
        {
            // 마지막 값 저장
            char temporay = roterConnectList[roterConnectList.Count - 1];
            // 리스트에서 마지막 값 제거
            roterConnectList.RemoveAt(roterConnectList.Count - 1);
            // 마지막 값을 리스트의 맨 앞에 추가
            roterConnectList.Insert(0, temporay);
        }
        else
        {
            // 첫번째 값 저장
            char temporay = roterConnectList[0];
            // 리스트에서 첫번째 값 제거
            roterConnectList.RemoveAt(0);
            // 첫번째 값을 리스트 맨 마지막에 추가
            roterConnectList.Add(temporay);
        }
    }

    /// <summary>
    /// 연결된 문자 반환 메서드
    /// </summary>
    /// <param name="name">입력 문자</param>
    /// <returns>연결될 문자</returns>
    public char GetConnect(char name)
    {
        // 연결된 문자 저장 변수
        char connect = new char();

        connect = roterConnectList[name - 'A'];

        // 연결된 문자 반환
        return connect;
    }

    /// <summary>
    /// 리시트의 연결된 문자 변경
    /// </summary>
    /// <returns>cuurentRatchet에서 ratchet을 나눈 나머지가 0이라면 다음 로터도 변경할지 확인</returns>
    public bool ChangeConnectList()
    {
        // 연결 갱신
        RatchetButtonEvent(true);

        // 현재 라쳇 위치 증가
        currentRatchet++;
        // 라쳇의 위치가 26보다 크다면
        currentRatchet = currentRatchet > 26 ? currentRatchet - 26 : currentRatchet;
        // UI 텍스트 적용
        currentText.text = currentRatchet.ToString();
        // 다음 로터도 이동시킬지 확인
        bool answer = ratchetTrigger % currentRatchet == 0;
        // 이동 여부 반환
        return answer;
    }

}

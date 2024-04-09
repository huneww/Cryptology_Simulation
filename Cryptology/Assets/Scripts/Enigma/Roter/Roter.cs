using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class RoterConnect
{
    // 입력 문자
    public char name;
    // 입력 문자와 연결된 문자
    public char connect;

    public RoterConnect(char name, char connect)
    {
        this.name = name;
        this.connect = connect;
    }

    // 깊은 복사(연결된 문자만)
    public char CopyConnect()
    {
        char clone = this.connect;

        return clone;
    }
}

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
    private List<RoterConnect> roterConnectList = new List<RoterConnect>();

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
            });
        ratchetDownBtn.onClick.AddListener(
            () =>
            {
                currentRatchet--;
                currentRatchet = currentRatchet <= 0 ? 26 : currentRatchet;
                currentText.text = currentRatchet.ToString();
            });
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

        // 리스트를 순회
        foreach (RoterConnect roter in roterConnectList)
        {
            // 동일한 이름을 가진 문자 획득
            if (roter.name == name)
            {
                // 동일한 문자의 연결된 문자를 저장
                connect = roter.connect;
                // 반복문 종료
                break;
            }
        }

        // 연결된 문자 반환
        return connect;
    }

    /// <summary>
    /// 리시트의 연결된 문자 변경
    /// </summary>
    /// <returns>cuurentRatchet에서 ratchet을 나눈 나머지가 0이라면 다음 로터도 변경할지 확인</returns>
    public bool ChangeConnectList()
    {
        // 리스트의 첫번째의 연결된 문자만 임시 저장
        char temporay = (char)roterConnectList[0].CopyConnect();
        // 리스트 순회
        for (int i = 1; i < roterConnectList.Count; i++)
        {
            // 리스트의 마지막이라면
            if (i == roterConnectList.Count - 1)
            {
                // 리스트의 첫번째를 저장
                roterConnectList[i].connect = temporay;
            }
            else
            {
                // 리스트 깊은 복사
                roterConnectList[i - 1].connect = (char)roterConnectList[i].CopyConnect();
            }
        }

        // 현재 라쳇 위치 증가
        currentRatchet++;
        // 라쳇의 위치가 26보다 크다면
        currentRatchet = currentRatchet > 26 ? currentRatchet - 26 : currentRatchet;
        // 다음 로터도 이동시킬지 확인
        bool answer = currentRatchet % ratchetTrigger == 0;
        // 이동 여부 반환
        return answer;
    }

}

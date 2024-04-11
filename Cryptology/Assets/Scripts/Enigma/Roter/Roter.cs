using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Roter : MonoBehaviour
{
    [SerializeField]
    [Range(1, 26)]
    [Tooltip("���� ��ġ, ���� ���Ͱ� �̵��� ��ġ")]
    private int ratchetTrigger = 1;
    [SerializeField]
    private Button ratchetTriggerUpBtn;
    [SerializeField]
    private Button ratchetTriggerDownBtn;
    [SerializeField]
    private TextMeshProUGUI triggerText;

    [SerializeField]
    [Tooltip("���� ������ ��ġ")]
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
    [Tooltip("���ĺ� ���� ����Ʈ")]
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
            // ������ �� ����
            char temporay = roterConnectList[roterConnectList.Count - 1];
            // ����Ʈ���� ������ �� ����
            roterConnectList.RemoveAt(roterConnectList.Count - 1);
            // ������ ���� ����Ʈ�� �� �տ� �߰�
            roterConnectList.Insert(0, temporay);
        }
        else
        {
            // ù��° �� ����
            char temporay = roterConnectList[0];
            // ����Ʈ���� ù��° �� ����
            roterConnectList.RemoveAt(0);
            // ù��° ���� ����Ʈ �� �������� �߰�
            roterConnectList.Add(temporay);
        }
    }

    /// <summary>
    /// ����� ���� ��ȯ �޼���
    /// </summary>
    /// <param name="name">�Է� ����</param>
    /// <returns>����� ����</returns>
    public char GetConnect(char name)
    {
        // ����� ���� ���� ����
        char connect = new char();

        connect = roterConnectList[name - 'A'];

        // ����� ���� ��ȯ
        return connect;
    }

    /// <summary>
    /// ����Ʈ�� ����� ���� ����
    /// </summary>
    /// <returns>cuurentRatchet���� ratchet�� ���� �������� 0�̶�� ���� ���͵� �������� Ȯ��</returns>
    public bool ChangeConnectList()
    {
        // ���� ����
        RatchetButtonEvent(true);

        // ���� ���� ��ġ ����
        currentRatchet++;
        // ������ ��ġ�� 26���� ũ�ٸ�
        currentRatchet = currentRatchet > 26 ? currentRatchet - 26 : currentRatchet;
        // UI �ؽ�Ʈ ����
        currentText.text = currentRatchet.ToString();
        // ���� ���͵� �̵���ų�� Ȯ��
        bool answer = ratchetTrigger % currentRatchet == 0;
        // �̵� ���� ��ȯ
        return answer;
    }

}

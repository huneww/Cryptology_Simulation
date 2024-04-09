using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class RoterConnect
{
    // �Է� ����
    public char name;
    // �Է� ���ڿ� ����� ����
    public char connect;

    public RoterConnect(char name, char connect)
    {
        this.name = name;
        this.connect = connect;
    }

    // ���� ����(����� ���ڸ�)
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
    /// ����� ���� ��ȯ �޼���
    /// </summary>
    /// <param name="name">�Է� ����</param>
    /// <returns>����� ����</returns>
    public char GetConnect(char name)
    {
        // ����� ���� ���� ����
        char connect = new char();

        // ����Ʈ�� ��ȸ
        foreach (RoterConnect roter in roterConnectList)
        {
            // ������ �̸��� ���� ���� ȹ��
            if (roter.name == name)
            {
                // ������ ������ ����� ���ڸ� ����
                connect = roter.connect;
                // �ݺ��� ����
                break;
            }
        }

        // ����� ���� ��ȯ
        return connect;
    }

    /// <summary>
    /// ����Ʈ�� ����� ���� ����
    /// </summary>
    /// <returns>cuurentRatchet���� ratchet�� ���� �������� 0�̶�� ���� ���͵� �������� Ȯ��</returns>
    public bool ChangeConnectList()
    {
        // ����Ʈ�� ù��°�� ����� ���ڸ� �ӽ� ����
        char temporay = (char)roterConnectList[0].CopyConnect();
        // ����Ʈ ��ȸ
        for (int i = 1; i < roterConnectList.Count; i++)
        {
            // ����Ʈ�� �������̶��
            if (i == roterConnectList.Count - 1)
            {
                // ����Ʈ�� ù��°�� ����
                roterConnectList[i].connect = temporay;
            }
            else
            {
                // ����Ʈ ���� ����
                roterConnectList[i - 1].connect = (char)roterConnectList[i].CopyConnect();
            }
        }

        // ���� ���� ��ġ ����
        currentRatchet++;
        // ������ ��ġ�� 26���� ũ�ٸ�
        currentRatchet = currentRatchet > 26 ? currentRatchet - 26 : currentRatchet;
        // ���� ���͵� �̵���ų�� Ȯ��
        bool answer = currentRatchet % ratchetTrigger == 0;
        // �̵� ���� ��ȯ
        return answer;
    }

}

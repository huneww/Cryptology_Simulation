using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : MonoBehaviour
{
    [SerializeField]
    [Tooltip("���ĺ� ���� ����Ʈ")]
    private List<RoterConnect> roterConnectList = new List<RoterConnect>();

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
}

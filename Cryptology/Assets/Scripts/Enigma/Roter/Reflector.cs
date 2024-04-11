using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : MonoBehaviour
{
    [SerializeField]
    [Tooltip("���ĺ� ���� ����Ʈ")]
    private List<char> roterConnectList = new List<char>();

    public char GetConnect(char name)
    {
        // ����� ���� ���� ����
        char connect = new char();

        connect = roterConnectList[name - 'A'];

        // ����� ���� ��ȯ
        return connect;
    }
}

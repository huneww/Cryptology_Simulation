using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : MonoBehaviour
{
    [SerializeField]
    [Tooltip("알파벳 연결 리시트")]
    private List<char> roterConnectList = new List<char>();

    public char GetConnect(char name)
    {
        // 연결된 문자 저장 변수
        char connect = new char();

        connect = roterConnectList[name - 'A'];

        // 연결된 문자 반환
        return connect;
    }
}

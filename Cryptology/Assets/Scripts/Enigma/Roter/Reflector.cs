using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : MonoBehaviour
{
    [SerializeField]
    [Tooltip("알파벳 연결 리시트")]
    private List<RoterConnect> roterConnectList = new List<RoterConnect>();

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
}

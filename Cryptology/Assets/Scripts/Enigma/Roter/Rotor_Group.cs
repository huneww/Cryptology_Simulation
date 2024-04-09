using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Rotor_Group : MonoBehaviour
{
    [SerializeField]
    private Roter[] roters;
    [SerializeField]
    private Reflector reflector;

    private List<Func<char, char>> roterAction = new List<Func<char, char>>();

    private void Awake()
    {
        RoterActionSetting();

        //RoterAction("a");
    }

    private void RoterActionSetting()
    {
        roters = GetComponentsInChildren<Roter>();
        reflector = GetComponentInChildren<Reflector>();

        // 입력시 로터 액션 추가
        for (int i = 0; i < roters.Length; i++)
        {
            roterAction.Add(roters[i].GetConnect);
        }
        //TODO: 반환판 추가
        roterAction.Add(reflector.GetConnect);
        for (int i = roters.Length - 1; i >= 0; i--)
        {
            roterAction.Add(roters[i].GetConnect);
        }

        //roterAction.Add(roters[0].GetConnect);
        //roterAction.Add(roters[1].GetConnect);
        //roterAction.Add(roters[2].GetConnect);

        //roterAction.Add(roters[2].GetConnect);
        //roterAction.Add(roters[1].GetConnect);
        //roterAction.Add(roters[0].GetConnect);
    }

    /// <summary>
    /// 로터 액션 실행
    /// </summary>
    /// <param name="input">입력 값</param>
    /// <returns></returns>
    public char RoterAction(string input)
    {
        // 입력값 대문자로 변경
        char connect = char.Parse(input.ToUpper());

        // 로터 액션 순회
        foreach (var action in roterAction)
        {
            connect = (char)(action?.Invoke(connect));
            Debug.Log(connect);
        }

        // 각 로터의 연결된 문자 변경
        // 특정 상황이 되면 다음 로터도 연결된 문자 변경
        if (roters[0].ChangeConnectList())
        {
            if (roters[1].ChangeConnectList())
            {
                roters[2].ChangeConnectList();
            }
        }

        return connect;
    }

}

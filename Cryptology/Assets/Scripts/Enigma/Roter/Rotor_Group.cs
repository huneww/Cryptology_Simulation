using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Rotor_Group : MonoBehaviour
{
    #region Open_Private_Fields
    [SerializeField]
    [Tooltip("로터")]
    private Roter[] roters;
    [SerializeField]
    [Tooltip("반환판")]
    private Reflector reflector;
    #endregion

    #region Private_Fields
    // 로터 액션을 저장할 델리게이트 리스트
    private List<Func<char, char>> roterAction = new List<Func<char, char>>();
    #endregion

    #region Custom_Methods
    /// <summary>
    /// 로터그룹 초기화 메서드
    /// </summary>
    public void Init()
    {
        // 자식객체에서 로터와 반환판 스크립트 획득
        roters = GetComponentsInChildren<Roter>(true);
        reflector = GetComponentInChildren<Reflector>(true);

        // 입력시 로터 액션 추가
        for (int i = 0; i < roters.Length; i++)
        {
            roterAction.Add(roters[i].GetConnect);
        }
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
    public char RoterAction(char input)
    {
        // 입력값 대문자로 변경
        char connect = input;
        // 로터 액션 순회
        foreach (var action in roterAction)
        {
            connect = (char)(action?.Invoke(connect));
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
    #endregion

}

using System;
using System.Collections.Generic;
using UnityEngine;

public class Roter_Group : MonoBehaviour
{
    [SerializeField]
    private Roter[] roters;

    private List<Func<char, char>> roterAction = new List<Func<char, char>>();

    private void Awake()
    {
        roterAction.Add(roters[0].GetConnect);
        roterAction.Add(roters[1].GetConnect);
        roterAction.Add(roters[2].GetConnect);
        //TODO: 반환판 추가
        roterAction.Add(roters[2].GetConnect);
        roterAction.Add(roters[1].GetConnect);
        roterAction.Add(roters[0].GetConnect);

        RoterAction("a");
    }

    public char RoterAction(string input)
    {
        char connect = char.Parse(input.ToUpper());
        foreach (var action in roterAction)
        {
            connect = (char)(action?.Invoke(connect));
            Debug.Log(connect);
        }
        return connect;
    }

}

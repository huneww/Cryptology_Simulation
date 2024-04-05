using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoterConnect
{
    public char name;
    public char connect;
}

public class Roter : MonoBehaviour
{
    [SerializeField]
    [Range(1, 26)]
    private int ratchet;
    [SerializeField]
    private List<RoterConnect> roterConnectList = new List<RoterConnect>();

    public char GetConnect(char name)
    {
        char connect = new char();

        foreach (RoterConnect roter in roterConnectList)
        {
            if (roter.name == name)
            {
                connect = roter.connect;
                break;
            }
        }

        return connect;
    }
}

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

        // �Է½� ���� �׼� �߰�
        for (int i = 0; i < roters.Length; i++)
        {
            roterAction.Add(roters[i].GetConnect);
        }
        //TODO: ��ȯ�� �߰�
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
    /// ���� �׼� ����
    /// </summary>
    /// <param name="input">�Է� ��</param>
    /// <returns></returns>
    public char RoterAction(string input)
    {
        // �Է°� �빮�ڷ� ����
        char connect = char.Parse(input.ToUpper());

        // ���� �׼� ��ȸ
        foreach (var action in roterAction)
        {
            connect = (char)(action?.Invoke(connect));
            Debug.Log(connect);
        }

        // �� ������ ����� ���� ����
        // Ư�� ��Ȳ�� �Ǹ� ���� ���͵� ����� ���� ����
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

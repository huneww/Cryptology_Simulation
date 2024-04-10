using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Rotor_Group : MonoBehaviour
{
    #region Open_Private_Fields
    [SerializeField]
    [Tooltip("����")]
    private Roter[] roters;
    [SerializeField]
    [Tooltip("��ȯ��")]
    private Reflector reflector;
    #endregion

    #region Private_Fields
    // ���� �׼��� ������ ��������Ʈ ����Ʈ
    private List<Func<char, char>> roterAction = new List<Func<char, char>>();
    #endregion

    #region Custom_Methods
    /// <summary>
    /// ���ͱ׷� �ʱ�ȭ �޼���
    /// </summary>
    public void Init()
    {
        // �ڽİ�ü���� ���Ϳ� ��ȯ�� ��ũ��Ʈ ȹ��
        roters = GetComponentsInChildren<Roter>(true);
        reflector = GetComponentInChildren<Reflector>(true);

        // �Է½� ���� �׼� �߰�
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
    /// ���� �׼� ����
    /// </summary>
    /// <param name="input">�Է� ��</param>
    /// <returns></returns>
    public char RoterAction(char input)
    {
        // �Է°� �빮�ڷ� ����
        char connect = input;
        // ���� �׼� ��ȸ
        foreach (var action in roterAction)
        {
            connect = (char)(action?.Invoke(connect));
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
    #endregion

}

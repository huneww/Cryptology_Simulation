using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PlugBoard : MonoBehaviour
{
    #region Open_Private_Fields
    [Tooltip("�÷��� ����Ʈ")]
    [SerializeField]
    private List<Plug> plugList = new List<Plug>();
    #endregion

    #region Public_Fields
    public bool isChaing = false;
    #endregion

    #region Custom_Methods
    /// <summary>
    /// �÷��� ���� �ʱ�ȭ �޼���
    /// </summary>
    public void Init()
    {
        // �ڽ����� �ִ� Plug�� ��� ȹ��
        Plug[] plugs = GetComponentsInChildren<Plug>(true);
        // ȹ���� Plug�� ��� ����Ʈ�� ����
        plugList = plugs.ToList<Plug>();

        // Plug �ʱ�ȭ
        char ch = 'A';
        foreach (Plug plug in plugList)
        {
            plug.Init(ch);
            ch++;
        }
    }

    /// <summary>
    /// text�� ��ǲ ������ ������ �ִ� Plug�� ��ȯ
    /// </summary>
    /// <param name="text">ȹ���� Plug�� ��ǲ ��</param>
    /// <returns>ã�� ���ϸ� null�� ��ȯ</returns>
    public Plug GetPlug(char text)
    {
        Plug returnPlug = null;

        foreach (Plug plug in plugList)
        {
            if (plug.InText == text)
            {
                returnPlug = plug;
            }
        }
        Debug.Log($"GetPlug.ReturnPlug.InText {returnPlug.InText}");
        Debug.Log($"GetPlug.ReturnPlug.OutText {returnPlug.OutText}");
        return returnPlug;
    }

    /// <summary>
    /// text�� ��ǲ ������ ������ �ִ� Plug�� ����� �� ��ȯ
    /// </summary>
    /// <param name="text">��ǲ ��</param>
    /// <returns></returns>
    public char GetText(char text)
    {
        char returnText = new char();

        foreach (Plug plug in plugList)
        {
            if (plug.InText == text)
            {
                returnText = plug.OutText;
            }
        }

        return returnText;
    }
    #endregion

}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PlugBoard : MonoBehaviour
{
    [Tooltip("�÷��� ����Ʈ")]
    [SerializeField]
    private List<Plug> plugList = new List<Plug>();

    public bool isChaing = false;

    private void Awake()
    {
        // �ʱ�ȭ
        Init();
    }

    private void Init()
    {
        // �ڽ����� �ִ� Plug�� ��� ȹ��
        Plug[] plugs = GetComponentsInChildren<Plug>();
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
    public Plug GetPlug(string text)
    {
        Plug returnPlug = null;

        foreach (Plug plug in plugList)
        {
            if (plug.InText == text)
            {
                returnPlug = plug;
            }
        }
        return returnPlug;
    }

    public string GetText(string text)
    {
        string returnText = string.Empty;

        foreach (Plug plug in plugList)
        {
            if (plug.InText == text)
            {
                returnText = plug.OutString;
            }
        }

        return returnText;
    }

}

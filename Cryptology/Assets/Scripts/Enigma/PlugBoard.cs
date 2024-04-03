using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PlugBoard : MonoBehaviour
{
    [Tooltip("플러그 리스트")]
    [SerializeField]
    private List<Plug> plugList = new List<Plug>();

    public bool isChaing = false;

    private void Awake()
    {
        // 초기화
        Init();
    }

    private void Init()
    {
        // 자식으로 있는 Plug를 모두 획득
        Plug[] plugs = GetComponentsInChildren<Plug>();
        // 획득한 Plug를 모두 리스트에 저장
        plugList = plugs.ToList<Plug>();

        // Plug 초기화
        char ch = 'A';
        foreach (Plug plug in plugList)
        {
            plug.Init(ch);
            ch++;
        }
    }

    /// <summary>
    /// text를 인풋 값으로 가지고 있는 Plug를 반환
    /// </summary>
    /// <param name="text">획득할 Plug의 인풋 값</param>
    /// <returns>찾지 못하면 null을 반환</returns>
    public Plug GetPlug(string text)
    {
        foreach (Plug plug in plugList)
        {
            if (plug.InText == text)
            {
                return plug;
            }
        }
        return null;
    }
}

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
    [Tooltip("플러그 리스트")]
    [SerializeField]
    private List<Plug> plugList = new List<Plug>();
    #endregion

    #region Public_Fields
    public bool isChaing = false;
    #endregion

    #region Custom_Methods
    /// <summary>
    /// 플러그 보드 초기화 메서드
    /// </summary>
    public void Init()
    {
        // 자식으로 있는 Plug를 모두 획득
        Plug[] plugs = GetComponentsInChildren<Plug>(true);
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
    /// text를 인풋 값으로 가지고 있는 Plug의 연결된 값 반환
    /// </summary>
    /// <param name="text">인풋 값</param>
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

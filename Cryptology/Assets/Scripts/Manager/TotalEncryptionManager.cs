using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class TotalEncryptionManager : MonoBehaviour
{
    #region Open_Pirvate_Fields
    [SerializeField]
    [Tooltip("암호화 방법")]
    private GameObject[] encryptionWay;
    [SerializeField]
    [Tooltip("암호화 방법 드랍다운")]
    private TMP_Dropdown dropDown;
    [SerializeField]
    [Tooltip("치환표의 알파벳 원본")]
    private List<WordNode> originalWord = new List<WordNode>();
    #endregion

    #region Monobehaviour_Callbacks
    private void Awake()
    {
        // 게임씬 사이즈 조정
        Screen.SetResolution(1280, 720, false);
        UISetting();
        DropDownOptionAdd();
    }
    #endregion

    #region Custom_Methods
    private void DropDownOptionAdd()
    {
        // 드랍다운 옵션 전부 제거
        dropDown.ClearOptions();
        // 드랍다운 옵션에 추가할 리스트
        List<string> dropDownGroup = new List<string>();
        // 암호화 방법 순회
        foreach (var way in encryptionWay)
        {
            // '_'를 기준으로 암호화 방법 오브젝트의 이름을 나눔
            string[] str = way.name.Split("_");
            // 첫번쨰가 암호화의 이름이기 때문에 리스트에 추가
            dropDownGroup.Add(str[0]);
        }
        // 리스트를 드랍다운 옵션에 추가
        dropDown.AddOptions(dropDownGroup);
    }

    private void UISetting()
    {
        // 원본 알파벳 설정
        for (int i = 0; i < originalWord.Count; i++)
        {
            originalWord[i].Word = Word.a + i;
        }

        // 드랍다운 이벤트 추가
        dropDown.onValueChanged.AddListener(
        (value) =>
        {
            // 암호화 방법을 전부 순회
            foreach (var way in encryptionWay)
            {
                // 방법 오브젝트 비활성화
                way.SetActive(false);
            }
            // 선택한 암호화 방법만 다시 활성화
            if (value < encryptionWay.Length)
            {
                encryptionWay[value].gameObject.SetActive(true);
            }
        });
    }
    #endregion

}

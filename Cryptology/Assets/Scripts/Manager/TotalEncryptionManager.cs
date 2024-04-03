using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class TotalEncryptionManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] encryptionWay;
    [SerializeField]
    private TMP_Dropdown dropDown;
    [SerializeField]
    private List<WordNode> originalWord = new List<WordNode>();

    private void Awake()
    {
        UISetting();
        DropDownOptionAdd();
    }

    private void DropDownOptionAdd()
    {
        dropDown.ClearOptions();
        List<string> dropDownGroup = new List<string>();
        foreach (var way in encryptionWay)
        {
            string[] str = way.name.Split("_");
            dropDownGroup.Add(str[0]);
        }
        dropDown.AddOptions(dropDownGroup);
    }

    private void UISetting()
    {
        for (int i = 0; i < originalWord.Count; i++)
        {
            originalWord[i].Word = Word.a + i;
        }

        dropDown.onValueChanged.AddListener(
        (value) =>
        {
            foreach (var way in encryptionWay)
            {
                way.SetActive(false);
            }
            if (value < encryptionWay.Length)
            {
                encryptionWay[value].gameObject.SetActive(true);
            }
        });
    }

}

using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class TotalEncryptionManager : MonoBehaviour
{
    #region Open_Pirvate_Fields
    [SerializeField]
    [Tooltip("��ȣȭ ���")]
    private GameObject[] encryptionWay;
    [SerializeField]
    [Tooltip("��ȣȭ ��� ����ٿ�")]
    private TMP_Dropdown dropDown;
    [SerializeField]
    [Tooltip("ġȯǥ�� ���ĺ� ����")]
    private List<WordNode> originalWord = new List<WordNode>();
    #endregion

    #region Monobehaviour_Callbacks
    private void Awake()
    {
        // ���Ӿ� ������ ����
        Screen.SetResolution(1280, 720, false);
        UISetting();
        DropDownOptionAdd();
    }
    #endregion

    #region Custom_Methods
    private void DropDownOptionAdd()
    {
        // ����ٿ� �ɼ� ���� ����
        dropDown.ClearOptions();
        // ����ٿ� �ɼǿ� �߰��� ����Ʈ
        List<string> dropDownGroup = new List<string>();
        // ��ȣȭ ��� ��ȸ
        foreach (var way in encryptionWay)
        {
            // '_'�� �������� ��ȣȭ ��� ������Ʈ�� �̸��� ����
            string[] str = way.name.Split("_");
            // ù������ ��ȣȭ�� �̸��̱� ������ ����Ʈ�� �߰�
            dropDownGroup.Add(str[0]);
        }
        // ����Ʈ�� ����ٿ� �ɼǿ� �߰�
        dropDown.AddOptions(dropDownGroup);
    }

    private void UISetting()
    {
        // ���� ���ĺ� ����
        for (int i = 0; i < originalWord.Count; i++)
        {
            originalWord[i].Word = Word.a + i;
        }

        // ����ٿ� �̺�Ʈ �߰�
        dropDown.onValueChanged.AddListener(
        (value) =>
        {
            // ��ȣȭ ����� ���� ��ȸ
            foreach (var way in encryptionWay)
            {
                // ��� ������Ʈ ��Ȱ��ȭ
                way.SetActive(false);
            }
            // ������ ��ȣȭ ����� �ٽ� Ȱ��ȭ
            if (value < encryptionWay.Length)
            {
                encryptionWay[value].gameObject.SetActive(true);
            }
        });
    }
    #endregion

}

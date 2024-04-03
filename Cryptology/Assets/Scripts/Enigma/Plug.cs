using UnityEngine;
using TMPro;
using System.Collections.Generic;
using TMP_Text = TMPro.TextMeshProUGUI;
using Unity.VisualScripting;
using System.Collections;
using System;

public class Plug : MonoBehaviour
{
    [Tooltip("�ƿ�ǲ ���� ����ٿ�")]
    [SerializeField]
    private TMP_Dropdown outText;
    [Tooltip("��ǲ Ȯ��")]
    [SerializeField]
    private string inText;
    [Tooltip("����� Plug")]
    [SerializeField]
    private Plug connectedText;
    public string InText
    {
        get
        {
            return inText;
        }
    }
    [SerializeField]
    PlugBoard plugBoard;


    public void Init(char inText)
    {
        // �÷��� ���带 ȹ��
        plugBoard = GetComponentInParent<PlugBoard>();

        // ��ǲ �ؽ�Ʈ �ʱ�ȭ
        this.inText = inText.ToString();
        // �ʱ� ���� Plug�� �ڱ��ڽ����� �ʱ�ȭ
        connectedText = this;

        // ����ٿ� �޴� �ɼ� ���� ����
        outText.ClearOptions();

        // ����ٿ� �޴��� �߰��� ����Ʈ
        List<string> words = new List<string>();
        for (int i = 'A'; i <= 'Z'; i++)
        {
            char intToChar = (char)i;
            words.Add(intToChar.ToString());
        }
        outText.AddOptions(words);

        // ��ǲ �ؽ�Ʈ �ʱ�ȭ
        TMP_Text text = GetComponentInChildren<TMP_Text>();
        text.text = inText.ToString();

        // ����ٿ� ������ ����
        outText.value = inText - 'A';
        // ����ٿ� ������ ���Ӿ��� ����
        // �޼ҵ� ȣ������ ������ �������� �̹ݿ�
        outText.RefreshShownValue();
        // ����ٿ� ������ ����� �̺�Ʈ �߰�
        outText.onValueChanged.AddListener(
            (value) =>
            {
                if (!plugBoard.isChaing)
                {
                    plugBoard.isChaing = true;
                    StartCoroutine(ConnectedTextChange(value));
                }
            });
    }

    /// <summary>
    /// ����� �÷��� ����
    /// </summary>
    /// <param name="value">������ �÷ΰ��� ����ٿ� ����</param>
    /// <param name="isConnected">����Ǿ��ִ��� Ȯ��, false = ����Ǿ���������</param>
    public IEnumerator ConnectedTextChange(int value, bool isConnected = false)
    {
        string text = outText.options[value].text;
        connectedText = plugBoard.GetPlug(text);
        outText.value = value;
        outText.RefreshShownValue();

        if (!isConnected)
        {
            char ch = char.Parse(inText);
            int charToInt = ch - 'A';
            StartCoroutine(connectedText.ConnectedTextChange(charToInt, true));
        }

        // ���� �����÷ξ� �߻�
        //if (connectedText == this)
        //{
        //    // ������ �÷����� ��ǲ�� ȹ��
        //    string text = outText.options[value].text;
        //    // �÷��� ����
        //    connectedText = plugBoard.GetPlug(text);
        //    if (connectedText == null)
        //    {
        //        Debug.LogError("ConnectedText is Null");
        //        yield break;
        //    }

        //    // ����Ǿ��ִ��� ������
        //    if (!isConnected)
        //    {
        //        // ���� ��ũ��Ʈ�� ��ǲ�� ���ڷ� ��ȯ
        //        char ch = char.Parse(inText);
        //        int charToInt = ch - 'A';
        //        // ����� �÷����� ����ٿ�޴� ȹ��
        //        TMP_Dropdown drop = connectedText.GetComponentInChildren<TMP_Dropdown>();
        //        // ����ٿ��� ������ ����
        //        drop.value = charToInt;
        //        // ���Ӿ��� ����
        //        drop.RefreshShownValue();
        //        // ��ǲ���� ���ڿ� ����Լ����� �����ָ鼭 �޼��� ȣ��
        //        StartCoroutine(connectedText.ConnectedTextChange(charToInt, true));
        //    }
        //}
        //else
        //{
        //    if (!isConnected)
        //    {
        //        // ������ ������ִ� �÷ΰ��� ������ ����
        //        char ch = char.Parse(connectedText.InText);
        //        int charToInt = ch - 'A';
        //        TMP_Dropdown dropdown2 = connectedText.GetComponentInChildren<TMP_Dropdown>();
        //        dropdown2.value = charToInt;
        //        dropdown2.RefreshShownValue();
        //        StartCoroutine(connectedText.ConnectedTextChange(charToInt, true));
        //    }

        //    // ������ �÷����� ��ǲ�� ȹ��
        //    string text = outText.options[value].text;
        //    // �÷��� ����
        //    connectedText = plugBoard.GetPlug(text);
        //    TMP_Dropdown dropdown = connectedText.GetComponentInChildren<TMP_Dropdown>();
        //    dropdown.value = value;
        //    dropdown.RefreshShownValue();

        //    // ����Ǿ��ִ��� ������
        //    if (!isConnected)
        //    {
        //        char ch = char.Parse(inText);
        //        int charToInt = ch - 'A';
        //        // ����� �÷����� ���� ����
        //        StartCoroutine(connectedText.ConnectedTextChange(charToInt, true));
        //    }
        //}
        Debug.Log(gameObject.name);
        plugBoard.isChaing = false;
        yield return null;
    }

}

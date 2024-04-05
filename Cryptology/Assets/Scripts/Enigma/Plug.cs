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

    [HideInInspector]
    public string OutString
    {
        get
        {
            return outText.options[outText.value].text;
        }
    }

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
        // outText.RefreshShownValue(); // �� �޼���� �ϸ� ������ �̺�Ʈ�� ȣ���
        // ������ �̺�Ʈ�� ȣ����� ����
        outText.SetValueWithoutNotify(outText.value);
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
        if (connectedText == this)
        {
            string text = outText.options[value].text;
            connectedText = plugBoard.GetPlug(text);
            outText.SetValueWithoutNotify(value);

            if (!isConnected)
            {
                char ch = char.Parse(inText);
                int charToInt = ch - 'A';
                StartCoroutine(connectedText.ConnectedTextChange(charToInt, true));
            }
        }
        else
        {
            // ������ �÷��� ȹ��
            string text = outText.options[value].text;
            Plug newcon = plugBoard.GetPlug(text);

            // ���Ӱ� ������ �÷αװ� �ڱ��ڽ��̶��
            if (newcon == this)
            {
                // �ڱ��ڽŰ� ����� �÷��� ������¸� �ʱ�ȭ
                ConnectedTextReset(connectedText);
                ConnectedTextReset(this);
            }
            else
            {
                // ������ �÷����� ������� Ȯ��
                // �ڱ��ڽŰ� ����Ǿ����� ������ ����� �÷��׿� �ڱ��ڽ��� ������� �ʱ�ȭ
                if (newcon.connectedText != newcon)
                {
                    ConnectedTextReset(newcon.connectedText);
                    ConnectedTextReset(newcon);
                }

                // �ڱ��ڽŰ� ����� �÷��� ������� �ʱ�ȭ
                ConnectedTextReset(connectedText);
                ConnectedTextReset(this);

                // �÷��� ����
                connectedText = newcon;
                // UI ����, �̺�Ʈ�� �߻����� �ȵ���
                outText.SetValueWithoutNotify(value);
                // newcon�� ������µ� ����
                if (!isConnected)
                {
                    char ch = char.Parse(inText);
                    int charToInt = ch - 'A';
                    StartCoroutine(connectedText.ConnectedTextChange(charToInt, true));
                }
            }
        }

        plugBoard.isChaing = false;
        yield return null;
    }

    /// <summary>
    /// ����� �÷α׸� �ڱ��ڽ����� ����
    /// </summary>
    /// <param name="resetPlug">����� �÷α׸� ������ �÷���</param>
    private void ConnectedTextReset(Plug resetPlug)
    {
        resetPlug.connectedText = resetPlug;
        char ch = char.Parse(resetPlug.InText);
        int charToInt = ch - 'A';
        TMP_Dropdown dropdown = resetPlug.GetComponentInChildren<TMP_Dropdown>();
        dropdown.SetValueWithoutNotify(charToInt);
    }

}

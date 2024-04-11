using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Collections;
using System;

using TMP_Text = TMPro.TextMeshProUGUI;

public class Plug : MonoBehaviour
{
    #region Open_Private_Fields
    [Tooltip("�ƿ�ǲ ���� ����ٿ�")]
    [SerializeField]
    private TMP_Dropdown outText;
    [Tooltip("��ǲ Ȯ��")]
    [SerializeField]
    private char inText;
    [Tooltip("����� Plug")]
    [SerializeField]
    private Plug connectedText;
    [SerializeField]
    private PlugBoard plugBoard;
    #endregion

    #region Property_Fields
    public char InText
    {
        get
        {
            return inText;
        }
    }
    public char OutText
    {
        get
        {
            string text = outText.options[outText.value].text;
            char returnText = char.Parse(text);
            return returnText;
        }
    }
    #endregion

    #region Custom_Methods
    /// <summary>
    ///  �÷��� �ʱ�ȭ �޼���
    /// </summary>
    /// <param name="inText">�÷����� �ʱ�ȭ�� ���ĺ�</param>
    public void Init(char inText)
    {

        // ��ǲ �ؽ�Ʈ �ʱ�ȭ
        this.inText = inText;
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
        // ������ �̺�Ʈ�� ȣ����� ����, �������� ���� �ٲ�� ��
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
            char text = char.Parse(outText.options[value].text);
            connectedText = plugBoard.GetPlug(text);
            outText.SetValueWithoutNotify(value);

            if (!isConnected)
            {
                int charToInt = inText - 'A';
                StartCoroutine(connectedText.ConnectedTextChange(charToInt, true));
            }
        }
        else
        {
            // ������ �÷��� ȹ��
            char text = char.Parse(outText.options[value].text);
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
                    int charToInt = inText - 'A';
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
        char ch = resetPlug.InText;
        int charToInt = ch - 'A';
        TMP_Dropdown dropdown = resetPlug.GetComponentInChildren<TMP_Dropdown>();
        dropdown.SetValueWithoutNotify(charToInt);
    }
    #endregion

}

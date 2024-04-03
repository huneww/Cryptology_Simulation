using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
using Unity.VisualScripting;

public class Vigenere_Sample : MonoBehaviour
{
    public Vigenere_Encryption encryption;

    #region Open_Private_Fields
    [SerializeField]
    private TextMeshProUGUI sampleText;
    [SerializeField]
    private TextMeshProUGUI sampleKeyText;
    [SerializeField]
    private TextMeshProUGUI plusTextString;
    [SerializeField]
    private TextMeshProUGUI plusTextInt;
    [SerializeField]
    private TextMeshProUGUI encryptionText;
    #endregion

    #region MonoBehaviour_Callbacks
    private void Awake()
    {
        encryption.sampleTextUpdate = SampleTextUpdate;
    }
    #endregion

    #region Custom_Methods
    /// <summary>
    /// ���� �ؽ�Ʈ ������Ʈ
    /// </summary>
    /// <param name="keyText">Ű ��</param>
    public void SampleTextUpdate(string keyText)
    {
        Debug.Log("SampleTextUpdate");
        StringBuilder sb = new StringBuilder();
        int[] keyTextValue = new int[keyText.Length];
        int[] sampleTextValue = new int[sampleText.text.Length];

        string lowerText = sampleText.text.ToLower();

        // ���� �ؽ�Ʈ ���ĺ� ���� ȹ��
        for (int i = 0; i < sampleTextValue.Length; i++)
        {
            int charToInt = (int)lowerText[i] - 97;
            sampleTextValue[i] = charToInt + 1;
        }

        // Ű �ؽ�Ʈ sb�� �߰�
        for (int i = 0; i < keyText.Length; i++)
        {
            if (i == keyText.Length - 1)
            {
                sb.AppendLine($"{keyText[i]}");
            }
            else
            {
                sb.Append($"{keyText[i]}, ");
            }
        }
        sb.AppendLine("Change to Number");

        // Ű�� �ҹ��ڷ� ����
        lowerText = keyText.ToLower();
        for (int i = 0; i < lowerText.Length; i++)
        {
            // �ؽ�Ʈ ���ڷ� ����
            // 97 ���� �ƽ�Ű�ڵ� ���̱� ����
            // a = 0, b = 1, c = 2,,,,,,
            int charToInt = (int)lowerText[i] - 97;
            keyTextValue[i] = charToInt + 1;
            if (i == lowerText.Length - 1)
            {
                sb.Append(keyTextValue[i]);
            }
            else
            {
                sb.Append(keyTextValue[i] + ", ");
            }
        }

        // ���� Ű �ؽ�Ʈ ����
        sampleKeyText.text = sb.ToString();
        sb.Clear();

        // �÷��� ���ڿ� �ؽ�Ʈ ����
        sb.AppendLine("S c h o o l");
        int length = 6;
        for (int i = 0; i < length; i++)
        {
            if (i == length - 1)
            {
                sb.AppendLine(keyText[i % keyText.Length].ToString());
            }
            else
            {
                sb.Append(keyText[i % keyText.Length] + " ");
            }
        }
        // �ؽ�Ʈ ����
        plusTextString.text = sb.ToString();
        sb.Clear();

        // �÷��� ���� �ؽ�Ʈ ����
        for (int i = 0; i < sampleTextValue.Length; i++)
        {
            if (i == sampleTextValue.Length - 1)
            {
                sb.AppendLine(sampleTextValue[i].ToString());
            }
            else
            {
                sb.Append(sampleTextValue[i].ToString() + ", ");
            }
        }
        for (int i = 0; i < sampleTextValue.Length; i++)
        {
            int index = i % keyTextValue.Length;
            if (i == sampleTextValue.Length - 1)
            {
                sb.Append(keyTextValue[index].ToString());
            }
            else
            {
                sb.Append(keyTextValue[index].ToString() + ", ");
            }
        }
        plusTextInt.text = sb.ToString();
        sb.Clear();

        // ��ȣȭ �ؽ�Ʈ ����
        for (int i = 0; i < sampleTextValue.Length; i++)
        {
            int index = i % keyTextValue.Length;
            int sum = sampleTextValue[i] + keyTextValue[index];
            sum %= 26;
            char ch = (char)(sum + 96);
            if (i == sampleTextValue.Length - 1)
            {
                sb.AppendLine(ch.ToString());
            }
            else
            {
                sb.Append(ch + ", ");
            }
        }
        for (int i = 0; i < sampleTextValue.Length; i++)
        {
            int index = i % keyTextValue.Length;
            // �� ���ĺ��� ��
            int sum = sampleTextValue[i] + keyTextValue[index];
            // ���ĺ��� 26�� �̹Ƿ� 26�� ���� ������ ���� �̿�
            sum %= 26;
            if (i == sampleTextValue.Length - 1)
            {
                sb.AppendLine(sum.ToString());
            }
            else
            {
                sb.Append(sum + ", ");
            }
        }

        encryptionText.text = sb.ToString();
    }
    #endregion
}

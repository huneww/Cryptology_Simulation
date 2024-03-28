using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class Vigenere_Sample : MonoBehaviour
{
    public Vigenere_Encryption encryption;

    [SerializeField]
    private TextMeshProUGUI sampleKeyText;
    [SerializeField]
    private TextMeshProUGUI plusTextString;
    [SerializeField]
    private TextMeshProUGUI plusTextInt;
    [SerializeField]
    private TextMeshProUGUI encryptionText;

    private void Start()
    {
        encryption.sampleTextUpdate = TextUpdate;
    }

    public void TextUpdate(string keyText, List<int> keyIndex)
    {
        StringBuilder sb = new StringBuilder();
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
        string lowerText = keyText.ToLower();
        foreach (char text in lowerText)
        {
            // �ؽ�Ʈ ���ڷ� ����
            // 97 ���� �ƽ�Ű�ڵ� ���̱� ����
            // a = 0, b = 1, c = 2,,,,,,
            int charToInt = (int)text - 97;
            sb.Append(keyIndex[charToInt] + ", ");
        }
        // ���� �ؽ�Ʈ ����
        sampleKeyText.text = sb.ToString();
        sb.Clear();

        // �÷��� ���ڿ� �ؽ�Ʈ ����
        sb.AppendLine("S c h o o l");
        int length = 6;
        for (int i = 0; i < length; i++)
        {
            if (i < length - 1)
            {
                sb.AppendLine(keyText[i % keyText.Length].ToString());
            }
            else
            {
                sb.Append(keyText[i % keyText.Length] + " ");
            }
        }
        sb.Clear();

        // �÷��� ���� �ؽ�Ʈ ����
        
    }

}

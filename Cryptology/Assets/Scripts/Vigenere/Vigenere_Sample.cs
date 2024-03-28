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

        // 키값 소문자로 변경
        string lowerText = keyText.ToLower();
        foreach (char text in lowerText)
        {
            // 텍스트 숫자로 변경
            // 97 빼기 아스키코드 값이기 때문
            // a = 0, b = 1, c = 2,,,,,,
            int charToInt = (int)text - 97;
            sb.Append(keyIndex[charToInt] + ", ");
        }
        // 예제 텍스트 변경
        sampleKeyText.text = sb.ToString();
        sb.Clear();

        // 플러스 문자열 텍스트 변경
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

        // 플러스 숫자 텍스트 변경
        
    }

}

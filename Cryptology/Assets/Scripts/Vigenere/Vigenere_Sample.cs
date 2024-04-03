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
    /// 샘플 텍스트 업데이트
    /// </summary>
    /// <param name="keyText">키 값</param>
    public void SampleTextUpdate(string keyText)
    {
        Debug.Log("SampleTextUpdate");
        StringBuilder sb = new StringBuilder();
        int[] keyTextValue = new int[keyText.Length];
        int[] sampleTextValue = new int[sampleText.text.Length];

        string lowerText = sampleText.text.ToLower();

        // 샘플 텍스트 알파벳 숫자 획득
        for (int i = 0; i < sampleTextValue.Length; i++)
        {
            int charToInt = (int)lowerText[i] - 97;
            sampleTextValue[i] = charToInt + 1;
        }

        // 키 텍스트 sb에 추가
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
        lowerText = keyText.ToLower();
        for (int i = 0; i < lowerText.Length; i++)
        {
            // 텍스트 숫자로 변경
            // 97 빼기 아스키코드 값이기 때문
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

        // 예제 키 텍스트 변경
        sampleKeyText.text = sb.ToString();
        sb.Clear();

        // 플러스 문자열 텍스트 변경
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
        // 텍스트 적용
        plusTextString.text = sb.ToString();
        sb.Clear();

        // 플러스 숫자 텍스트 변경
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

        // 암호화 텍스트 변경
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
            // 각 알파벳의 합
            int sum = sampleTextValue[i] + keyTextValue[index];
            // 알파벳이 26자 이므로 26을 나눈 나머지 값을 이용
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

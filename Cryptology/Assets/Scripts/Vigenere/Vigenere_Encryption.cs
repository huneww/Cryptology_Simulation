using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Vigenere_Encryption : MonoBehaviour, IEncryption
{
    string key = string.Empty;

    public string Encryption(string originalText, string vigenrereKey)
    {
        key = vigenrereKey;
        string returnString = Action(originalText);
        return returnString;
    }

    [Obsolete("이 메서드를 사용하게 되면 원하는 값이 반환되지 않습니다." +
        "대신 Encryption(string originalText, string vigenrerKey)" +
        " 메서드를 사용해 주세요.")]
    public string Encryption(string originalText, Dictionary<char, char> words = null)
    {
        return Action(originalText);
    }

    private string Action(string originalText)
    {
        // 암호화한 문자열 제작 StringBuilder
        StringBuilder sb = new StringBuilder();

        // 입력값 소문자로 변환
        string lowerText = originalText.ToLower();

        // 입력 값의 벨류값 저장
        int[] originalTextValue = new int[originalText.Length];

        // 입력값의 벨류값 저장
        for (int i = 0; i < originalText.Length; i++)
        {
            int charToInt = lowerText[i] - 96;
            originalTextValue[i] = charToInt;
        }

        // 암호화
        for (int i = 0; i < originalText.Length; i++)
        {
            // 텍스트가 알파벳인지 확인
            if (originalText[i] >= 'a' && originalText[i] <= 'z')
            {
                int index = i % key.Length;
                int sum = originalTextValue[i] + key[index];
                if (sum > 26)
                {
                    sum -= 26;
                }
                // 합값 알파벳으로 변경
                char ch = (char)(sum + 96);
                Debug.Log($"{sum}, {ch}");
                sb.Append(ch.ToString());
            }
            // 특수문자, 띄어쓰기 라면 그대로 추가
            else
            {
                sb.Append(originalText[i]);
            }
        }

        return sb.ToString();
    }
}

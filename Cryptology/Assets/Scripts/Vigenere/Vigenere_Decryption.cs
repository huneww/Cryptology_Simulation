using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Vigenere_Decryption : MonoBehaviour, IDecryption
{
    private string key = string.Empty;

    public string Decryption(string encryptionText, string vigenereKey)
    {
        key = vigenereKey;
        string returnString = Action(encryptionText);
        return returnString;
    }

    [Obsolete("이 메서드를 사용하게 되면 원하는 값이 반환되지 않습니다." +
    "대신 Decryption(string originalText, string vigenrerKey)" +
    " 메서드를 사용해 주세요.", false)]
    public string Decryption(string encryptionText, Dictionary<char, char> words = null)
    {
        return Action(encryptionText);
    }

    private string Action(string encryptionText)
    {
        // 입력 값의 벨류값 저장
        int[] encryptionTextValue = new int[encryptionText.Length];
        // 암호화한 문자열 제작 StringBuilder
        StringBuilder sb = new StringBuilder();

        // 입력 값 소문자로 변환
        string lowerText = encryptionText.ToLower();
        for (int i = 0; i < encryptionText.Length; i++)
        {
            int charToInt = lowerText[i] - 96;
            encryptionTextValue[i] = charToInt;
        }

        // 복호화
        for (int i = 0; i < encryptionText.Length; i++)
        {
            int index = i % key.Length;
            int minus = encryptionTextValue[i] - key[index];
            if (minus <= 0)
            {
                minus += 26;
            }
            char ch = (char)(minus + 96);
            sb.Append(ch.ToString().ToUpper());
        }

        return sb.ToString();
    }
}

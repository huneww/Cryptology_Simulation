using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MonoAlphabetic_Encryption : MonoBehaviour, IEncryption
{
    public string Encryption(string originalText, Dictionary<char, char> words)
    {
        StringBuilder sb = new StringBuilder();

        foreach (char text in originalText)
        {
            // 대문자
            if (text >= 65 && text <= 97)
            {
                // 소문자로 변경
                char lowerText = (char)(text + 32);
                // 소문자 값 획득
                char encryptionText = words[lowerText];
                // 획득한 소문자를 대문자로 변경
                encryptionText = (char)(encryptionText - 32);
                // sb에 추가
                sb.Append(encryptionText);
            }
            // 소문자
            else if (text >= 97 && text <= 122)
            {
                // 대문자로 변경
                char upperText = (char)(text - 32);
                sb.Append(words[text]);
            }
            // 그외의 특수 문자
            else
            {
                sb.Append(text);
            }
        }
        return sb.ToString();
    }
}

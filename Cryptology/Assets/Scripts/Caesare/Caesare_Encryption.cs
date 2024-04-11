using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Caesare_Encryption : MonoBehaviour, IEncryption
{
    public string Encryption(string originalText, Dictionary<char, char> words)
    {
        StringBuilder sb = new StringBuilder();

        foreach (char text in originalText)
        {
            // 대문자
            if (text >= 'A' && text <= 'Z')
            {
                // 소문자로 변경
                char lowerText = (char)(text + 32);
                // 소문자 값 획득
                char encryptionText = words[lowerText];
                // 대문자로 변경
                encryptionText = (char)(text + 32);
                // sb에 추가
                sb.Append(encryptionText);
            }
            // 소문자
            else if ( text >= 'a' && text <= 'z')
            {
                // 대문자로 변경
                char upperText = words[text];
                upperText = (char)(text + 32);
                sb.Append(upperText);
            }
            // 특수문자, 띄어쓰기
            else
            {
                sb.Append(text);
            }
        }

        return sb.ToString();
    }
}

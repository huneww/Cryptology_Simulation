using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MonoAlphabetic_Decryption : MonoBehaviour, IDecryption
{
    public string Decryption(string encryptionText, Dictionary<char, char> words)
    {
        StringBuilder sb = new StringBuilder();

        foreach (char text in encryptionText)
        {
            // 대문자
            if (text >= 65 && text <= 97)
            {
                // 소문자로 변경
                char lowerText = (char)(text + 32);
                // 소문자를 벨류값으로 가지고 있는 키를 획득
                char decryptionText = words.FirstOrDefault(value => value.Value == lowerText).Key;
                // 획득한 값을 대문자로 변경
                decryptionText = (char)(decryptionText - 32);
                // sb에 추가
                sb.Append(decryptionText);
            }
            // 소문자
            else if (text >= 97 && text <= 122)
            {
                // 소문자를 벨류값으로 가지고 있는 키를 획득
                char decryptionText = words.FirstOrDefault(value => value.Value == text).Key;
                // 대문자로 변경
                decryptionText = (char)(decryptionText - 32);
                // sb에 추가
                sb.Append(decryptionText);
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

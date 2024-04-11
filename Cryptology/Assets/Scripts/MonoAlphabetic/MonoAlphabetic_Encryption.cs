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
            // �빮��
            if (text >= 65 && text <= 97)
            {
                // �ҹ��ڷ� ����
                char lowerText = (char)(text + 32);
                // �ҹ��� �� ȹ��
                char encryptionText = words[lowerText];
                // ȹ���� �ҹ��ڸ� �빮�ڷ� ����
                encryptionText = (char)(encryptionText - 32);
                // sb�� �߰�
                sb.Append(encryptionText);
            }
            // �ҹ���
            else if (text >= 97 && text <= 122)
            {
                // �빮�ڷ� ����
                char upperText = (char)(text - 32);
                sb.Append(words[text]);
            }
            // �׿��� Ư�� ����
            else
            {
                sb.Append(text);
            }
        }
        return sb.ToString();
    }
}

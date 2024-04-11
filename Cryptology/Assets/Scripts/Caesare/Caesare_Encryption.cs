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
            // �빮��
            if (text >= 'A' && text <= 'Z')
            {
                // �ҹ��ڷ� ����
                char lowerText = (char)(text + 32);
                // �ҹ��� �� ȹ��
                char encryptionText = words[lowerText];
                // �빮�ڷ� ����
                encryptionText = (char)(text + 32);
                // sb�� �߰�
                sb.Append(encryptionText);
            }
            // �ҹ���
            else if ( text >= 'a' && text <= 'z')
            {
                // �빮�ڷ� ����
                char upperText = words[text];
                upperText = (char)(text + 32);
                sb.Append(upperText);
            }
            // Ư������, ����
            else
            {
                sb.Append(text);
            }
        }

        return sb.ToString();
    }
}

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
            // �빮��
            if (text >= 65 && text <= 97)
            {
                // �ҹ��ڷ� ����
                char lowerText = (char)(text + 32);
                // �ҹ��ڸ� ���������� ������ �ִ� Ű�� ȹ��
                char decryptionText = words.FirstOrDefault(value => value.Value == lowerText).Key;
                // ȹ���� ���� �빮�ڷ� ����
                decryptionText = (char)(decryptionText - 32);
                // sb�� �߰�
                sb.Append(decryptionText);
            }
            // �ҹ���
            else if (text >= 97 && text <= 122)
            {
                // �ҹ��ڸ� ���������� ������ �ִ� Ű�� ȹ��
                char decryptionText = words.FirstOrDefault(value => value.Value == text).Key;
                // �빮�ڷ� ����
                decryptionText = (char)(decryptionText - 32);
                // sb�� �߰�
                sb.Append(decryptionText);
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

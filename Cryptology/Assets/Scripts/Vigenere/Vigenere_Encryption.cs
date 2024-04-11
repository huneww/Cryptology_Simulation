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

    [Obsolete("�� �޼��带 ����ϰ� �Ǹ� ���ϴ� ���� ��ȯ���� �ʽ��ϴ�." +
        "��� Encryption(string originalText, string vigenrerKey)" +
        " �޼��带 ����� �ּ���.")]
    public string Encryption(string originalText, Dictionary<char, char> words = null)
    {
        return Action(originalText);
    }

    private string Action(string originalText)
    {
        // ��ȣȭ�� ���ڿ� ���� StringBuilder
        StringBuilder sb = new StringBuilder();

        // �Է°� �ҹ��ڷ� ��ȯ
        string lowerText = originalText.ToLower();

        // �Է� ���� ������ ����
        int[] originalTextValue = new int[originalText.Length];

        // �Է°��� ������ ����
        for (int i = 0; i < originalText.Length; i++)
        {
            int charToInt = lowerText[i] - 96;
            originalTextValue[i] = charToInt;
        }

        // ��ȣȭ
        for (int i = 0; i < originalText.Length; i++)
        {
            // �ؽ�Ʈ�� ���ĺ����� Ȯ��
            if (originalText[i] >= 'a' && originalText[i] <= 'z')
            {
                int index = i % key.Length;
                int sum = originalTextValue[i] + key[index];
                if (sum > 26)
                {
                    sum -= 26;
                }
                // �հ� ���ĺ����� ����
                char ch = (char)(sum + 96);
                Debug.Log($"{sum}, {ch}");
                sb.Append(ch.ToString());
            }
            // Ư������, ���� ��� �״�� �߰�
            else
            {
                sb.Append(originalText[i]);
            }
        }

        return sb.ToString();
    }
}

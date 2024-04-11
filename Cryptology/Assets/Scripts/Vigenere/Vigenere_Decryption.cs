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

    [Obsolete("�� �޼��带 ����ϰ� �Ǹ� ���ϴ� ���� ��ȯ���� �ʽ��ϴ�." +
    "��� Decryption(string originalText, string vigenrerKey)" +
    " �޼��带 ����� �ּ���.", false)]
    public string Decryption(string encryptionText, Dictionary<char, char> words = null)
    {
        return Action(encryptionText);
    }

    private string Action(string encryptionText)
    {
        // �Է� ���� ������ ����
        int[] encryptionTextValue = new int[encryptionText.Length];
        // ��ȣȭ�� ���ڿ� ���� StringBuilder
        StringBuilder sb = new StringBuilder();

        // �Է� �� �ҹ��ڷ� ��ȯ
        string lowerText = encryptionText.ToLower();
        for (int i = 0; i < encryptionText.Length; i++)
        {
            int charToInt = lowerText[i] - 96;
            encryptionTextValue[i] = charToInt;
        }

        // ��ȣȭ
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

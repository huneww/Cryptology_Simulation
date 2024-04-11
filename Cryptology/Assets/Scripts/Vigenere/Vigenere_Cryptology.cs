using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using System;

[RequireComponent(typeof(Vigenere_Encryption), typeof(Vigenere_Decryption))]
public class Vigenere_Cryptology : Cryptology_Base
{
    #region Open_Private_Fields
    [Space(10), Header("Vigenere Fields")]
    [SerializeField]
    [Tooltip("Ű �������� �ܾ� Ȥ�� ���ĺ� ����")]
    private TMP_InputField keyInputField;
    [SerializeField]
    [Tooltip("Ű �� ���� ��ȯ ��ư")]
    private Button keyRandomBtn;
    [SerializeField]
    [Min(3)]
    [Tooltip("�ܾ� ���� �ּ� ��")]
    private int wordLengthMin = 3;
    [SerializeField]
    [Min(3)]
    [Tooltip("�ܾ� ���� �ִ� ��")]
    private int wordLengthMax = 15;
    #endregion

    #region Private_Fields
    private Vigenere_Encryption encryption;
    private Vigenere_Decryption decryption;

    private string vigenereKey = "love";
    // Ű �� ���ڿ�
    private string VigenereKey
    {
        get
        {
            if (string.IsNullOrEmpty(vigenereKey))
            {
                vigenereKey = "love";
                return vigenereKey;
            }
            return vigenereKey;
        }
        set
        {
            vigenereKey = value;
            // �ؽ�Ʈ �� ����
            keyInputField.text = value;
            // ���� �ؽ�Ʈ ����
            SetSampleText();
            // Ű �� ����� ������ ����
            ChangeKeyTextValue(value);
        }
    }

    // ���� �ܾ� ���� ��ųʸ�
    // Ű �� : ������ ����
    private Dictionary<int, List<string>> words = new Dictionary<int, List<string>>();

    // Ű ���� ����
    private List<int> keyTextValue = new List<int>();

    #endregion

    // ���� �ؽ�Ʈ ������Ʈ �븮��
    public Action<string> sampleTextUpdate;

    #region MonoBehaviour_Callbacks
    private void OnEnable()
    {
        // ���� ���ĺ� ����Ʈ ��Ȱ��ȭ
        foreach (WordNode obj in wordList)
        {
            obj.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        // ���� ���ĺ� ����Ʈ Ȱ��ȭ
        foreach (WordNode obj in wordList)
        {
            obj.gameObject.SetActive(true);
        }
    }
    #endregion

    #region Custom_Methods
    public override void Init()
    {
        // �븮�� ����
        GetComponentInChildren<Vigenere_Sample>().Init();
        // ��ȣȭ ��� ȹ��
        GetCryptologyWay();
        // ���ܾ� ����
        LoadEnglishWords();
        // UI ����
        UISetting();
        // Ű ���� ������ ����
        ChangeKeyTextValue(VigenereKey);
        // ġȯǥ ����
        SetSampleText();
    }

    private void GetCryptologyWay()
    {
        encryption = GetComponent<Vigenere_Encryption>();
        decryption = GetComponent<Vigenere_Decryption>();
    }

    /// <summary>
    /// ���ܾ� ȹ��(Resources/Words.txt���� ���ܾ� ȹ��)
    /// </summary>
    private void LoadEnglishWords()
    {
        // �ؽ�Ʈ ������ �о ���ܾ� ����
        TextAsset text = Resources.Load("Words") as TextAsset;

        // �о�� �ؽ�Ʈ�� ���͹��ڷ� ������ �迭�� ����
        string[] dic = text.text.Split("\n").ToArray();

        // �ּ�, �ִ� ������ ���ڿ��� ����Ʈ�� �ʱ�ȭ
        for (int i = wordLengthMin; i <= wordLengthMax; i++)
        {
            words[i] = new List<string>();
        }

        // �о�� ���ܾ ��ȸ
        foreach (string word in dic)
        {
            // ���ܾ� ���̸� ȹ��
            int length = word.Length;
            // ���ܾ� ���̰� ª���� ū�� Ȯ��
            if (length >= wordLengthMin && length <= wordLengthMax)
            {
                // ��ųʸ��� ���̿� �´� ����Ʈ�� ����
                words[length].Add(word);
            }
        }
    }

    private void UISetting()
    {
        // ���� ���ĺ� ����
        for (int i = 0; i < 26; i++)
        {
            original.Add((char)('a' + i));
        }

        // ���� �Է� �Ϸ� �̺�Ʈ �߰�
        inputField.onEndEdit.AddListener(
            (text) =>
            {
                // ��ȣȭ
                if (isEncryption)
                {
                    //Encryption(text);
                    outputField.text = encryption.Encryption(text, VigenereKey);
                }
                // �ص�
                else
                {
                    //Decryption(text);
                    outputField.text = decryption.Decryption(text, VigenereKey);
                }
            });

        // ��ȣȭ ���� �ص����� Ȯ�� ��� �̺�Ʈ �߰�
        toggle.onValueChanged.AddListener(
            (value) =>
            {
                isEncryption = value;
                if (value)
                {
                    toggle.GetComponentInChildren<TextMeshProUGUI>().text = "Encryption";
                    // ������ �Է��� ���� �ִٸ� ��ȣȭ
                    if (!string.IsNullOrEmpty(inputField.text))
                    {
                        outputField.text = encryption.Encryption(inputField.text, VigenereKey);
                    }
                }
                else
                {
                    toggle.GetComponentInChildren<TextMeshProUGUI>().text = "Decryption";
                    // ������ �Է��� ���� �ִٸ� ��ȣȭ
                    if (!string.IsNullOrEmpty(inputField.text))
                    {
                        outputField.text = decryption.Decryption(inputField.text, VigenereKey);
                    }
                }
            });

        // �ʱ� ��ǲ �ؽ�Ʈ ����
        keyInputField.text = VigenereKey;

        // Ű �Է� �Ϸ� �̺�Ʈ �߰�
        keyInputField.onEndEdit.AddListener(
            (text) =>
            {
                // �ؽ�Ʈ ���� ������ Ȯ��
                if (string.IsNullOrEmpty(text))
                {
                    VigenereKey = "love";
                }
                else
                {
                    // �Է°����� �ҹ��ڷ� ����
                    VigenereKey = keyInputField.text.ToLower();
                }
            });

        // Ű �� ���� ��ư Ŭ�� �̺�Ʈ �߰�
        keyRandomBtn.onClick.AddListener(
            () =>
            {
                while (true)
                {
                    // ���ڿ� ���� ����
                    int length = UnityEngine.Random.Range(wordLengthMin, wordLengthMax);
                    // ������ �ܾ �ִ��� Ȯ��
                    if (words[length].Count > 0)
                    {
                        // ����Ʈ�� �ε��� ȹ��
                        int strIndex = UnityEngine.Random.Range(0, words[length].Count);
                        // ���ڿ� ����
                        string randomStr = words[length][strIndex];
                        // Ű �� ����
                        VigenereKey = randomStr;
                        // �ݺ��� ����
                        break;
                    }
                }
            });
    }

    /// <summary>
    /// ���� �ؽ�Ʈ ����
    /// </summary>
    public void SetSampleText()
    {
        // Vigenere_Sample�� SampleTextUpdate �޼���� ����Ǿ�����
        sampleTextUpdate(VigenereKey);

        // �Է°� �ִ��� Ȯ��
        if (!string.IsNullOrEmpty(inputField.text))
        {
            // �Է°� ����
            if (isEncryption)
            {
                outputField.text = encryption.Encryption(inputField.text, VigenereKey);
            }
            else
            {
                outputField.text = decryption.Decryption(inputField.text, VigenereKey);
            }
        }
    }

    /// <summary>
    /// ����� Ű���� ������ ����
    /// </summary>
    /// <param name="changeText">����� Ű ��</param>
    private void ChangeKeyTextValue(string changeText)
    {
        // ����Ʈ �ʱ�ȭ
        keyTextValue.Clear();
        // ����� Ű �� �ҹ��ڷ� ��ȯ
        string lowerText = changeText.ToLower();
        // Ű ���� �°� ������ ����Ʈ�� ����
        for (int i = 0; i < lowerText.Length; i++)
        {
            int charToInt = lowerText[i] - 96;
            keyTextValue.Add(charToInt);
        }
    }
    #endregion
}

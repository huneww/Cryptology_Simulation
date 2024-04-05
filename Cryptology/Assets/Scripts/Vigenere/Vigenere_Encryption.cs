using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;

public class Vigenere_Encryption : Encryption_Base
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
    [Tooltip("���� Ű�� �ؽ�Ʈ")]
    private TextMeshProUGUI sampleText;
    [Min(3)]
    [Tooltip("�ܾ� ���� �ּ� ��")]
    private int wordLengthMin = 3;
    [SerializeField]
    [Min(3)]
    [Tooltip("�ܾ� ���� �ִ� ��")]
    private int wordLengthMax = 15;
    #endregion

    #region Private_Fields
    // Ű �� ���ڿ�
    private string VigenereKey
    {
        get
        {
            if (!PlayerPrefs.HasKey("VigenereKey"))
                return "love";
            return PlayerPrefs.GetString("VigenereKey");
        }
        set
        {
            PlayerPrefs.SetString("VigenereKey", value);
            // �ؽ�Ʈ �� ����
            keyInputField.text = value;
            // ġȯǥ ����
            SetEncryptionWord();
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
    private void Awake()
    {
        LoadEnglishWords();
    }

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

    private void Start()
    {
        // UI ����
        UISetting();
        base.UISetting();
        ChangeKeyTextValue(VigenereKey);
        // ġȯǥ ����
        SetEncryptionWord();
    }

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
    private new void UISetting()
    {
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
                    // �Է°����� ����
                    VigenereKey = keyInputField.text;
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
    /// UI ġȯǥ ����
    /// </summary>
    public override void SetEncryptionWord()
    {
        // Vigenere_Sample�� SampleTextUpdate �޼���� ����Ǿ�����
        sampleTextUpdate(VigenereKey);

        // �Է°� �ִ��� Ȯ��
        if (!string.IsNullOrEmpty(inputField.text))
        {
            // �Է°� ����
            if (isEncryption)
            {
                Encryption(inputField.text);
            }
            else
            {
                Decryption(inputField.text);
            }
        }
    }

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

    public override void Encryption(string originalText)
    {
        Debug.Log("override Encryption");
        // �Է� ���� ������ ����
        int[] originalTextValue = new int[originalText.Length];
        // ��ȣȭ�� ���ڿ� ���� StringBuilder
        StringBuilder sb = new StringBuilder();

        // �Է°� �ҹ��ڷ� ��ȯ
        string lowerText = originalText.ToLower();

        // �Է°��� ������ ����
        for (int i = 0; i < originalText.Length; i++)
        {
            int charToInt = lowerText[i] - 96;
            originalTextValue[i] = charToInt;
        }

        // ��ȣȭ
        for (int i = 0; i < originalText.Length; i++)
        {
            int index = i % VigenereKey.Length;
            int sum = originalTextValue[i] + keyTextValue[index];
            if (sum > 26)
            {
                sum -= 26;
            }
            // �հ� ���ĺ����� ����
            char ch = (char)(sum + 96);
            Debug.Log($"{sum}, {ch}");
            sb.Append(ch.ToString());
        }

        // �ƿ�ǲ �ʵ��� �ؽ�Ʈ ����
        outputField.text = sb.ToString();

    }

    public override void Decryption(string encryptionText)
    {
        Debug.Log("override Decryption");
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
            int index = i % VigenereKey.Length;
            Debug.Log(index);
            int minus = encryptionTextValue[i] - keyTextValue[index];
            if (minus <= 0)
            {
                minus += 26;
            }
            char ch = (char)(minus + 96);
            sb.Append(ch.ToString());
        }

        outputField.text = sb.ToString();

    }
    #endregion
}

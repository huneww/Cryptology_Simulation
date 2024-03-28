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
    private string vigenereKey
    {
        get
        {
            if (!PlayerPrefs.HasKey("vigenereKey"))
                return "Love";
            return PlayerPrefs.GetString(vigenereKey);
        }
        set
        {
            PlayerPrefs.SetString(vigenereKey, value);
            // �ؽ�Ʈ �� ����
            keyInputField.text = value;
            // ġȯǥ ����
            SetEncryptionWord();
        }
    }

    // Ű �� ���ڿ� ���� ���� ����
    private List<int> keyIndex = new List<int>();

    // ���� �ܾ� ���� ��ųʸ�
    private Dictionary<int, List<string>> words = new Dictionary<int, List<string>>();

    #endregion

    // ���� �ؽ�Ʈ ������Ʈ �븮��
    public Action<string, List<int>> sampleTextUpdate;

    #region MonoBehaviour_Callbacks
    private void Awake()
    {
        // �ؽ�Ʈ ������ �о ���ܾ� ����
        TextAsset text = Resources.Load("Words") as TextAsset;
        // Ű ���� ����
        for (int i = 1; i <= 26; i++)
        {
            keyIndex.Add(i);
        }

        string[] dic = text.text.Split("\n").ToArray();

        for (int i = wordLengthMin; i <= wordLengthMax; i++)
        {
            words[i] = new List<string>();
        }

        foreach (string word in dic)
        {
            int length = word.Length;
            if (length > wordLengthMin && length <= wordLengthMax)
            {
                words[length].Add(word);
            }
        }
    }

    private void Start()
    {
        // UI ����
        UISetting();
        base.UISetting();
    }

    private void OnEnable()
    {
        // ġȯǥ ����
        SetEncryptionWord();
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
        keyInputField.text = vigenereKey;

        // Ű �Է� �Ϸ� �̺�Ʈ �߰�
        keyInputField.onEndEdit.AddListener(
            (text) =>
            {
                // �ؽ�Ʈ ���� ������ Ȯ��
                if (string.IsNullOrEmpty(text))
                {
                    vigenereKey = "Love";
                }
                else
                {
                    // �Է°����� ����
                    vigenereKey = keyInputField.text;
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
                        vigenereKey = randomStr;
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
        sampleTextUpdate(vigenereKey, keyIndex);

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
    #endregion
}

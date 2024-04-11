using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

[RequireComponent(typeof(Caesare_Encryption), typeof(Caesare_Decryption))]
public class Caesare_Cryptology : Cryptology_Base
{
    #region Open_Private_Field
    private int caesareKeyValue
    {
        get
        {
            if (!PlayerPrefs.HasKey("caesareKeyValue"))
                return 1;
            return PlayerPrefs.GetInt("caesareKeyValue");
        }
        set
        {
            // �� 1 ~ 25 �� ���̿� �ֵ��� ����
            int changeValue = Mathf.Clamp(value, 1, 25);
            // �� ����
            PlayerPrefs.SetInt("caesareKeyValue", changeValue);
            // ġȯǥ ����
            SetSubstitution();
        }
    }

    [Space(10), Header("Caesare Fields")]
    [Tooltip("Ű �� ��ǲ �ʵ�")]
    [SerializeField]
    private TMP_InputField keyValueInputField;

    [Tooltip("Ű �� ���� ���� ��ư")]
    [SerializeField]
    private Button keyValueRandomBtn;
    #endregion

    #region Private_Fields
    private Caesare_Encryption encryption;
    private Caesare_Decryption decryption;
    #endregion

    #region Custom_Methods
    public override void Init()
    {
        // ��ȣȭ ��� ȹ��
        GetCryptologyWay();
        // UI �ʱ�ȭ
        UISetting();
        // ġȯǥ ����
        SetSubstitution();
    }
    private void GetCryptologyWay()
    {
        encryption = GetComponent<Caesare_Encryption>();
        decryption = GetComponent<Caesare_Decryption>();
    }
    /// <summary>
    /// UI �̺�Ʈ ����
    /// </summary>
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
                    outputField.text = encryption.Encryption(text, encryptionWord);
                }
                // �ص�
                else
                {
                    //Decryption(text);
                    outputField.text = decryption.Decryption(text, encryptionWord);
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
                        outputField.text = encryption.Encryption(inputField.text, encryptionWord);
                    }
                }
                else
                {
                    toggle.GetComponentInChildren<TextMeshProUGUI>().text = "Decryption";
                    // ������ �Է��� ���� �ִٸ� ��ȣȭ
                    if (!string.IsNullOrEmpty(inputField.text))
                    {
                        outputField.text = decryption.Decryption(inputField.text, encryptionWord);
                    }
                }
            });

        // Ű �� ��ǲ �ʵ��� �ؽ�Ʈ�� ����
        keyValueInputField.text = caesareKeyValue.ToString();

        // ��ǲ �ʵ� �Է� �Ϸ� �̺�Ʈ �߰�
        keyValueInputField.onEndEdit.AddListener(
            (text) =>
            {
                // Ű ���� 1 ~ 25������ ���� �������� ����
                caesareKeyValue = Mathf.Clamp(int.Parse(text), 1, 25);

            });

        // Ű �� ���� ��ư �̺�Ʈ �߰�
        keyValueRandomBtn.onClick.AddListener(
        () =>
        {
            // Ű ���� 1 ~ 25������ ���� �������� ����
            caesareKeyValue = Random.Range(1, 25);
            // ��ǲ �ʵ� �ؽ�Ʈ�� ����
            keyValueInputField.text = caesareKeyValue.ToString();
        });
    }

    /// <summary>
    /// UI ġȯǥ ����
    /// </summary>
    public void SetSubstitution()
    {
        // ��ųʸ� �ʱ�ȭ
        encryptionWord.Clear();
        for (int i = 0; i < 26; i++)
        {
            int addCode = ('a' - caesareKeyValue + i);
            if (addCode < 'a')
            {
                addCode = 'z' - (96 - addCode);
            }
            wordList[i].Word = (Word)addCode;
            encryptionWord.Add(original[i], (char)addCode);
        }

        // �Է°� �ִ��� Ȯ��
        if (!string.IsNullOrEmpty(inputField.text))
        {
            // �Է°� ����
            if (isEncryption)
            {
                outputField.text = encryption.Encryption(inputField.text, encryptionWord);
            }
            else
            {
                outputField.text = decryption.Decryption(inputField.text, encryptionWord);
            }
        }
    }
    #endregion

}

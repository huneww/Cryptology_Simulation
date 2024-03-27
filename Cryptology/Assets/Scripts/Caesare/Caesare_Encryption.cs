using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;
using System.Linq;

public class Caesare_Encryption : Encryption_Base
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
            int changeValue = Mathf.Clamp(value, 1, 25);
            PlayerPrefs.SetInt("caesareKeyValue", changeValue);
            SetEncryptionWord();
            if (!string.IsNullOrEmpty(inputField.text))
            {
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
    }

    [Space(10), Header("Caesare Fields")]
    [Tooltip("Ű �� ��ǲ �ʵ�")]
    [SerializeField]
    private TMP_InputField keyValueInputField;

    [Tooltip("Ű �� ���� ���� ��ư")]
    [SerializeField]
    private Button keyValueRandomBtn;
    #endregion

#region MonoBehaviour_CallBacks
    private void Start()
    {
        // UI �ʱ�ȭ
        UISetting();
    }

    private void OnEnable()
    {
        // ġȯǥ ����
        SetEncryptionWord();
    }

    private void OnDisable()
    {
        // ġȯǥ ����
        SetEncryptionWord();
    }
    #endregion

    #region Custom_Methods
    /// <summary>
    /// UI �̺�Ʈ ����
    /// </summary>
    private void UISetting()
    {
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
    private void SetEncryptionWord()
    {
        // ��ųʸ� �ʱ�ȭ
        encryption.Clear();
        for (int i = 0; i < 26; i++)
        {
            int addCode = ('a' - caesareKeyValue + i);
            if (addCode < 'a')
            {
                addCode = 'z' - (96 - addCode);
            }
            wordList[i].Word = (Word)addCode;
            encryption.Add(original[i], (char)addCode);
        }
    }
    #endregion

}

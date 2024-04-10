using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enigma_Encryption : Encryption_Base
{
    #region Open_Private_Fields
    [Header("���� â"), Space(5)]
    [Tooltip("�÷��� ���� ���� â")]
    [SerializeField]
    private GameObject plugBoardObj;
    [Tooltip("�÷��� ���� ��ư")]
    [SerializeField]
    private Button plugBoardBtn;
    [Tooltip("���� ���� ���� â")]
    [SerializeField]
    private GameObject rotorObj;
    [Tooltip("���� ���� ��ư")]
    [SerializeField]
    private Button rotorBtn;
    #endregion

    #region Private_Fields
    // �÷��� ���� ������Ʈ
    private PlugBoard plugBoard;
    // ���� ������Ʈ
    private Rotor_Group rotor;
    #endregion

    #region Monobehaviour_Callbacks
    private void Awake()
    {
        UISetting();
        // �÷��� ����, ������ ��ũ��Ʈ ȹ��
        // �ʱ�ȭ �޼��� ȣ��
        plugBoard = plugBoardObj.GetComponent<PlugBoard>();
        plugBoard.Init();
        rotor = rotorObj.GetComponent<Rotor_Group>();
        rotor.Init();
    }

    private void OnEnable()
    {
        // ���� ���ĺ� ������Ʈ ��Ȱ��ȭ
        foreach (WordNode node in wordList)
        {
            node.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        // ���� ���ĺ� ������Ʈ Ȱ��ȭ
        foreach (WordNode node in wordList)
        {
            node.gameObject.SetActive(true);
        }
    }
    #endregion


    #region Custom_Methods
    public override void UISetting()
    {
        // �÷��� ���� Ȱ��ȭ ��ư �̺�Ʈ
        plugBoardBtn.onClick.AddListener(
            () =>
            {
                rotorObj.SetActive(false);
                plugBoardObj.SetActive(true);
            });

        // ���� Ȱ��ȭ ��ư �̺�Ʈ
        rotorBtn.onClick.AddListener(
            () =>
            {
                rotorObj.SetActive(true);
                plugBoardObj.SetActive(false);
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
                        Encryption(inputField.text);
                    }
                }
                else
                {
                    toggle.GetComponentInChildren<TextMeshProUGUI>().text = "Decryption";
                    // ������ �Է��� ���� �ִٸ� ��ȣȭ
                    if (!string.IsNullOrEmpty(inputField.text))
                    {
                        Decryption(inputField.text);
                    }
                }
            });
        // ��ǲ�� ���� �̺�Ʈ �߰�
        inputField.onValueChanged.AddListener(
            (text) =>
            {
                InputFieldEvent(text);
            });
    }

    /// <summary>
    /// �Է°� ��ȣȭ
    /// </summary>
    /// <param name="text">�Է°�</param>
    private void InputFieldEvent(string text)
    {
        // �Է°��� ������� Ȯ��
        if (!string.IsNullOrEmpty(text))
        {
            StringBuilder sb = new StringBuilder();

            // �ؽ�Ʈ ��ü �빮�ڷ� ����
            string newText = text.ToUpper();

            // �Է� �ؽ�Ʈ�� �Ǹ����� �ε��� �� ȹ��
            int lastIndex = text.Length - 1;

            // �Է� ���ڰ� ���ĺ����� Ȯ��
            if (newText[lastIndex] >= 'A' && newText[lastIndex] <= 'Z')
            {
                // �Է� �ؽ�Ʈ�� ���̰� �ƿ�ǲ�� �ؽ�Ʈ�� ���̺��� �涧
                // ª���� ������ �Է� �ؽ�Ʈ�� 1�� �����
                if (text.Length > outputField.text.Length)
                {

                    // ������ �ؽ�Ʈ �빮�ڷ� ����
                    char upperText = newText[lastIndex];

                    // ���� �ؽ�Ʈ�� �÷��׺��带 ���� �ƿ�ǲ ���� ȹ��
                    char plugText = plugBoard.GetText(upperText);

                    // ���� ��ȸ
                    char rotorText = rotor.RoterAction(plugText);

                    // �ٽ� �ѹ� �÷��� ���� Ȯ��
                    char returnText = plugBoard.GetText(rotorText);

                    // �ƿ�ǲ�� �ִ� �ؽ�Ʈ�� �߰�
                    sb.Append(outputField.text);

                    // �Է� �ؽ�Ʈ�� ������ ���� �ҹ��ڷ� �߰�
                    sb.Append(returnText.ToString());
                }
                else
                {
                    // �Է� �ؽ�Ʈ�� ���̸�ŭ �ݺ�
                    for (int i = 0; i < text.Length; i++)
                    {
                        // ����� �ƿ�ǲ�ʵ��� ���ڸ� �߰�
                        sb.Append(outputField.text[i]);
                    }
                }
            }
            // �Է� ���ڰ� Ư������, ���� ���
            else
            {
                // ������� �ؽ�Ʈ �߰�
                sb.Append(outputField.text);
                sb.Append(newText[lastIndex].ToString());
            }
            outputField.text = sb.ToString();
        }
    }
    public override void Decryption(string encryptionText)
    {
        
    }
    public override void Encryption(string originalText)
    {
        
    }
    #endregion

}

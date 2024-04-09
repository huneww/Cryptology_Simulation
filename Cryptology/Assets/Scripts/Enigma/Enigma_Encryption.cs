using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enigma_Encryption : Encryption_Base
{
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

    private PlugBoard plugBoard;
    private Rotor_Group rotor;

    private void Awake()
    {
        UISetting();
        plugBoard = plugBoardObj.GetComponent<PlugBoard>();
        rotor = rotorObj.GetComponent<Rotor_Group>();
    }

    private void OnEnable()
    {
        foreach (WordNode node in wordList)
        {
            node.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        foreach (WordNode node in wordList)
        {
            node.gameObject.SetActive(true);
        }
    }

    public override void UISetting()
    {
        plugBoardBtn.onClick.AddListener(
            () =>
            {
                rotorObj.SetActive(false);
                plugBoardObj.SetActive(true);
            });

        rotorBtn.onClick.AddListener(
            () =>
            {
                rotorObj.SetActive(true);
                plugBoardObj.SetActive(false);
            });

        // ���� ���� ��ư �̺�Ʈ �߰�
        explainOpenBtn.onClick.AddListener(
            () =>
            {
                explain.SetActive(true);
            });

        // ���� �ݴ� ��ư �̺�Ʈ �߰�
        explainCloseBtn.onClick.AddListener(
            () =>
            {
                explain.SetActive(false);
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

        inputField.onValueChanged.AddListener(
            (text) =>
            {
                InputFieldEvent(text);
            });
    }

    public override void Encryption(string originalText)
    {
        base.Encryption(originalText);
    }

    public override void Decryption(string encryptionText)
    {
        base.Decryption(encryptionText);
    }

    private void InputFieldEvent(string text)
    {
        if (!string.IsNullOrEmpty(text))
        {
            StringBuilder sb = new StringBuilder();

            // �Է� �ؽ�Ʈ�� ���̰� �ƿ�ǲ�� �ؽ�Ʈ�� ���̺��� �涧
            // ª���� ������ �Է� �ؽ�Ʈ�� 1�� �����
            if (text.Length > outputField.text.Length)
            {
                // �Է� �ؽ�Ʈ�� �Ǹ����� �ε��� �� ȹ��
                int lastIndex = text.Length - 1;
                // ������ �ؽ�Ʈ �빮�ڷ� ����
                string upperText = text[lastIndex].ToString().ToUpper();
                // ���� �ؽ�Ʈ�� �÷��׺��带 ���� �ƿ�ǲ ���� ȹ��
                string plugText = plugBoard.GetText(upperText);
                // ���� ��ȸ
                char roterText = rotor.RoterAction(plugText);
                // �ٽ� �ѹ� �÷��� ���� Ȯ��
                string returnText = plugBoard.GetText(roterText.ToString());
                // �ƿ�ǲ�� �ִ� �ؽ�Ʈ�� �߰�
                sb.Append(outputField.text);
                // �Է� �ؽ�Ʈ�� ������ ���� �ҹ��ڷ� �߰�
                sb.Append(returnText.ToLower());
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
            outputField.text = sb.ToString();
        }
    }

}

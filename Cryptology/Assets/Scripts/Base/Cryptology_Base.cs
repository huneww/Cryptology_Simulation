using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using System.Linq;

public abstract class Cryptology_Base : MonoBehaviour
{
    #region Open_Private_Field
    [Header("Base Fields")]
    [Tooltip("���ĺ� ���")]
    [SerializeField]
    protected List<WordNode> wordList = new List<WordNode>();

    [Tooltip("��ȯ�� ���ڿ� �Է� �ʵ�")]
    [SerializeField]
    protected TMP_InputField inputField;

    [Tooltip("��ȯ�� ���ڿ� ��� �ʵ�")]
    [SerializeField]
    protected TMP_InputField outputField;

    [Tooltip("����Ī ���")]
    [SerializeField]
    protected Toggle toggle;
    #endregion

    #region Private_Field
    // ���ĺ� ����
    protected List<char> original = new List<char>();
    // ġȯ�� ���ĺ�
    protected Dictionary<char, char> encryptionWord = new Dictionary<char, char>();
    // ��ȣȭ�Ұ����� Ȯ�� ����
    protected bool isEncryption = true;
    #endregion

    #region Abstract_Methods
    public abstract void Init();
    #endregion

}

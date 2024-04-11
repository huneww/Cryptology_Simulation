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
    [Tooltip("알파벳 노드")]
    [SerializeField]
    protected List<WordNode> wordList = new List<WordNode>();

    [Tooltip("변환할 문자열 입력 필드")]
    [SerializeField]
    protected TMP_InputField inputField;

    [Tooltip("변환된 문자열 출력 필드")]
    [SerializeField]
    protected TMP_InputField outputField;

    [Tooltip("스위칭 토글")]
    [SerializeField]
    protected Toggle toggle;
    #endregion

    #region Private_Field
    // 알파벳 원본
    protected List<char> original = new List<char>();
    // 치환된 알파벳
    protected Dictionary<char, char> encryptionWord = new Dictionary<char, char>();
    // 암호화할것인지 확인 변수
    protected bool isEncryption = true;
    #endregion

    #region Abstract_Methods
    public abstract void Init();
    #endregion

}

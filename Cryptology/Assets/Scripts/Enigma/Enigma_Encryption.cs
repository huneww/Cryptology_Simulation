using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enigma_Encryption : Encryption_Base
{

    #region MonoBehaviour_Callbacks
    private void OnEnable()
    {
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
    public override void Encryption(string originalText)
    {
        
    }

    public override void Decryption(string encryptionText)
    {
        
    }

    public override void SetEncryptionWord()
    {
        throw new System.NotImplementedException();
    }
    #endregion

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDecryption
{
    public abstract string Decryption(string encryptionText, Dictionary<char, char> words);
}

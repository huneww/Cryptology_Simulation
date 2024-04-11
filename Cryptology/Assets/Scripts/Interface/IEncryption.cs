using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEncryption
{
    public abstract string Encryption(string originalText, Dictionary<char, char> words);
}

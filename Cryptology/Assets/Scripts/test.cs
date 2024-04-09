using UnityEngine;

public class test : MonoBehaviour
{
    private void Start()
    {
        string words = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        char[] wordArray = words.ToCharArray();
        int i = wordArray.Length;
        while(i > 0)
        {
            int j = Random.Range(0, i--);
            char temp = wordArray[i];
            wordArray[i] = wordArray[j];
            wordArray[j] = temp;
        }
        foreach (char c in wordArray)
            Debug.Log(c);
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class test : MonoBehaviour
{
    public List<int> testList = new List<int>();

    private void Start()
    {
        for (int i = 0; i < 26;)
        {
            bool check = true;
            int value = Random.Range(97, 123);
            foreach (int list in testList)
            {
                if (list == value)
                {
                    check = false;
                    break;
                }
            }
            if (check)
            {
                testList.Add(value);
                i++;
            }
        }
    }
}

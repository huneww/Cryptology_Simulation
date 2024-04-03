using UnityEngine;
using TMPro;

public enum Word
{
    a = 97, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p, q, r, s, t, u, v, w, x, y, z,
}

public class WordNode : MonoBehaviour
{
    [SerializeField]
    private Word word = Word.a;
    public Word Word
    {
        get
        {
            return word;
        }
        set
        {
            word = value;
            text.text = word.ToString();
        }
    }
    [SerializeField]
    private TMP_Text text;

    private void Awake()
    {
        text.text = word.ToString();
    }

}

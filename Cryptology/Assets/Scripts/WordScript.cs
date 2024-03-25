using UnityEngine;
using TMPro;

public enum World
{
    a = 97, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p, q, r, s, t, u, v, w, x, y, z,
}

public class WordScript : MonoBehaviour
{
    [SerializeField]
    private World world = World.a;
    public World World
    {
        get
        {
            return world;
        }
        set
        {
            world = value;
            text.text = world.ToString();
        }
    }
    [SerializeField]
    private TMP_Text text;

    private void Awake()
    {
        text.text = world.ToString();
    }

}

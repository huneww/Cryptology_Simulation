using UnityEngine;
using TMPro;

public class TotalEncryptionManager : MonoBehaviour
{
    [SerializeField]
    private MonoBehaviour[] encryptionWay;
    [SerializeField]
    private TMP_Dropdown dropDown;

    private void Awake()
    {
        UISetting();
    }

    private void UISetting()
    {
        dropDown.onValueChanged.AddListener(
        (value) =>
        {
            foreach (var way in encryptionWay)
            {
                way.gameObject.SetActive(false);
            }
            encryptionWay[value].gameObject.SetActive(true);
        });
    }
}

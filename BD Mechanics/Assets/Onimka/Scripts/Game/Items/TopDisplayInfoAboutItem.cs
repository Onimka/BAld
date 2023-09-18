using TMPro;
using UnityEngine;

public class TopDisplayInfoAboutItem : MonoBehaviour
{

    [SerializeField] private TMP_Text _nameItem;
    public static TopDisplayInfoAboutItem Instance;

    private void Awake()
    {
        Instance = this;
    }


    // Переделать на получение сыылки на айтем и из него доставать название, хп и т.д.
    public void SetTitle(string title)
    {
        _nameItem.text = title;
    }

}

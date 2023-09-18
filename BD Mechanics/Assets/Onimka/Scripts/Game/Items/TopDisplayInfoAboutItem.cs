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


    // ���������� �� ��������� ������ �� ����� � �� ���� ��������� ��������, �� � �.�.
    public void SetTitle(string title)
    {
        _nameItem.text = title;
    }

}

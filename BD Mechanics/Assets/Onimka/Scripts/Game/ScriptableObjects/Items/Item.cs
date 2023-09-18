using UnityEngine;


[ CreateAssetMenu(fileName = "new Item" , menuName = "Inventory Item Data / Base Item")]
public class Item : ScriptableObject, IDisplayItem, IInvertoryItemInfo
{
    [SerializeField] private string _id;

    [SerializeField] private string _title;

    [SerializeField] private string _descrition;

    [SerializeField] private int _maxItemInvertorySlot;

    [SerializeField] private Sprite _icon;

    [SerializeField] private GameObject _prefabItem;

    public void Use()
    {
        Debug.Log("Use item: " + _title);
    }

    public string Title => _title;

    public string Id => _id;

    public Sprite Icon => _icon;

    public string Description => _descrition;

    public int MaxItemInvertorySlot => _maxItemInvertorySlot;
}

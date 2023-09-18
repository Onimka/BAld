using UnityEngine;

public class ItemInfoTest : MonoBehaviour, IDisplayItem, IInvertoryItemInfo
{
    [SerializeField] private Item _itemData;

    public string Id => _itemData.Id;

    public Sprite Icon => _itemData.Icon;

    public string Title => _itemData.Title;

    public string Description => _itemData.Description;

    public int MaxItemInvertorySlot => _itemData.MaxItemInvertorySlot;
}

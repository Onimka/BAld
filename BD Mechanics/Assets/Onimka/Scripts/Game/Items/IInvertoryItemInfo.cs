using UnityEngine;

public interface IInvertoryItemInfo
{
    string Id { get; }

    Sprite Icon { get; }

    string Title { get; }

    string Description { get; }

    int MaxItemInvertorySlot { get; }
}
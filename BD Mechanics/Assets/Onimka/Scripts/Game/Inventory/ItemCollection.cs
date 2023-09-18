using System.Collections.Generic;
using UnityEngine;

public class ItemCollection : MonoBehaviour
{
   [SerializeField] private List<ItemConteiner> _items  = new List<ItemConteiner>();



    [System.Serializable]
    private class ItemConteiner
    {
        [SerializeField] private Item _item;
        [SerializeField] private int _count = 1;
 
        public int MaxCount => _item.MaxItemInvertorySlot;
    }
}

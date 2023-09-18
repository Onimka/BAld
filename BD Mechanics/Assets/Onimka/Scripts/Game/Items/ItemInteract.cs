using UnityEngine;

public class ItemInteract : MonoBehaviour, IDisplayItem
{

    [SerializeField] private string _title;
    public string Title => _title;
   
}

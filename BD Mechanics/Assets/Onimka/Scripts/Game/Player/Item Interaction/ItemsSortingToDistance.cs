using System.Collections.Generic;
using UnityEngine;

public class ItemsSortingToDistance : MonoBehaviour
{
    private List<GameObject> _items = new List<GameObject>();
    private string _tagItems = "Item";
    private bool _selectIsPressed;

    private void OnDisable()
    {
        SelectItems(false);
    }

    private void Update()
    {
        SelectDeselectItems();
    }
    

    public bool CheckConteinsItem(GameObject item)
    {
        return _items.Contains(item);
    }


    private void SelectDeselectItems()
    {
        if (Input.GetKeyDown(HotKeys.SelectItems))
        {
            _selectIsPressed = true;
            SelectItems(_selectIsPressed);
            
            Debug.Log("is Select");
        }

        else if (Input.GetKeyUp(HotKeys.SelectItems))
        {
            _selectIsPressed = false;
            SelectItems(_selectIsPressed);
            
            Debug.Log("is Deselect");
        }
    }

    private void SelectItems(bool active)
    {
        foreach (var item in _items)
        {
            item.GetComponent<DispleyVisible>()?.Selection(active);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tagItems) && !_items.Contains(other.gameObject))
        {
            _items.Add(other.gameObject);         
            other.GetComponent<DispleyVisible>()?.Selection(_selectIsPressed);
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_tagItems) && _items.Contains(other.gameObject))
        {
            _items.Remove(other.gameObject);
            other.GetComponent<DispleyVisible>()?.Selection(false);
        }
    }
}

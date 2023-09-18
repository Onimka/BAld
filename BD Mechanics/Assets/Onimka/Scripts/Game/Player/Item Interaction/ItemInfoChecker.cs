using UnityEngine;
using UnityEngine.EventSystems;

public class ItemInfoChecker : MonoBehaviour
{
    private const float _distanceRay = 50f;
    private DispleyVisible _currentItem;
    private DispleyVisible _previoslyItem;

    private ItemsSortingToDistance _itemChecker;

    private Camera _mainCamera;

    private void Awake()
    {
        _itemChecker = GetComponent<ItemsSortingToDistance>();
        _mainCamera = Camera.main;
    }


    private void Update()
    {
        CheckIntectiveItems();

        MouseHandle();
    }

    private void CheckIntectiveItems()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _distanceRay))
        {
            var activeItem = hit.collider.GetComponent<DispleyVisible>();
            if (activeItem && !EventSystem.current.IsPointerOverGameObject() && _itemChecker.CheckConteinsItem(hit.collider.gameObject))
            {
                if (_currentItem && _currentItem != activeItem)
                {
                    DisableItem();
                }

                _currentItem = activeItem;
                TopDisplayInfoAboutItem.Instance.SetTitle( _currentItem.Name);
                _currentItem.TargetSelect(true);
            }
            else
            {
                DisableItem();
            }
        }
        else
        {
            DisableItem();
        }
    }

    private void MouseHandle()
    {
        if (Input.GetKeyDown(HotKeys.LeftMouse) && _currentItem != null)
        {
            if (_previoslyItem != null)
                _previoslyItem.Selected = false;

            _currentItem.Selected = true;
            _previoslyItem = _currentItem;
        }

        if (Input.GetKeyDown(HotKeys.RightMouse))
        {
            if (_previoslyItem != null)
                _previoslyItem.Selected = false;
            _previoslyItem = null;
        }
    }

    private void DisableItem()
    {
        if (_currentItem != null)
        {
            _currentItem.TargetSelect(false);
            TopDisplayInfoAboutItem.Instance.SetTitle("");
            _currentItem = null;
        }
    }

}

using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIAbilityCheckWay : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _nameTXT;
    [SerializeField] private Vector3 _offset = new Vector3(1f, 1f, 0f);

    private bool _isActive;
    private Canvas _mainCanvas;
    private RectTransform _rectTransform;


    private string _noWay = "Не пройти";
    private string _haveWay = "";

    public static UIAbilityCheckWay Insctance;

    private void Awake()
    {
        Insctance = this;
        EnableState(false);
        _mainCanvas = GetComponentInParent<Canvas>();
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        MoveUIToCursore();
    }

    public void SetIcon(Sprite sprite) =>
            _icon.sprite = sprite;

    public void HaveWayState(bool haveWay) =>
        _nameTXT.text = haveWay ? _haveWay : _noWay;

    public void EnableState(bool active)
    {
        _isActive = active;
        gameObject.SetActive(_isActive);
    }

    private void MoveUIToCursore()
    {
        if (_isActive)
        {
            Vector2 movePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_mainCanvas.transform as RectTransform, Input.mousePosition + _offset, _mainCanvas.worldCamera, out movePos);
            Vector3 mousePosition = _mainCanvas.transform.TransformPoint(movePos);
            _rectTransform.position = mousePosition;
        }
    }
}

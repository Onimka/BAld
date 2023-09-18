using UnityEngine;
using TMPro;

public class DispleyVisible : MonoBehaviour
{
    [Header("InstantiateParam")]
    [SerializeField] private GameObject _prefabLable;
    [SerializeField] private Vector3 _offset = new Vector3(0, 0.8f, 0);
    private TMP_Text _nameTXT;
    private string _markText = @"<mark=#270303 padding=""15, 15, 5, 5""><font=""LiberationSans SDF"">";
    private GameObject _lableName;
    private Outline _outline;

    [HideInInspector] public bool Selected = false;

    public string Name => displayItem?.Title;

    private IDisplayItem displayItem;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        displayItem = GetComponent<IDisplayItem>();

        this.gameObject.tag = "Item";
        _lableName = Instantiate(_prefabLable, transform.position, Quaternion.identity);
        _lableName.transform.SetParent(transform);
        _lableName.transform.localPosition += _offset;
        _lableName.SetActive(false);
        _nameTXT = _lableName.GetComponentInChildren<TMP_Text>();
        _nameTXT.text = _markText + Name;
    }

    public void Selection(bool active)
    {
        _lableName.SetActive(active);
    }

    public void TargetSelect(bool active)
    {
        if(_outline != null)
            _outline.enabled = active;
    }
}

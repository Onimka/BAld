using UnityEngine;
using UnityEngine.UI;
using System;

public class UISkillButton : MonoBehaviour
{

    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _selection;


    private bool _isSelect;
    private AbstractHumanState _state;

    public event Action<AbstractHumanState> OnChanged;

    public AbstractHumanState State => _state;
    public Sprite Icon => _icon.sprite;

    //private Color _selectColor = new Color(0.3f, 0.3f, 0.3f);
    //private Color _deselectColor = new Color(0.5f, 0.5f, 0.5f);


   
    public void SetParam(AbstractHumanState state)
    {
        _state = state;
        _icon.sprite = state.DataMeAbility.Icon;
    }


    private void Awake()
    {
        Selected(false);
    }

    public void Selected(bool isSelect)
    {      
        _isSelect = isSelect;
        _selection.SetActive(isSelect);
        //_icon.color = isSelect ? _selectColor : _deselectColor;
    }

    public void OnCick()
    {
        if(!_isSelect)
            OnChanged?.Invoke(State);
    }
}

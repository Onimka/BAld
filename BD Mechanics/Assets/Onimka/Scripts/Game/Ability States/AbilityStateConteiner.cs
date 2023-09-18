using System.Collections.Generic;
using UnityEngine;

public class AbilityStateConteiner : MonoBehaviour
{
    public List<AbstractHumanState> AbilityStates => _abilityStates;
    private PlayerAbilityStateMachine _playerStateMachine;

    [SerializeField] private List<AbstractHumanState> _abilityStates = new List<AbstractHumanState>();

    [SerializeField] private GameObject _buttonPrefab;
    [SerializeField] private RectTransform _parentButton;
    private List<UISkillButton> _buttons = new List<UISkillButton>();


    private void Awake()
    {
        _playerStateMachine = GetComponent<PlayerAbilityStateMachine>();
    }

    private void OnEnable()
    {
        CreateAllButtons();
    }

    private void OnDisable()
    {
        RemoveAllButtons();
    }

    private void CreateAllButtons()
    {
        foreach (var state in _abilityStates)
        {
            CreateStateButton(state);          
        }
    }

    private void RemoveAllButtons()
    {
        foreach (var state in _buttons)
        {
            state.OnChanged -= EnterInNewState;
        }
    }

    private void EnterInNewState(AbstractHumanState newState)
    {
        _playerStateMachine.EnterInNewState(newState);
        foreach (var button in _buttons)
        {
            if (button.State != _playerStateMachine.CurrentState)
                button.Selected(false);
            else
                button.Selected(true);
        }
    }

    public void ExitFromCurrentState()
    {
        foreach (var button in _buttons)
        {
            button.Selected(false);
        }
    }

    public void CreateStateButton(AbstractHumanState abilityState)
    {
        GameObject button = Instantiate(_buttonPrefab, _parentButton);
        UISkillButton uiButton = button.GetComponent<UISkillButton>();
        uiButton.SetParam(abilityState);
        _buttons.Add(uiButton);
        uiButton.OnChanged += EnterInNewState;
    }

}

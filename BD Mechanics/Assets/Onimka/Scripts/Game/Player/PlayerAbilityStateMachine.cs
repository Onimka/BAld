using UnityEngine;

public class PlayerAbilityStateMachine : MonoBehaviour
{
    private AbilityStateConteiner _abilityStateConteiner;

    [SerializeField] private RadiusActionVisible _radiusAction;

    [SerializeField] private FantomPlayerController _fantomPlayer;

    [SerializeField] private float _speedRotation;

    [SerializeField] private AbstractHumanState _freedomMoveState; // Пока по умолчанию
    [SerializeField] private CheckFantomState _checkFantomState;

    private AbstractHumanState _previuslyState;
    public AbstractHumanState CurrentState => _currentState;
    private AbstractHumanState _currentState;


    private float _distanceToRotation = 1.5f;
    private float _distanceStartAction = 0.5f;



    private void Awake()
    {
        EnterInNewState(_freedomMoveState);
        _abilityStateConteiner = GetComponent<AbilityStateConteiner>();
    }

    private void OnDisable()
    {      
        _currentState.OnFinished -= ExitFromCurrentState;
        _currentState.OnStartAction -= DisableAllNavigationEffets;
    }

    private void Update()
    {
        StateUpdate();
    }

    private void StateUpdate()
    {
        _currentState.UpdateState();

        OnClickExit();

        if (_previuslyState != null)
        {
            UsedFantomState();

            return;
        }

        else if (_currentState != _freedomMoveState && !_currentState.isAction)
        {
            if (_currentState.isHaveWay)
            {
                DrawerLineArk.Instance.UpdateRenderLine(transform.position, _currentState.Result);
                DrawerLineArk.Instance.ActiveDeactiveLine(true, transform);
                DrawerLineArk.Instance.CanMove(true);

                _fantomPlayer.ActivateFantom(false);
                RotationByTarget(_currentState.Result, transform, _speedRotation);
                UIAbilityCheckWay.Insctance.EnableState(true);
                UIAbilityCheckWay.Insctance.HaveWayState(true);
                _checkFantomState.DisableDrawLine();
                _previuslyState = null;
            }

            else
            {
                _checkFantomState.CheckWayToTarget(_currentState.Result, _currentState.RadiusToCast);

                if (_checkFantomState.isHaveWay)
                {
                    DrawerLineArk.Instance.UpdateRenderLine(_fantomPlayer.transform.position, _currentState.Result);
                    DrawerLineArk.Instance.ActiveDeactiveLine(true, _fantomPlayer.transform);
                    DrawerLineArk.Instance.CanMove(true);

                    RotationByTarget(_currentState.Result, _fantomPlayer.transform, 20f);
                    _fantomPlayer.SetPosition(_checkFantomState.Result);
                    _fantomPlayer.ActivateFantom(true);

                    UIAbilityCheckWay.Insctance.HaveWayState(true);

                    if (Input.GetKeyDown(HotKeys.LeftMouse))
                    {
                        DisableAllNavigationEffets();

                        _previuslyState = _currentState;
                        ExitFromCurrentState();

                        _freedomMoveState.DoAction(_checkFantomState.Result);
                        _fantomPlayer.ActivateFantom(false);
                    }
                }
                else
                {
                    UIAbilityCheckWay.Insctance.HaveWayState(false);
                    DrawerLineArk.Instance.ActiveDeactiveLine(false);
                    DrawerLineArk.Instance.CanMove(false);

                    _fantomPlayer.ActivateFantom(false);
                    _previuslyState = null;
                }

            }

        }
    }

    private void OnClickExit()
    {
        if (Input.GetKeyDown(HotKeys.RightMouse) && !_currentState.isAction)
        {
            ExitFromCurrentState();
            _fantomPlayer.ActivateFantom(false);
            DrawerLineArk.Instance.ActiveDeactiveLine(false);
            _previuslyState = null;
            _freedomMoveState.DoAction(transform.position);
        }
    }

    private void UsedFantomState()
    {
        float distBetweenPlayerAndFantomPoint = (transform.position - _checkFantomState.Result).magnitude;

        if (distBetweenPlayerAndFantomPoint <= _distanceToRotation)
            RotationByTarget(_previuslyState.Result, transform, _speedRotation);
        if (distBetweenPlayerAndFantomPoint <= _distanceStartAction)
        {
            _previuslyState.DoAction(_previuslyState.Result);
            _previuslyState = null;
        }
    }

    private void ExitFromCurrentState()
    {
        if (_currentState != _freedomMoveState)
        {
            _currentState.OnFinished -= ExitFromCurrentState;
            _currentState.OnStartAction -= DisableAllNavigationEffets;

            _currentState.ExitState();
            _currentState = _freedomMoveState;

            _freedomMoveState.enabled = true;
            DisableAllNavigationEffets();

            _abilityStateConteiner.ExitFromCurrentState();
        }

    }

    public void EnterInNewState(AbstractHumanState newState)
    {
        _currentState?.ExitState();
        _currentState = newState;
        _currentState.EnterState();

        _currentState.OnFinished += ExitFromCurrentState;
        _currentState.OnStartAction += DisableAllNavigationEffets;

        _freedomMoveState.enabled = false;

        if (_currentState != _freedomMoveState)
        {
            _radiusAction.EnableVisible(_currentState.RadiusToCast);
            UIAbilityCheckWay.Insctance.SetIcon(_currentState.DataMeAbility.Icon);
        }
    }

    private void DisableAllNavigationEffets()
    {
        UIAbilityCheckWay.Insctance.EnableState(false);
        DrawerLineArk.Instance.ActiveDeactiveLine(false);
        _checkFantomState.DisableDrawLine();

        if (!_radiusAction.isDisable)
            _radiusAction.DisableVisible();
    }

    private void RotationByTarget(Vector3 Target, Transform pos, float rotationSpeed)
    {
        var direction = Target - pos.position;
        direction.y = 0f;
        Quaternion rotation = Quaternion.LookRotation(direction);
        pos.rotation = Quaternion.Lerp(pos.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}

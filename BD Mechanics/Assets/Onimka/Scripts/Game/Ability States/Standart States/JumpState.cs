using UnityEngine;
using DG.Tweening;
using System;


public class JumpState : AbstractHumanState
{
    [SerializeField] private float _abilityRadius = 5f;

    public override float RadiusToCast => _abilityRadius;

    public override bool isAction => _isAction;

    public override event Action OnFinished;
    public override event Action OnStartAction;


    public override void EnterState()
    {
       
    }

    public override void UpdateState()
    {
        if (CheckArea(transform.position, _abilityRadius))
            OnClick();
    }

    public override void ExitState()
    {
       
    }

    private void OnClick()
    {
        if (Input.GetKeyDown(HotKeys.LeftMouse))
        {
            DoAction(_result);         
        }
    }

    private void StartAction()
    {
        _agent.enabled = false;
        _isAction = true;
        OnStartAction?.Invoke();
        _animator.SetTrigger("Jump");
    }

    private void FinishAction()
    {
        _agent.enabled = true;
        _isAction = false;
        OnFinished?.Invoke();
    }

    public override void DoAction(Vector3 target)
    {
        StartAction();

        DOTween.Sequence().AppendInterval(0.4f).
                    Append(transform.DOJump(target, 2f, 1, 0.9f).SetEase(Ease.OutQuad)).AppendInterval(0.3f).OnComplete(() => FinishAction());
    }
}

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using System;

public class FreedomMovementState : AbstractHumanState
{
    private float _maxRange = 1.5f;
    private int _countTry = 20;

    private NavMeshPath _navMeshPath;

    public override event Action OnFinished;
    public override event Action OnStartAction;

    public override float RadiusToCast => throw new NotImplementedException();

    public override bool isAction => false;

    private void Start()
    {
        _navMeshPath = new NavMeshPath();
    }

    public override void EnterState()
    {

    }

    public override void UpdateState()
    {
        OnClick();
    }

    public override void ExitState()
    {
        GoToTarget(transform.position);
    }

    private void OnClick()
    {
        if (Input.GetKeyDown(HotKeys.LeftMouse) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit placeInfo;
            if (Physics.Raycast(ray, out placeInfo))
            {
                GoToTarget(placeInfo.point);
            }
        }
    }

    public bool GoToTarget(Vector3 target)
    {
        Vector3 result = Vector3.zero;
      
        for (float range = 0.5f; range < _maxRange; range += 0.5f)
        {
            if (RandomPointOnNavMesh(target, range, out result))
            {
                _agent.SetDestination(result);
                MovePoint.Instance.ActiveDeative(true, result);
                return true;
            }
        }
        return false;
    }

    private bool RandomPointOnNavMesh(Vector3 center, float range, out Vector3 result)
    {
        List<Vector3> sorting = new List<Vector3>();
        for (int i = 0; i < _countTry; i++)
        {
            Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, range, NavMesh.AllAreas))
            {
                sorting.Add(hit.position);
            }
        }
        if (sorting.Count == 0)
        {
            result = Vector3.zero;
            return false;
        }

        result = SortDistanceToTarget(sorting);
        return true;
    }

    public override void DoAction(Vector3 target)
    {
        GoToTarget(target);
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class CheckFantomState : AbstractHumanState
{
    private int _countTryToCheck = 200;

    private NavMeshPath _navMeshPath;

    private Vector3 _currentTarget = Vector3.zero;

    [HideInInspector] public bool inRadius;

    public override float RadiusToCast => throw new NotImplementedException();

    public override bool isAction => throw new NotImplementedException();

    public override event Action OnFinished;
    public override event Action OnStartAction;

    private void Start()
    {
        _navMeshPath = new NavMeshPath();
    }

    public override void EnterState()
    {
        throw new NotImplementedException();
    }

    public override void ExitState()
    {
        throw new NotImplementedException();
    }

    public override void UpdateState()
    {
        
    }
   
    public void GoToTarget()
    {
        _agent.SetDestination(_result);
        DisableDrawLine();
    }

    public bool CheckWayToTarget(Vector3 target, float radius)
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 result = Vector3.zero;

            var distBetweenResult = (target - _currentTarget).magnitude;

            if (distBetweenResult > 0.7f)
            {
                _isHaveWay = RandomPointOnNavMesh(target, radius, out result);
                if (_isHaveWay)
                {

                    base._result = result;
                    base._isHaveWay = true;
                    _currentTarget = target;
                }
            }

            return _isHaveWay;
        }
        _isHaveWay = false;
        return _isHaveWay;
    }

    private bool RandomPointOnNavMesh(Vector3 center, float range, out Vector3 result)
    {
        List<Vector3> sorting = new List<Vector3>();
        for (int i = 0; i < _countTryToCheck; i++)
        {
            Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, range, NavMesh.AllAreas))
            {
                _agent.CalculatePath(hit.position, _navMeshPath);
                float distBetweenPoints = (center - hit.position).magnitude;
                if (_navMeshPath.status == NavMeshPathStatus.PathComplete && base.RayToCheck(hit.position, center, range) && distBetweenPoints <= range)
                    sorting.Add(hit.position);
            }
        }
        if (sorting.Count == 0)
        {
            result = Vector3.zero;
            DisableDrawLine();
            return false;
        }

        result = SortDistanceToTarget(sorting);

        DrawLineToTarget(result);

        return true;
    }

    public void DisableDrawLine()
    {
        DrawerLinePath.Instance.DeactivateLine();
    }

    private void DrawLineToTarget(Vector3 target)
    {       
        _agent.CalculatePath(target, _navMeshPath);
        DrawerLinePath.Instance.DrawLineToTarget(target, _navMeshPath);
    }

    public override void DoAction(Vector3 target)
    {
        throw new NotImplementedException();
    }

}

using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using System.Collections.Generic;

public abstract class AbstractHumanState : MonoBehaviour
{
    [SerializeField] protected DataAbility _dataAbility;
    public DataAbility DataMeAbility => _dataAbility;

    protected Animator _animator;
    protected bool _isAction = false;
    protected NavMeshAgent _agent;
    protected Vector3 _result;
    protected bool _isHaveWay;

    public Vector3 Result => _result;

    public abstract event Action OnFinished;
    public abstract event Action OnStartAction;

    public bool isHaveWay => _isHaveWay;
    public abstract bool isAction { get; }
    public abstract float RadiusToCast { get; }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
    }

    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void ExitState();

    public abstract void DoAction(Vector3 target);


    protected bool RayToCheck(Vector3 myPos, Vector3 target, float distanceToTarget)
    {
        Vector3 offsetStart = transform.position + new Vector3(0, 1.5f, 0);
        Vector3 offsetEnd = target + new Vector3(0, 1f, 0);
        Ray ray = new Ray(offsetStart, offsetEnd - offsetStart);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distanceToTarget))
        {
            Debug.DrawRay(offsetStart, (offsetEnd - offsetStart) * distanceToTarget, Color.red);
            return false;
        }
        Debug.DrawRay(offsetStart, (offsetEnd - offsetStart) * distanceToTarget, Color.yellow);
        return true;
    }

    protected bool CheckArea(Vector3 startPos, float abilityRadius)
    {
        if (!EventSystem.current.IsPointerOverGameObject() && !_isAction)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit placeInfo;
            if (Physics.Raycast(ray, out placeInfo))
            {
                NavMeshHit hit;
                if (NavMesh.SamplePosition(placeInfo.point, out hit, 0.1f, NavMesh.AllAreas))
                {
                    var distance = (transform.position - hit.position).magnitude;

                    if (distance <= abilityRadius && RayToCheck(transform.position, hit.position, distance))
                    {
                        _result = hit.position;
                        _isHaveWay = true;
                        return true;
                    }
                    _result = hit.position;
                    _isHaveWay = false;

                    return _isHaveWay;
                }

                return false;
            }

            return false;

        }

        return false;

    }

    protected Vector3 SortDistanceToTarget(List<Vector3> sorting)
    {
        float minDist = (sorting[0] - transform.position).magnitude;
        Vector3 result = sorting[0];
        foreach (var item in sorting)
        {
            var dist = (item - transform.position).magnitude;
            if (dist < minDist)
            {
                result = item;
                minDist = dist;
            }
        }
        return result;
    }
}


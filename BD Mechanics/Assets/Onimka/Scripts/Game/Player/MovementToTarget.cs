using UnityEngine;

public class MovementToTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speedCamera;
    private Transform _selfTransfotm;

    private void Start()
    {
        _selfTransfotm = base.transform;
    }
    private void Update()
    {
        _selfTransfotm.position = Vector3.MoveTowards(_selfTransfotm.position, _target.position, _speedCamera * Time.deltaTime);
    }
}

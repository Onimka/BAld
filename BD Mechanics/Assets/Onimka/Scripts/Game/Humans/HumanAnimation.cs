using UnityEngine;
using UnityEngine.AI;

public class HumanAnimation : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private NavMeshAgent _agent;

    private void Update()
    {
        _anim.SetFloat("Speed", _agent.velocity.magnitude);
    }

}

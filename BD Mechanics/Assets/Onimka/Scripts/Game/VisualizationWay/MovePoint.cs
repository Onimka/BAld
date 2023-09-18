using UnityEngine;

public class MovePoint : MonoBehaviour
{
    public static MovePoint Instance;
    private string _tagTrigger = "Player";


    private void Awake()
    {
        Instance = this;
        ActiveDeative(false);
    }

    public void ActiveDeative(bool active)
    {
        gameObject.SetActive(active);
    }

    public void ActiveDeative(bool active, Vector3 target)
    {
        gameObject.SetActive(active);
        transform.position = target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActiveDeative(false);
        }
    }
}

using UnityEngine;

public class FantomPlayerController : MonoBehaviour
{
    // ЗАГЛУШКА
    // Сделать реализацию через опрос инвентаря на экипировку

    [SerializeField] private GameObject _fantomPlayer;


    public void ActivateFantom(bool active)
    {
        _fantomPlayer.SetActive(active);
    }

    public void SetPosition(Vector3 target)
    {
        _fantomPlayer.transform.position = target;
    }

}

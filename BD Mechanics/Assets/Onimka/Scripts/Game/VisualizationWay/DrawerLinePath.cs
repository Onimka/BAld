using UnityEngine;
using UnityEngine.AI;

public class DrawerLinePath : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRendererPrefab;
    private LineRenderer _linePath;
    public static DrawerLinePath Instance;

    private void Awake()
    {
        Instance = this;
        CreateLineRenderers(transform);
    }

    private void CreateLineRenderers(Transform pos)
    {
        _linePath = Instantiate(lineRendererPrefab, Vector3.zero, Quaternion.identity);
        _linePath.transform.SetParent(pos);
        _linePath.gameObject.SetActive(false); // Отключаем, чтобы он не отображался в сцене
    }

    public void DeactivateLine()
    {
        _linePath.gameObject.SetActive(false);
    }

    public void DrawLineToTarget(Vector3 target, NavMeshPath navMeshPath)
    {
        _linePath.gameObject.SetActive(true);
        _linePath.positionCount = navMeshPath.corners.Length;

        var corner = navMeshPath.corners;
        for (int i = 0; i < corner.Length; i++)
        {
            Vector3 pos = new Vector3(corner[i].x, corner[i].y + 0.5f, corner[i].z);
            _linePath.SetPosition(i, pos);
        }
    }
}

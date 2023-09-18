using UnityEngine;

public class DrawerLineArk : MonoBehaviour
{
    [SerializeField] private int heightPointBiaze_0 = 2;
    [SerializeField] private int heightPointBiaze_1 = 10;
    [SerializeField] private LineRenderer lineRendererPrefab;
    //[SerializeField] private Gradient _trueColor;
    //[SerializeField] private Gradient _falseColor;
    private LineRenderer _lineArc; // Список компонентов Line Renderer

    public static DrawerLineArk Instance;

    private void Awake()
    {
        Instance = this;
        CreateLineRenderers(transform);
    }

    private void OnDisable()
    {
        MovePoint.Instance.ActiveDeative(false);
    }

    public void CreateLineRenderers(Transform pos)
    {
        _lineArc = Instantiate(lineRendererPrefab, Vector3.zero, Quaternion.identity);
        _lineArc.transform.SetParent(pos);
        _lineArc.gameObject.SetActive(false); // Отключаем, чтобы он не отображался в сцене
    }

    public void UpdateRenderLine(Vector3 startPos, Vector3 target)
    {

        int sigmentsNum = 20;
        Vector3[] points = new Vector3[sigmentsNum + 1];    
        
        Vector3 averageVector = Vector3.Lerp(startPos, target, 0.5f);
        var pointBiaze_0 = startPos + new Vector3(0, heightPointBiaze_0, 0);
        var pointBiaze_1 = averageVector + new Vector3(0, heightPointBiaze_1, 0);

        for (int i = 0; i < sigmentsNum + 1; i++)
        {
            float param = (float)i / sigmentsNum;
            

            Vector3 point = Bezier.GetPoint(pointBiaze_0, pointBiaze_1, target, param);
            points[i] = point;
        }
        MovePoint.Instance.ActiveDeative(true, target);
        _lineArc.positionCount = points.Length;
        _lineArc.SetPositions(points);
    }

    public void RemoveLines()
    {
        _lineArc.positionCount = 0;
    }

    public void ActiveDeactiveLine(bool activate, Transform parent = null)
    {
        _lineArc.gameObject.SetActive(activate);
        MovePoint.Instance.ActiveDeative(activate);
    }

    public void CanMove(bool isCan)
    {
        //lineRenderer.colorGradient = canBe ? _trueColor: _falseColor;
    }
}


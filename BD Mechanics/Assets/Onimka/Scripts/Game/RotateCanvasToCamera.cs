using UnityEngine;

public class RotateCanvasToCamera : MonoBehaviour
{   
    private Transform mainCamera;

    private void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    private void Update()
    {
        AlignCamera();
    }

    private void AlignCamera()
    {
        if (mainCamera != null)
        {
            var forward = transform.position - mainCamera.position;
            forward.Normalize();
            var up = Vector3.Cross(forward, mainCamera.right);
            transform.rotation = Quaternion.LookRotation(forward, up);
        }
    }   
}

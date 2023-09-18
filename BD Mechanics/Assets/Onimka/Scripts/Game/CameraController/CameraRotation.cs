using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity = 3.0f;
    [SerializeField] private float _distanceFromTarget = 3.0f;
    [SerializeField] private float _zoom = 17.0f;
    [SerializeField] private float _minZoom = 8.0f;
    [SerializeField] private float _maxZoom = 21.0f;

    private float _rotationY;
    private float _rotationX;

    [SerializeField] private Transform _target;
    [SerializeField] private Transform _camera;

    private Vector3 _currentRotation;
    private Vector3 _smoothVelocity = Vector3.zero;

    [SerializeField]
    private float _smoothTime = 0.2f;

    [SerializeField]
    private Vector2 _rotationXMinMax = new Vector2(-40, 40);

    private void Update()
    {
        OnRotate();
        OnZoom();
    }

    private void OnRotate()
    {
        if (Input.GetMouseButton(2))
        {
            float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

            _rotationY += mouseX;
            _rotationX += mouseY;

            _rotationX = Mathf.Clamp(_rotationX, _rotationXMinMax.x, _rotationXMinMax.y);

            Vector3 nextRotation = new Vector3(_rotationX, _rotationY);

            _currentRotation = Vector3.SmoothDamp(_currentRotation, nextRotation, ref _smoothVelocity, _smoothTime);
            transform.localEulerAngles = _currentRotation;
        }
        transform.position = _target.position - transform.forward * _distanceFromTarget;
        
    }

    private void OnZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        _zoom -= scroll * 2;
        _zoom = Mathf.Clamp(_zoom, _minZoom, _maxZoom);
        _camera.position = transform.position - _camera.forward * _zoom;
    }
}

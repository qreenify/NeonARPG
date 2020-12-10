using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Distances")]
    [Range(2f,20f)]
    public float distance = 5f;
    public float minDistance = 1f;
    public float maxDistance = 7f;

    public Vector3 offset;
    [Header("Speeds")]
    public float smoothSpeed = 5f;
    public float scrollSensitivity = 1;

    private static CameraFollow _camera;
       

    void Start()
    {
        if (_camera != null)
        {
            Destroy(_camera.gameObject);
            DontDestroyOnLoad(gameObject);
            _camera = this;
            if (PlayerController.playerController == null) return;
            target = _camera.target != null ? _camera.target : PlayerController.playerController.transform;
            PlayerController.playerController.camera = GetComponent<Camera>();
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            _camera = this;
            if (PlayerController.playerController == null) return;
            target = PlayerController.playerController != null ? PlayerController.playerController.transform : null;
            PlayerController.playerController.camera = GetComponent<Camera>();
        }
    }

    private void LateUpdate()
    {
        if (!target)
        {
            print("NO TARGET FOR CAMERA");
                return;

        }

        float num = Input.GetAxis("Mouse ScrollWheel");
        distance -= num * scrollSensitivity;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        Vector3 pos = target.position + offset;
        pos -= transform.forward * distance;

        transform.position = Vector3.Lerp(transform.position, pos, smoothSpeed * Time.deltaTime);

    }
}

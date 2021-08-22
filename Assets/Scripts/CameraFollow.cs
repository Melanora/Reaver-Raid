using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    private Transform cameraTransform;
    private Vector3 pos;

    void Awake()
    {
        cameraTransform = gameObject.GetComponent<Transform>();
    }

    void LateUpdate()
    {
        pos = target.position + offset;
        pos.x = 0;
        cameraTransform.position = pos;
    }

}

//using System;
//using UnityEngine;
//using UnityEngine.InputSystem;

//public class CameraController : MonoBehaviour
//{
//    [SerializeField] Transform target; // The object around which the camera will rotate
//    [SerializeField] float distanceToTarget = 5.0f;
//    [SerializeField] float rotationSpeed = 100;

//    [SerializeField] Camera cam;
//    private Vector3 previousPosition;

//    void Start()
//    {
////        cam = GetComponent<Camera>();
//    }
//    void Update()
//    {
//        if (Input.GetMouseButtonDown(0))
//        {
//            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
//        }
//        else if (Input.GetMouseButton(0))
//        {
//            Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
//            Vector3 direction = previousPosition - newPosition;

//            float rotationAroundYAxis = -direction.x * rotationSpeed;
//            float rotationAroundXAxis = direction.y * rotationSpeed;

//            // Rotate the camera around its current position
//            cam.transform.RotateAround(cam.transform.position, Vector3.up, rotationAroundYAxis);
//            cam.transform.RotateAround(cam.transform.position, cam.transform.right, rotationAroundXAxis);

//            previousPosition = newPosition;
//        }
//    }
//}
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // The object around which the camera will rotate
    public float rotationSpeed = 5.0f;
    public float zoomSpeed ;
    public float minZoomDistance = 2.0f;
    public float maxZoomDistance = 10.0f;

    void Update()
    {
        // Rotate camera with mouse drag
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

            RotateCamera(mouseX, mouseY);
        }

        // Zoom in/out with mouse scroll wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        ZoomCamera(scroll);
    }

    void RotateCamera(float mouseX, float mouseY)
    {
        // Rotate the camera around the target
        transform.RotateAround(target.position, Vector3.up, mouseX);
        transform.RotateAround(target.position, transform.right, -mouseY);

        // Ensure the camera is always looking at the target
        transform.LookAt(target);
    }

    void ZoomCamera(float scroll)
    {
        // Calculate zoom amount
        float zoomAmount = scroll * zoomSpeed;

        // Adjust the camera's position based on the zoom amount
        transform.Translate(Vector3.forward * zoomAmount);

        // Clamp the camera position to the specified zoom limits
        transform.position = new Vector3(
            transform.position.x,
            Mathf.Clamp(transform.position.y, minZoomDistance, maxZoomDistance),
            transform.position.z
        );
    }
}

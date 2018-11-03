using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float cameraSpeed = 120.0f;
    public GameObject toFollow;
    public float upperClamp = 80.0f;
    public float lowerClamp = -80.0f;
    public float inputSens = 150.0f;
    private float distanceX;
    private float distanceY;
    private float distanceZ;
    private float rotationY;
    private float rotationX;
    private float smoothinX;
    private float smoothinY;
    private float usrinputX;
    private float usrinputY;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 rotation = transform.localRotation.eulerAngles;
        rotationY = rotation.y;
        rotationX = rotation.x;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            usrinputX = Input.GetAxis("Mouse Y");
            usrinputY = Input.GetAxis("Mouse X");
            rotationX += usrinputX * inputSens * Time.deltaTime;
            rotationY += usrinputY * inputSens * Time.deltaTime;
            rotationX = Mathf.Clamp(rotationX, lowerClamp, upperClamp);
            transform.rotation = Quaternion.Euler(rotationX, rotationY, 0.0f);
        }
    }

    private void LateUpdate()
    {
        Transform target = toFollow.transform;
        transform.position = Vector3.MoveTowards(transform.position, target.position, cameraSpeed * Time.deltaTime);
    }
}

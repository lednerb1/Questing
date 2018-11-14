using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct POINT
{
    public int x;
    public int y;
}

public class CameraController : MonoBehaviour
{

    public float cameraSpeed = 120.0f;
    public GameObject toFollow;
    public GameObject heightFollow;
    public Texture2D cursorTexture;
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
    private Transform target;
    private Vector3 updateVector;
    private POINT mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 rotation = transform.localRotation.eulerAngles;
        rotationY = rotation.y;
        rotationX = rotation.x;
        //Cursor.SetCursor
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            GetCursorPos(out mousePosition);   
        }
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            Cursor.lockState = CursorLockMode.None;
            SetCursorPos(mousePosition.x, mousePosition.y);
            Cursor.visible = true;
        }

        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
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
        target = toFollow.transform;
        updateVector = target.position;
        updateVector.y = heightFollow.transform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, updateVector, cameraSpeed * Time.deltaTime);
        
    }

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool GetCursorPos(out POINT lpPoint);

    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int X, int Y);

}


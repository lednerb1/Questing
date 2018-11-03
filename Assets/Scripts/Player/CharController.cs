using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour {

    public float speed = 10.0f;
    private CharacterController cc;
    public GameObject camera;
    public float gravity = 10.0f;
    public float rotationSpeed = 120.0f;
    public float jumpPower = 10.0f;
    private float beginJump = 0.0f;
    private float acc = 1f;
    private Vector3 toLook;

    // Start is called before the first frame update
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {

        cc.transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal"), 0) * Time.deltaTime * rotationSpeed);
        cc.Move(cc.transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * speed);

        //Begin A: If Mouse2 is pressed then make player rotation follow camera
        if (Input.GetMouseButton(1))
        {
            toLook = camera.transform.forward;
            toLook.y = 0;
            if (toLook.sqrMagnitude != 0.0f)
                cc.transform.LookAt(cc.transform.position + toLook);
            cc.Move(cc.transform.right * Input.GetAxis("Horizontal") * Time.deltaTime * speed);
        }
        //End A.

        if (Time.time - beginJump > 0.27f) {
            cc.Move(new Vector3(0, -gravity * Time.deltaTime * acc, 0));
            acc += 0.04f;
        }
        else
        {
            cc.Move(new Vector3(0, gravity * Time.deltaTime * jumpPower * acc, 0));
            acc += 0.01f;
        }

        if (cc.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            beginJump = Time.time;
            acc = 1f;
        }

        //Debug.Log(cc.transform.position);

    }
}

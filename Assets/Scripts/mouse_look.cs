using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse_look : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public Transform playerBody;

    float x_rotation = 0f;
    float mouse_x;
    float mouse_y;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        mouse_x = 0f;
        mouse_y = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        mouse_x = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouse_y = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        x_rotation -= mouse_y;
        x_rotation = Mathf.Clamp(x_rotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(x_rotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouse_x);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse_look : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public Transform playerBody;

    float x_rotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouse_x = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouse_y = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        x_rotation -= mouse_y;
        x_rotation = Mathf.Clamp(x_rotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(x_rotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouse_x);
    }
}

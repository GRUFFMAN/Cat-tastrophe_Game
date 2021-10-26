using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMouseMovement : MonoBehaviour
{
    public Transform[] cameraPoints;
    Vector2 prevmousepos = Vector2.zero;
    public float dampener = 0.5f;
    Quaternion originalRotation;

    // Start is called before the first frame update
    void Start()
    {
        Transform point = cameraPoints[Random.Range(0, cameraPoints.Length)];
        transform.position = point.position;
        transform.rotation = point.rotation;
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousepos = Input.mousePosition;
        if (prevmousepos == Vector2.zero)
        {
            prevmousepos = mousepos;
        }

        Vector2 dmouse = mousepos - prevmousepos;
        dmouse = dmouse * Time.deltaTime * dampener;
        transform.Rotate(-dmouse.y, dmouse.x, 0);
        //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, originalRotation.z);
        
        //eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, originalRotation.z);
        prevmousepos = mousepos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float speed = 10f;
    private float minDist = 15f;
    private float maxDist = 90f;
    private float sensitivity = 10f;

    void Update()
    {
        float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        transform.position = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        float fov = Camera.main.fieldOfView;
        fov += -Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minDist, maxDist);
        Camera.main.fieldOfView = fov;
    }
}

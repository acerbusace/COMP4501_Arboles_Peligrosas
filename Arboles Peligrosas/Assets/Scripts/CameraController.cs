using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float speed = 10f;
    private float scrollSpeed = 10f;
    private float minDist = 10f;
    private float maxDist = 35f;
    private float sensitivity = 10f;

    private float zSnapOffset = -15;

    public SelectController selectController;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            GameObject unit = selectController.getSelctedFrame();
            if (unit != null)
                transform.position = new Vector3(unit.transform.position.x, transform.position.y, unit.transform.position.z + zSnapOffset);
        }


        float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        transform.position = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        //float fov = Camera.main.fieldOfView;
        //fov += -Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        //fov = Mathf.Clamp(fov, minDist, maxDist);
        //Camera.main.fieldOfView = fov;

        float scroll = Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        if (scroll != 0f) {
            float y = scrollSpeed * speed * Time.deltaTime;
            if (scroll > 0f && transform.position.y > minDist)
                transform.position = new Vector3(transform.position.x, transform.position.y - y, transform.position.z);
            else if (scroll < 0f && transform.position.y < maxDist)
                transform.position = new Vector3(transform.position.x, transform.position.y + y, transform.position.z);
        }
    }
}

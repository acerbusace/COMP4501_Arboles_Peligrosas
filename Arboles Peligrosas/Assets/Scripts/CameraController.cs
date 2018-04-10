using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float acceleration;
    private float baseAcceleration = 5f;
    public float deceleration;

    private Vector3 velocity = new Vector3();
    private float maxVelocity = 30f;
    private float minDist = 5f;
    private float maxDist = 30f;

    private float speed = 25f;
    private float minSpeed = 10f;
    private float maxSpeed = 35f;

    private float scrollSensitivity = 100f;

    private float zSnapOffset = -15; // z-axis offset for camera snapping
    private bool follow = false;

    public SelectController selectController;

    void Start()
    {
        transform.position = new Vector3(transform.position.x, maxDist, transform.position.z);
    }

    void Update()
    {
        // Spacebar is Pressed
        // Then snap to the last selected unit
        if (Input.GetKeyDown(KeyCode.Space))
            follow = true;
        else if (Input.GetKeyUp(KeyCode.Space))
            follow = false;

        if (follow) {
            GameObject unit = selectController.getLastSelctedFrame();
            if (unit != null) {
                transform.position = new Vector3(unit.transform.position.x, transform.position.y, unit.transform.position.z + zSnapOffset);
                //velocity = new Vector3();
            }
        }

        float scrollPrecent = (transform.position.y - minDist) / (maxDist - minDist);
        float speed = Mathf.Lerp(minSpeed, maxSpeed, scrollPrecent);

        Vector3 velocity = new Vector3(
            Input.GetAxis("Horizontal"),
            0f,
            Input.GetAxis("Vertical")
        );

        //transform.position += velocity.normalized * speed * Time.deltaTime;
        transform.position += velocity * speed * Time.deltaTime;

        transform.position += new Vector3(
            0f,
            -(Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity * speed * Time.deltaTime),
            0f
        );

        // make sure camera's y position is not out of min/max distance
        if (transform.position.y > maxDist) transform.position = new Vector3(transform.position.x, maxDist, transform.position.z);
        if (transform.position.y < minDist) transform.position = new Vector3(transform.position.x, minDist, transform.position.z);

    }
}

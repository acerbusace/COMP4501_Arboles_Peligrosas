using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float acceleration;
    public float deceleration;

    private Vector3 velocity = new Vector3();
    private float maxVelocity = 100f;
    private float minDist = 10f;
    private float maxDist = 35f;

    private float scrollSensitivity = 100f;

    private float zSnapOffset = -15; // z-axis offset for camera snapping

    public SelectController selectController;

    void Update()
    {
        // Spacebar is Pressed
        // Then snap to the last selected unit
        if (Input.GetKeyDown(KeyCode.Space)) {
            GameObject unit = selectController.getLastSelctedFrame();
            if (unit != null) {
                transform.position = new Vector3(unit.transform.position.x, transform.position.y, unit.transform.position.z + zSnapOffset);
                velocity = new Vector3();
            }
        }

        // sets velocity based on input axis
        velocity += new Vector3(
            Input.GetAxis("Horizontal") * acceleration * Time.deltaTime,
            -(Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity * acceleration * Time.deltaTime), 
            Input.GetAxis("Vertical") * acceleration * Time.deltaTime
        );

        // make sure velocity doesn't exceed max value
        if (velocity.x > maxVelocity) velocity.x = maxVelocity;
        if (velocity.z > maxVelocity) velocity.z = maxVelocity;

        // decelerate, if input axis are not pressed
        if (Input.GetAxis("Horizontal") == 0)
            velocity.x /= 1f + (deceleration * Time.deltaTime);
        if (Input.GetAxis("Vertical") == 0)
            velocity.z /= 1f + (deceleration * Time.deltaTime);
        if (Input.GetAxis("Mouse ScrollWheel") == 0)
            velocity.y /= 1f + (deceleration * Time.deltaTime);

        transform.position += velocity * Time.deltaTime;

        // make sure camera's y position is not out of min/max distance
        if (transform.position.y > maxDist) transform.position = new Vector3(transform.position.x, maxDist, transform.position.z);
        if (transform.position.y < minDist) transform.position = new Vector3(transform.position.x, minDist, transform.position.z);
    }
}

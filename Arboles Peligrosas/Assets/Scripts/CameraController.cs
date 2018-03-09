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
    private float minDist = 10f;
    private float maxDist = 35f;

    private float speed = 25f;

    private float scrollSensitivity = 100f;

    private float zSnapOffset = -15; // z-axis offset for camera snapping
    private bool follow = false;

    public SelectController selectController;

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

        transform.position += new Vector3(
            Input.GetAxis("Horizontal") * speed * Time.deltaTime,
            -(Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity * speed * Time.deltaTime),
            Input.GetAxis("Vertical") * speed * Time.deltaTime
        );

        // make sure camera's y position is not out of min/max distance
        if (transform.position.y > maxDist) transform.position = new Vector3(transform.position.x, maxDist, transform.position.z);
        if (transform.position.y < minDist) transform.position = new Vector3(transform.position.x, minDist, transform.position.z);

        /*float x = Input.GetAxis("Horizontal") * acceleration * Time.deltaTime;
        float y = -(Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity * acceleration * Time.deltaTime);
        float z = Input.GetAxis("Vertical") * acceleration * Time.deltaTime;

        if (velocity.x == 0) print("val is zero!!!");
        else
            print(velocity);

        // sets velocity based on input axis
        velocity += new Vector3(
            velocity.x == 0 && x != 0 ? (x > 0 ? baseAcceleration : -baseAcceleration) : x,
            -(Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity * acceleration * Time.deltaTime),
            velocity.z == 0 && z != 0 ? (z > 0 ? baseAcceleration : -baseAcceleration) : z
        );



        // make sure velocity doesn't exceed max value
        if (velocity.x > maxVelocity) velocity.x = maxVelocity;
        if (velocity.x < -maxVelocity) velocity.x = -maxVelocity;
        if (velocity.z > maxVelocity) velocity.z = maxVelocity;
        if (velocity.z < -maxVelocity) velocity.z = -maxVelocity;

        // decelerate, if input axis are not pressed
        if (Input.GetAxis("Horizontal") == 0) {
            velocity.x /= 1f + (deceleration * Time.deltaTime);
            if (Mathf.Abs(velocity.x) < 1f) velocity.x = 0f;
        }
        if (Input.GetAxis("Vertical") == 0) {
            velocity.z /= 1f + (deceleration * Time.deltaTime);
            if (Mathf.Abs(velocity.z) < 1f) velocity.z = 0f;
        }
        if (Input.GetAxis("Mouse ScrollWheel") == 0) {
            velocity.y /= 1f + (deceleration * Time.deltaTime);
            if (Mathf.Abs(velocity.y) < 1f) velocity.y = 0f;
        }

        transform.position += velocity * Time.deltaTime;*/
    }
}

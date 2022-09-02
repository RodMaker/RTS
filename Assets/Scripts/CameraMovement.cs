using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    float speed = 0.06f;
    float zoomSpeed = 10.0f;
    float rotateSpeed = 0.1f;

    float maxHeight = 40.0f;
    float minHeight = 4.0f;

    Vector2 p1;
    Vector2 p2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 0.06f;
            zoomSpeed = 20.0f;
        }
        else
        {
            speed = 0.035f;
            zoomSpeed = 10.0f;
        }

        float horizontalSpeed = transform.position.y * speed * Input.GetAxis("Horizontal");
        float verticalSpeed = transform.position.y * speed * Input.GetAxis("Vertical");
        float scrollSpeed = Mathf.Log(transform.position.y) * -zoomSpeed * Input.GetAxis("Mouse ScrollWheel");

        if ((transform.position.y >= maxHeight) && (scrollSpeed > 0))
        {
            scrollSpeed = 0;
        }
        else if ((transform.position.y <= minHeight) && (scrollSpeed < 0))
        {
            scrollSpeed = 0;
        }

        if ((transform.position.y + scrollSpeed) > maxHeight)
        {
            scrollSpeed = maxHeight - transform.position.y;
        }
        else if ((transform.position.y + scrollSpeed) < minHeight)
        {
            scrollSpeed = minHeight - transform.position.y;
        }

        Vector3 verticalMove = new Vector3(0, scrollSpeed, 0);
        Vector3 lateralMove = horizontalSpeed * transform.right;
        Vector3 forwardMove = transform.forward;
        forwardMove.y = 0;
        forwardMove.Normalize();
        forwardMove *= verticalSpeed;

        Vector3 move = verticalMove + lateralMove + forwardMove;

        transform.position += move;

        GetCameraRotation();
    }

    void GetCameraRotation()
    {
        if (Input.GetMouseButtonDown(2)) // pressed
        {
            p1 = Input.mousePosition;

        }

        if (Input.GetMouseButton(2)) // held down
        {
            p2 = Input.mousePosition;

            float dx = (p2 - p1).x * rotateSpeed;
            float dy = (p2 - p1).y * rotateSpeed;

            transform.rotation *= Quaternion.Euler(new Vector3(0, dx, 0)); // y rotation
            transform.GetChild(0).transform.rotation *= Quaternion.Euler(new Vector3(-dy, 0, 0)); // x rotation

            p1 = p2;
        }
    }
}

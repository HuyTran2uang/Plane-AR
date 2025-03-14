using UnityEngine;

public class Target : MonoBehaviour
{
    private Vector2 prevTouch1, prevTouch2;
    private bool isPinching = false;
    private bool isRotating = false;
    private float rotationSpeed = 0.2f; // Điều chỉnh tốc độ xoay

    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                prevTouch1 = touch1.position;
                prevTouch2 = touch2.position;
                isPinching = true;
            }
            else if (isPinching && (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved))
            {
                // Zoom In/Out
                float prevDistance = Vector2.Distance(prevTouch1, prevTouch2);
                float currentDistance = Vector2.Distance(touch1.position, touch2.position);
                float zoomFactor = (currentDistance - prevDistance) * 0.01f; // Điều chỉnh tốc độ zoom
                transform.localScale += Vector3.one * zoomFactor;

                prevTouch1 = touch1.position;
                prevTouch2 = touch2.position;
            }
        }
        else if (Input.touchCount == 1) // Xoay bằng 1 ngón
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                isRotating = true;
            }
            else if (isRotating && touch.phase == TouchPhase.Moved)
            {
                float rotationX = touch.deltaPosition.y * rotationSpeed; // Xoay theo trục X khi vuốt dọc
                float rotationY = -touch.deltaPosition.x * rotationSpeed; // Xoay theo trục Y khi vuốt ngang

                transform.Rotate(Vector3.right, rotationX, Space.World); // Xoay theo trục X
                transform.Rotate(Vector3.up, rotationY, Space.World);    // Xoay theo trục Y
            }
        }
        else
        {
            isPinching = false;
            isRotating = false;
        }
    }
}

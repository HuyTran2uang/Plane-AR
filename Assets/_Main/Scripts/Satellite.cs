using UnityEngine;

public class Satellite : MonoBehaviour
{
    public float rotationSpeed = 10;

    private void FixedUpdate()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.localEulerAngles += Vector3.up * rotationSpeed * Time.deltaTime;
    }

#if UNITY_EDITOR
    public bool isShowGizmos;

    private void OnDrawGizmos()
    {
        if (!isShowGizmos) return;
        Rotate();
    }
#endif
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow sharedInstance;

    public Transform target;
    public Vector3 offset = new Vector3(0.6f, 0.5f,-10f);
    public float dampTime = 0.4f;
    public Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        sharedInstance = this;
        Application.targetFrameRate = 60;
    }

    public void ResetCameraPosition()
    {
        Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
        Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(offset.x, offset.y, point.z));
        Vector3 destination = point + delta;
        destination = new Vector3(target.position.x, offset.y, offset.z);
        transform.position = destination;
    }
    void Update()
    {
        Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
        Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(offset.x, offset.y, point.z));
        Vector3 destination = point + delta;
        destination = new Vector3(target.position.x, target.position.y, offset.z);
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private Transform targetToFollow;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothFactor;

    void Update()
    {
        Vector3 targetPosition = targetToFollow.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.deltaTime);
        transform.position = new Vector3(smoothPosition.x,smoothPosition.y,transform.position.z);
    }
}

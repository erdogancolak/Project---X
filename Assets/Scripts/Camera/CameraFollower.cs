using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [Header("References")]

    GameObject target;

    [Space]

    [Header("Settings")]

    [SerializeField] private float smoothSpeed;

    [SerializeField] Vector3 offset;

    public static bool isGameStart;
    void LateUpdate()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        if (isGameStart)
        {
            target = GameObject.FindWithTag("Player");
            Vector3 targetPosition = target.transform.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
            transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
        }
    }
}

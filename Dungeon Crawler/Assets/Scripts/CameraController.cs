using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offSett;

    private void Update()
    {
        transform.position = target.position + offSett;
    }
}



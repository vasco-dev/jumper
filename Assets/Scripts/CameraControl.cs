using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private void LateUpdate()
    {
        Vector3 playerPos = PlayerController.Instance.transform.position;
        // smooth
        Vector3 pos = new(transform.position.x, playerPos.y, transform.position.z);
        transform.position = pos;
    }
}

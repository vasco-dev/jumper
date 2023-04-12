using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // ammount to offset the center of the camera from the player
    [SerializeField]
    private float _playerOffset = 2f;

    // treshold for the max speed the camera can follow the player
    [SerializeField]
    private float _cameraMaxThresholdScale = 1f;
    private void LateUpdate()
    {
        Vector3 playerPos = PlayerController.Instance.transform.position;
        // TO-DO smooth
        float vel = PlayerController.Instance.Body.velocity.y;

        float posY = Mathf.SmoothDamp(transform.position.y, playerPos.y + _playerOffset, ref vel, 0.1f*_cameraMaxThresholdScale) ;

        Vector3 pos = new(transform.position.x, posY, transform.position.z);
        transform.position = pos;
    }
}

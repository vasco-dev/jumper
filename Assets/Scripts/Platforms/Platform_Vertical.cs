using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Vertical : Platform
{
    // max diference the platform will have from its initial position, in units
    [SerializeField]
    private float _amplitude = 1f;

    // how fast will the platform perform the movement
    [SerializeField]
    private float _speedScale = 1f;

    // the initial position of the platform
    private Vector3 _startPosition;

    private void Start()
    {
        StartCoroutine(VerticalMove());
    }

    private IEnumerator VerticalMove()
    {
        // get the initial position
        _startPosition = transform.position;

        while (gameObject.activeSelf)
        {
            // create the offset from the initial position using sine function
            Vector3 offset = new(0, _amplitude * Mathf.Sin(_speedScale * Time.time), 0);
            // apply the offset
            transform.position = _startPosition + offset;

            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }

}

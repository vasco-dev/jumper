using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_RoofWall : Platform
{
    // reference to the roof or wall to destroy
    [SerializeField]
    private GameObject _roofOrWall;

    protected override void OnPlayerLanded()
    {
        if (_roofOrWall != null)
        {
            Destroy(_roofOrWall.gameObject);
            _roofOrWall = null;
        }
    }
}

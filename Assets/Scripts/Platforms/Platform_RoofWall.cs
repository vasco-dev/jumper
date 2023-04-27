using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_RoofWall : Platform
{
    // reference to the roof or wall to destroy
    [SerializeField]
    private GameObject _roofOrWall;

    public override void SetIsRight(bool isRightToSet)
    {
        IsRight = isRightToSet;
        transform.localScale = new(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
    protected override void OnPlayerLanded()
    {
        if (_roofOrWall != null)
        {
            Destroy(_roofOrWall.gameObject);
            _roofOrWall = null;
        }
    }
}

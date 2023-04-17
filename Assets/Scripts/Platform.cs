using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public int Index { get; private set; } = 0;
    public bool IsRight { get; private set; } = false;




    public void SetIndex(int indexToSet){
        Index = indexToSet;
    }
    public void SetIsRight(bool isRightToSet){
        IsRight = isRightToSet;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public int Index { get; private set; } = 0;




    public void SetIndex(int indexToSet){
        Index = indexToSet;
    }
}

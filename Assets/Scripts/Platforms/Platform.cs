using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Platform : MonoBehaviour
{
    // the specific index of this platfrom for the platform manager
    public int Index { get; private set; } = 0;
    // is this platform in the right side of the screen
    public bool IsRight { get; private set; } = false;

    public void SetIndex(int indexToSet){
        Index = indexToSet;
    }
    public void SetIsRight(bool isRightToSet){
        IsRight = isRightToSet;
    }

    private void OnTriggerEnter(Collider collision)
    {
        // check if the collision is the player
        collision.TryGetComponent<PlayerController>(out PlayerController checkPlayer);

        if(checkPlayer != null)
        {


            //TO-DO: ADD SCORE

            checkPlayer.SetCheckpoint(Index);
            OnPlayerLanded();
        }
    }

    // behaviour to run for platforms that have specific behaviours
    protected virtual void OnPlayerLanded()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private bool hasBeenTouched;
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
        if (!hasBeenTouched)
        {
            // check if the collision is the player
            collision.TryGetComponent<PlayerController>(out PlayerController checkPlayer);

            if (checkPlayer != null)
            {
                hasBeenTouched = true;
                GameManager.Instance.AddScore(10);
                AudioManager.Instance.Play("Score");
                PlatformManager.Instance.SetCheckpoint(this);
                OnPlayerLanded();
            }
        }
        //if goes wrong put inside playercheck
        AudioManager.Instance.Play("LandingImpact");
    }

    // behaviour to run for platforms that have specific behaviours
    protected virtual void OnPlayerLanded()
    {

    }
}

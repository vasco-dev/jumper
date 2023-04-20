using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformContact : MonoBehaviour
{
    private bool touchedPlatform;
    private bool deactivate;
    private float deactivationCounter;
    private bool isRespawnPoint;

    void OnEnable()
    {
        touchedPlatform = false;
        deactivate = false;
        isRespawnPoint = false;
    }

    void Update()
    {
        if (deactivate)
        {
            deactivationCounter += Time.deltaTime;
        }
        if(deactivationCounter > 3f)
        {
            isRespawnPoint = false;
        }
        if (isRespawnPoint)
        {
            Debug.Log("Inside the spot");
            GameManager.Instance.SetRespawnPosition(gameObject.transform.position);
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            //gameObject.GetComponentInChildren<Transform>().gameObject.SetActive(true);
        }
        if (!isRespawnPoint) gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            deactivate = false;
            deactivationCounter = 0f;
            isRespawnPoint = true;
            if (!touchedPlatform)
            {
                touchedPlatform = true;
                GameManager.Instance.AddScore(10);
            }
            AudioManager.Instance.Play("LandingImpact");
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            deactivate = false;
            deactivationCounter = 0f;
            isRespawnPoint = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player")) deactivate = true;
    }
}

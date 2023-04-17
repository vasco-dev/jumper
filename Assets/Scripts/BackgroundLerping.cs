using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLerping : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    private Material material;
    private float currentLerping;
    private float reset;
    private float previousPos;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().sharedMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.transform.position.y > previousPos)
        {
            previousPos = Player.transform.position.y;
            currentLerping = (Player.transform.position.y / 100) - reset;
            if (currentLerping > 0.99f)
            {
                reset++;
            }
            material.SetFloat("_Lerping", currentLerping);
        }
    }

    public void ResetBackground()
    {
        currentLerping = 0f;
        reset = 0f;
        previousPos = 0f;
        material.SetFloat("_Lerping", 0f);
        Player.transform.position = new Vector3(0, 0, 0);
    }
}

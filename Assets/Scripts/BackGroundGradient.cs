using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundGradient : MonoBehaviour
{
    MeshRenderer backGroundMesh;
    [SerializeField] [Range(0f, 1f)] float lerpTime;
    [SerializeField] Color[] colors;

    int colorIndex = 0;
    private float currentLerping;
    private float playerpos;
    Color firstMat;


    // Start is called before the first frame update
    void Start()
    {
        backGroundMesh = GetComponent<MeshRenderer>();
        firstMat = backGroundMesh.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        //backGroundMesh.material.color = Color.Lerp(backGroundMesh.material.color, colors[colorIndex], currentLerping);
        //if(playerpos )
        if(colorIndex < 1)
        {
            backGroundMesh.material.color = Color.Lerp(firstMat, colors[colorIndex], currentLerping);
        }
        else
        {
            backGroundMesh.material.color = Color.Lerp(colors[colorIndex - 1], colors[colorIndex], currentLerping);
        }
        HandlePlayerPosition();
    }

    private void HandlePlayerPosition()
    {
        playerpos += 0.1f;
        if (playerpos > 150) playerpos = 50;
        Debug.Log("Current Player Position: " + playerpos);
        colorIndex = Mathf.FloorToInt(playerpos / 100);
        Debug.Log("Current Color Index: " + colorIndex);
        Debug.Log("CurrentLerping: " + currentLerping);
        currentLerping = (playerpos / 100) - colorIndex;

        /*if player pos for maior q last player pos entao ele tem de encrementar
         * mas se player pos for menor do q lastplayerpos entao passa a decrementar*/

        //backGroundMesh.material.color = Color.Lerp(
    }
}

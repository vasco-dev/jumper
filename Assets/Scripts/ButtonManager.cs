using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }

    public void ShowTutorial()
    {
        GameManager.Instance.ShowTutorial();
    }

    public void HideTutorial()
    {
        GameManager.Instance.HideTutorial();
    }
}

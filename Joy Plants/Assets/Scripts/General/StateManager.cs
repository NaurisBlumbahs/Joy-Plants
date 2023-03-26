using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarterAssets
{ 
public class StateManager : MonoBehaviour
{
    [SerializeField]
    GameObject mainMenuCanvas;

    public void onStartGame()
    {
         mainMenuCanvas.SetActive(false);
    }
    public void onExitGame()
    {
        Application.Quit();
    }
}
}

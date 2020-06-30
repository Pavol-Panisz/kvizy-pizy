using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoMainMenu : MonoBehaviour
{
    public void MainMenu()
    {
        GameManager gm;
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        
        if (gm) { gm.LoadScene(0); }
    }
}

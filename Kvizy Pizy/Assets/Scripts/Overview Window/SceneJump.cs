using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneJump : MonoBehaviour
{
    private GameManager gm;
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();   
    }

    public void SetQuizToPlayFromText(Text txt) { gm.SetQuizToPlayFromText(txt); }
    public void LoadScene(int n) { gm.LoadScene(n); }
    
}

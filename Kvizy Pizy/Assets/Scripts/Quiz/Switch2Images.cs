using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Switch2Images : MonoBehaviour
{

    [SerializeField] Image imageToSwitch;
    [SerializeField] Sprite a, b;

    void Start()
    {
        imageToSwitch.sprite = a;
    }

    public void Switch() 
    {
        if (imageToSwitch.sprite == a) { imageToSwitch.sprite = b; }
        else { imageToSwitch.sprite = a; }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//without this, you wouldn't see the Question field in the editor, on the gameobject called "Test"
[System.Serializable]
public class Question
{
    public string question; //..How many months are there in a year?
    public int index; //........1
    public float time; //.......5
    public string correct; //...AC
    
    public string optionA; //...12
    public string optionB; //...10
    public string optionC; //...twelve
    public string optionD; //...nine
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileIO : MonoBehaviour
{
    public GameObject createQuizButton;

    string currentQuizName;
    public void SetQuizName(string str) { currentQuizName = str; }
    
    public void CreateQuiz() {
        string path = Application.dataPath + "/Resources/Kvízy/" + currentQuizName;
        if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }
        else { Debug.Log("This directory already exists"); }
    }
    public void DeleteQuiz() { }
    public void CreateInfo() { }
    public void WriteInfo() { }
    public void CreateQuestion() { }
    public void WriteQuestion() { }

    private void Update()
    {
        if (Directory.Exists(currentQuizName)) {  }

    }
}

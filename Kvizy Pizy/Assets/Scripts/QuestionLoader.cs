using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionLoader : MonoBehaviour
{
    public Question question;
    private void Awake()
    {
        string loadedQuestion = JsonFileReader.LoadJsonAsResource("Kvízy/Kvíz_1/1.json");
        Debug.Log(loadedQuestion);

        question = JsonUtility.FromJson<Question>(loadedQuestion);
    }
}
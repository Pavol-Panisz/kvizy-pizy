using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class Overview : MonoBehaviour
{
    [Header("Fieds")]
    [SerializeField] private InputField nameOfQuizField;
    [SerializeField] private InputField quizDescField;
    [SerializeField] private Text timeText;
    [SerializeField] private Text questionCountText;

    [Header("Question Creator")] [SerializeField] private QuestionCreator questionCreator;
    
    string quizName;
    float overallQuizTime = 0f;
    float questionCount = 0f;
    GameManager gm;
    StorageSys st;

    private void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        st = GameObject.FindGameObjectWithTag("GameManager").GetComponent<StorageSys>();
        if (questionCreator == null) { Debug.Log("no question creator assigned!"); }
    }

    private void Start()
    { 

    }

    /// <summary>
    /// Updates the info.json file for the quiz named quizName in questionCreator
    /// </summary>
    public void UpdateOverview()
    {
        overallQuizTime = 0f;
        questionCount = 0f;
        string path = gm.quizzesDir + "/" + quizName + "/Otázky/";


        //calculate the overall quiz time and the quiz' question count 
        try
        {
            IEnumerable<string> jsonFilePaths = Directory.EnumerateFiles(path, "*.json");

            foreach (string filePath in jsonFilePaths)
            {
                //Debug.Log(filePath);
                st.LoadQuestion(filePath);
                overallQuizTime += st.otazka.cas;
                questionCount++;
            }
        }
        catch (Exception exc)
        {
            Debug.Log("there was an exception:" + exc.Message);
        }

        //Debug.Log("celkovy cas kvizu:" + overallQuizTime.ToString() + " pocet otazok: " + questionCount.ToString());
        SetValuesIntoFields();
    }

    /// <summary>
    /// Puts the info.json values of the quiz named 'quizName'
    /// into the fields of the Overview Window
    /// </summary>
    public void SetValuesIntoFields()
    {
        string path = gm.quizzesDir + "/" + quizName + "/info.json";
        st.LoadInfo(path);
        
        nameOfQuizField.text = st.info.nazov; Debug.Log("just set the name into the field: \"" + st.info.nazov + "\"");
        quizDescField.text = st.info.opis;
        timeText.text = overallQuizTime.ToString();
        questionCountText.text = questionCount.ToString();
    }

    public void SetQuizName(string str) { quizName = str; }

    public void SetQuizNameFromText(Text txt) { quizName = txt.text; }
}

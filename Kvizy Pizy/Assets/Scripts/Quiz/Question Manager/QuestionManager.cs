using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class QuestionManager : MonoBehaviour
{
    GameManager gm = null;
    StorageSys st = null;
    private List<Otazka> questionList = new List<Otazka>();
    string playingQuiz = "Default - NEMAZAŤ";
    char sep = Path.DirectorySeparatorChar;

    int currentQuestion = 0;
    private float currentQuestionDuration;
    private string currentCorrectAnswers;

    [SerializeField] Color winningColor;
    [SerializeField] private Image[] ImagesToColor = new Image[4];

    [SerializeField] private Text AnswerA;
    [SerializeField] private Text AnswerB;
    [SerializeField] private Text AnswerC;
    [SerializeField] private Text AnswerD;
    [SerializeField] private Text HeaderText;
    [SerializeField] private Text QuestionNumIndicator;

    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private WindowManager windowManager;

    [SerializeField] private GameObject windowToCloseUponEnd, windowToOpenUponEnd;
    [SerializeField] private Countdown countdownBarScript;

    void Start()
    {

        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        st = GameObject.FindGameObjectWithTag("GameManager").GetComponent<StorageSys>();
        playingQuiz = gm.GetQuizToPlay();
        string path = gm.quizzesDir + sep + playingQuiz + sep + "Otázky";

        //populate the questionList
        IEnumerable<string> jsonFiles = Directory.EnumerateFiles(path, "*.json");
        Otazka ot;
        foreach(string jsonFilePath in jsonFiles) {

            Debug.Log(jsonFilePath);

            ot =  st.LoadAndReturnQuestion(jsonFilePath);
            questionList.Add(ot);
        }

        ShowQuestion(currentQuestion);
    }

    public void ShowQuestion(int n)
    {
        n--;
        if (playingQuiz != null)
        {
            /*string q = n.ToString() + ".json";
            string path = gm.quizzesDir + sep + playingQuiz + sep + "Otázky" + sep + q;

            if (File.Exists(path))
            {
                st.LoadQuestion(path);
                HeaderText.text = st.otazka.otazka;
                AnswerA.text = st.otazka.odpovedA;
                AnswerB.text = st.otazka.odpovedB;
                AnswerC.text = st.otazka.odpovedC;
                AnswerD.text = st.otazka.odpovedD; 


            }
            else 
            {
                Debug.Log("This quiz has no question number " + q);
                windowManager.CloseWindow(windowToCloseUponEnd);
                windowManager.OpenWindow(windowToOpenUponEnd);
            }
        }
        else { Debug.Log("We're currently not playing any quiz, its just the scene thats open"); }*/

            HeaderText.text = questionList[n].otazka;
            AnswerA.text = questionList[n].odpovedA;
            AnswerB.text = questionList[n].odpovedB;
            AnswerC.text = questionList[n].odpovedC;
            AnswerD.text = questionList[n].odpovedD;

            currentQuestionDuration = questionList[n].cas;
            currentCorrectAnswers = questionList[n].spravnaOdpoved;
        }
    }

    public void ShowQuestionFromText(Text txt) { ShowQuestion(int.Parse(txt.text)); }

    public void ShowNext() { 
        currentQuestion++;

        if (currentQuestion <= questionList.Count) 
        {
            UpdateIndicator();
            ShowQuestion(currentQuestion);
            playerManager.ResetPositions();
            RestartTimerTilNext();
        }
        else
        {
            Debug.Log("This quiz has no question number " + currentQuestion.ToString());
            windowManager.CloseWindow(windowToCloseUponEnd);
            windowManager.OpenWindow(windowToOpenUponEnd);
        }
    }

    private void UpdateIndicator()
    {
        string str = currentQuestion.ToString() + " / " + questionList.Count.ToString();
        QuestionNumIndicator.text = str;
    }

    private void RestartTimerTilNext()
    {
        countdownBarScript.Restart(currentQuestionDuration); //the timer, once finished, calls ShowNext()
    }
}

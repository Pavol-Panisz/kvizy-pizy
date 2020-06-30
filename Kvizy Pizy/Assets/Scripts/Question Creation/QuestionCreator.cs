using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
public class QuestionCreator : MonoBehaviour
{
    [SerializeField] private Text questionText;
    [SerializeField] private Text optionAText;
    [SerializeField] private Text optionBText;
    [SerializeField] private Text optionCText;
    [SerializeField] private Text optionDText;
    [SerializeField] private Text timeText;
    [SerializeField] private Text questionNumberText;

    [Header("Answer Checkmarks")]
    [SerializeField] private Toggle[] toggles = new Toggle[4];
    [SerializeField] private string answersStr;
    
    [Header("Overview")]
    [SerializeField] private Overview overviewScript;

    private StorageSys st;
    private GameManager gm;
    private string quizName;
    
    //used to determine, whether the current Question has been filled out completely.
    //note that answersStr is also taken into accout, in the method AreAllFieldsFilledOut
    private Text[] textFieldInputs = new Text[6];

    private void Awake()
    {
        st = GameObject.FindGameObjectWithTag("GameManager").GetComponent<StorageSys>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        foreach (Toggle tg in toggles)
        {
            tg.onValueChanged.AddListener(delegate { 
                UpdateAnswers(tg.gameObject.GetComponentInChildren<Text>().text, tg.isOn); 
                });
        }

        textFieldInputs[0] = questionText;

        textFieldInputs[1] = optionAText;
        textFieldInputs[2] = optionBText;
        textFieldInputs[3] = optionCText;
        textFieldInputs[4] = optionDText;

        textFieldInputs[5] = timeText;
    }

    public void SaveQuestion()
    {
        if (AreAllInputsFilledOut())
        {
            string questionNumStr = questionNumberText.text;
            //Debug.Log(quizName);
            string path = gm.quizzesDir + "/" + quizName + "/Otázky/" + questionNumStr + ".json";
            //Debug.Log(path);

            PutFieldsIntoStorage();
            st.SaveQuestion(path);
        }
        else { Debug.Log("Can't save - not all inputs are filled out"); }
    }

    private float ParseToFloat(string str)
    {
        if (str.Length != 0)
        {
            char separator = 'N';
            if (str.Contains(",")) { separator = ','; }
            else if (str.Contains(".")) { separator = '.'; }

            if (separator != 'N')
            {
                string[] nums = str.Split(separator); //"123,456" -> [["123"]["456"]]

                float placesToPush = Mathf.Pow(10, (float)nums[1].Length);
                float behindDecMark = float.Parse(nums[1]) / placesToPush;
                float num = float.Parse(nums[0]) + behindDecMark;
                return num;
            }

            else { return float.Parse(str); }
        }
        else { Debug.Log("someone somewhere tried to parse a null string"); return 0f; }
    }

    public void SetQuizName(string str) { quizName = str; } //to be called upon the creation of a quiz

    public void ChangeQuizName(string str) //to be called when a quiz dir has already been created and we wish to change its name
    {
        string newQuizName = str;
        //create new directory called newQuizName and
        //copy the contents of quizName into it.
        //then, delete the directory called quizName
        //then, quizName = newQuizName
    }

    public void IncreaseIndex() 
    {
        //TODO make it so that stuff gets saved only when a change occurred

        if (AreAllInputsFilledOut())
        {
            //save the current input into the current question
            int currentIndex = int.Parse(questionNumberText.text);
            string questionName = currentIndex.ToString();
            string path = gm.quizzesDir + "/" + quizName + "/Otázky/" + questionName + ".json";
            PutFieldsIntoStorage();
            st.SaveQuestion(path);

            //increment currentIndex and then create the string of the next question's path
            currentIndex++;
            questionName = currentIndex.ToString();
            questionNumberText.text = questionName;
            string newPath = gm.quizzesDir + "/" + quizName + "/Otázky/" + questionName + ".json";


            //if the next question exists - show it
            //else, blank all fields
            //string newPathNoExtension = newPath.Replace(".json", "");

            Debug.Log(newPath);
            if (File.Exists(newPath))
            {
                Debug.Log("found the file: " + newPath);
                st.LoadQuestion(newPath);
                PutStorageIntoFields(currentIndex);
            }
            else
            {
                Debug.Log("this file does not exist: " + newPath);
                BlankAllFields();
            }
        }
        else 
        { 
            Debug.Log("not all inputs are filled out"); 
        }
    }
    public void DecreaseIndex()
    {
        //TODO make it so that stuff gets saved only when a change occurred

        int currentIndex = int.Parse(questionNumberText.text);
        currentIndex--;

        if (currentIndex <= 0)
        {
            currentIndex = 1;
            //open the "Quiz Overview" window
        }

        string path = gm.quizzesDir + "/" + quizName + "/Otázky/" + currentIndex.ToString() + ".json";
        st.LoadQuestion(path);
        PutStorageIntoFields(currentIndex);
        questionNumberText.text = currentIndex.ToString();
    }


    private bool AreAllInputsFilledOut()
    {
        foreach (Text txt in textFieldInputs)
        {
            string str = txt.text;
            //Debug.Log(str + " length: " + str.Length.ToString());
            if (str.Length == 0) { return false; }
        }
        if (answersStr.Length == 0) { return false; }
        return true;
    }

    private void UpdateAnswers(string str, bool isOn)
    {
        if (isOn) { answersStr = answersStr + str; }
        if (!isOn) { answersStr = answersStr.Replace(str, ""); }
    }

    private void BlankAllFields()
    {/*
        questionText.text = "";

        optionAText.text = "";
        optionBText.text = "";
        optionCText.text = "";
        optionDText.text = "";

        timeText.text = "";
        */
        questionText.gameObject.GetComponentInParent<InputField>().text = ""; //baaaad design

        optionAText.gameObject.GetComponentInParent<InputField>().text = "";
        optionBText.gameObject.GetComponentInParent<InputField>().text = "";
        optionCText.gameObject.GetComponentInParent<InputField>().text = "";
        optionDText.gameObject.GetComponentInParent<InputField>().text = "";

        timeText.gameObject.GetComponentInParent<InputField>().text = "";

        foreach (Toggle tg in toggles) { tg.isOn = false; }
    }

    private void PutFieldsIntoStorage()
    {
        st.otazka.otazka = questionText.text;
        st.otazka.odpovedA = optionAText.text;
        st.otazka.odpovedB = optionBText.text;
        st.otazka.odpovedC = optionCText.text;
        st.otazka.odpovedD = optionDText.text;
        st.otazka.spravnaOdpoved = answersStr;
        st.otazka.cas = ParseToFloat(timeText.text);

    }

    public void RefreshFields() { PutStorageIntoFields(1); questionNumberText.text = "1"; }

    private void PutStorageIntoFields(int questionNum) //loads the appropriate values into the question indexed at questionNum
    {
        string qName = questionNum.ToString();
        string path = gm.quizzesDir + "/" + quizName + "/Otázky/" + qName + ".json";

        try { st.LoadQuestion(path); }
        catch (Exception e) 
        {
            Debug.Log(e.Message);
            BlankAllFields();
            return;
        }
        questionText.gameObject.GetComponentInParent<InputField>().text = st.otazka.otazka; //baaaad design

        optionAText.gameObject.GetComponentInParent<InputField>().text = st.otazka.odpovedA;
        optionBText.gameObject.GetComponentInParent<InputField>().text = st.otazka.odpovedB;
        optionCText.gameObject.GetComponentInParent<InputField>().text = st.otazka.odpovedC;
        optionDText.gameObject.GetComponentInParent<InputField>().text = st.otazka.odpovedD;

        timeText.gameObject.GetComponentInParent<InputField>().text = st.otazka.cas.ToString();

        foreach (Toggle tg in toggles) //set the checkmarks
        {
            string letter = tg.GetComponentInChildren<Text>().text; //bad design, it assumes that the text will be for ex. just "A" instead of "A:"
            if (st.otazka.spravnaOdpoved.Contains(letter)) { tg.isOn = true; }
            else { tg.isOn = false; }
            timeText.text = st.otazka.cas.ToString();
        }
    }

    public string GetQuizName() { return quizName; }

    public void TellOverviewQuizName() { overviewScript.SetQuizName(quizName); }

    public void SetQuizNameFromText(Text txt) { quizName = txt.text; }
}

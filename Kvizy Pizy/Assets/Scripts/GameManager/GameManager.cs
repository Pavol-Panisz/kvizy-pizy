using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    public TextAsset SampleInfo { get; private set; }
    public TextAsset SampleQuestion { get; private set; }

    public string quizzesDir; // "Kvízy", where all quizzes are stored
    public string templatesDir; // "Predloha", where the templates of important storage files are stored (info.json, for example)

    private string quizToPlay; //used when switching scenes to Game, so that QuestionManager there knows what to load

    private char sep = Path.DirectorySeparatorChar;

    private StorageSys st;

    private void Awake()
    { 
        SetFolderPaths();
        ForceSingleton();

        //create the Quiz directory where all quizzes are stored
        if (!Directory.Exists(quizzesDir)) { Directory.CreateDirectory(quizzesDir); }

        //grab the sample info and sample question json files
        SampleInfo = Resources.Load<TextAsset>(templatesDir + sep + "info");
        SampleQuestion = Resources.Load<TextAsset>(templatesDir + sep +"otázka");
    }

    private void Start()
    {
        st = GameObject.FindGameObjectWithTag("GameManager").GetComponent<StorageSys>();
    }

    private void SetFolderPaths()
    {
        Debug.Log("set the folder paths");
        quizzesDir = Application.persistentDataPath + sep + "Kvízy";

        // the Predloha dir's content is accessed using Resources.Load,
        // which automatically knows where the Resources dir is, 
        // containing the Predloha dir.
        templatesDir = "Predloha"; 
    }

    private void ForceSingleton()
    {
        if (instance != null && instance != this) { Destroy(this.gameObject); }
        else { instance = this; }

        if (instance == this) { DontDestroyOnLoad(this.gameObject); }
    }

    public void LoadScene(int n)
    {
        string path = quizzesDir + sep + quizToPlay + sep + "Otázky" + sep + "1.json";
        if (File.Exists(path))
        {
            SceneManager.LoadScene(n);
        }
        else { Debug.Log("that quiz doesn't even have 1 question!"); }
    }

    public void SetQuizToPlayFromText(Text txt) { quizToPlay = txt.text; }
    public string GetQuizToPlay() { return quizToPlay; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class CreateNewQuiz : MonoBehaviour
{
    public Text nameText;
    public Text descriptionText;
    public Button createButton;

    [Header("Word checking")]
    public Color warningColor;
    private Color standardColor;
    public List<string> BannedWords = new List<string>();
    [SerializeField] private QuizList quizList;

    private GameManager gm;
    private StorageSys storageSys;
    private QuestionCreator questionCreator;

    private void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        storageSys = GameObject.FindGameObjectWithTag("GameManager").GetComponent<StorageSys>();
        questionCreator = GameObject.FindGameObjectWithTag("QuestionCreator").GetComponent<QuestionCreator>();
    }

    private void Start()
    {
        //CreateQuiz("Testovný Kvíz 1"); /* DEBUG */
        createButton.onClick.AddListener(delegate {quizList.AddField(nameText.text.TrimEnd(' ')); });
    }

    private void Update()
    {
        if (NameContainsBannedWord() || NameExists())
        {
            createButton.interactable = false;
        }
        else { createButton.interactable = true; }
    }

    public void NewQuizButtonClicked()
    {
        string name = nameText.text;

        name = name.TrimEnd(' ');  
        CreateQuiz(name);
    }

    private void CreateQuiz(string name)
    {
        //SetQuizName is called only in this one place!!!
        //To change the name of an already created directory, we use ChangeQuizName!!!
        questionCreator.SetQuizName(name); 

        string thisQuizDir = gm.quizzesDir + "/" + name;
        Directory.CreateDirectory(thisQuizDir);

        Directory.CreateDirectory(thisQuizDir + "/Obrázky");
        Directory.CreateDirectory(thisQuizDir + "/Otázky");
        
        File.WriteAllText(thisQuizDir + "/info.json", gm.SampleInfo.text);

        //TODO: Make a Json Utility class for reading and overwriting json files
        storageSys.info.nazov = name; //we use name instead of nameText.text because we trimmed whitespace from the end of the var. name
        storageSys.info.opis = descriptionText.text;
        storageSys.SaveInfo(thisQuizDir + "/info.json");

        storageSys.LoadInfo(thisQuizDir + "/info.json");
    }

    private bool NameContainsBannedWord()
    {
        foreach (string word in BannedWords)
        {
            if (nameText.text.Contains(word)) { return true; }
        }
        return false;
    }

    private bool NameExists()
    {
        if (Directory.Exists(gm.quizzesDir + "/" + nameText.text)) { return true; }
        else { return false; }
    }
}

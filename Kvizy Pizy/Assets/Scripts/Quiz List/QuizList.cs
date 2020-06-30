using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System;

public class QuizList : MonoBehaviour
{
    GameManager gm;
    List<string> quizNames;
    Dictionary<string, GameObject> quizzesDict;
    Color defaultColor;

    [SerializeField] private GameObject fieldPrefab;
    [SerializeField] private Transform fieldParent;
    [SerializeField] private Color contrastColor; //every 2nd field's background colour

    [SerializeField] GameObject overviewWindow;
    [SerializeField] private WindowManager windowManager;
    private Overview overview;
    private ContentSwitcher overviewResizer;

    public InputField TEST_DeletionInputField;

    private void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        quizNames = new List<string>();
        quizzesDict = new Dictionary<string, GameObject>();

        overviewResizer = overviewWindow.GetComponent<ContentSwitcher>();
        overview = overviewWindow.GetComponent<Overview>();

        Debug.Log("called awake. quizzes dir: " + gm.quizzesDir);

        defaultColor = fieldPrefab.GetComponent<Image>().color;
        CreateFields();
    }

    private void Start()
    {
        //defaultColor = fieldPrefab.GetComponent<Image>().color;
        //CreateFields();
    }

    private void AbcSortQuizNames() { quizNames.Sort(); }

    private void LoadQuizNames()
    {
        IEnumerable<string> directories = Directory.EnumerateDirectories(gm.quizzesDir);
        
        //remove the quiz paths leading up to the directory name string
        string del = gm.quizzesDir + "\\"; //for some reason, the character '\' comes after the quizzesDir 

        Debug.Log(directories.Count<string>());

        for (int n = 0; n != directories.Count<string>(); n++)
        {
            var str = directories.ElementAt<string>(n);
            str = str.Replace(del, "");
            str = str.TrimEnd(' ');
            quizNames.Add(str);
        }

        //quizNames = directories.ToList<string>();
        AbcSortQuizNames();
    }

    public void CreateFields()
    {
        Debug.Log("called create fields");

        LoadQuizNames();

        int n = 0;
        foreach (string dirPath in quizNames)
        {
            GameObject go = InstantiateField(dirPath);
            SetColor(go);

            string str = dirPath.TrimEnd(' ');
            quizzesDict.Add(str, go);
        }
    }

    public void AddField(string newField)
    {
       
        Debug.Log("added field called: " + newField);

        GameObject go = InstantiateField(newField);
        quizNames.Add(newField);
        AbcSortQuizNames();
        int index = quizNames.BinarySearch(newField);
        go.transform.SetSiblingIndex(index);

        newField = newField.TrimEnd(' ');
        quizzesDict.Add(newField, go);
        //recolor the fields starting at the sibling index
        RecolorFromIndex(index);
    }

    private GameObject InstantiateField(string name)
    {
        GameObject go = UnityEngine.Object.Instantiate(fieldPrefab, fieldParent.position, Quaternion.identity) as GameObject;
        go.transform.SetParent(fieldParent, false);
        go.transform.position = Vector3.zero;
        go.GetComponentInChildren<Text>().text = name;
        //Debug.Log("created a field for: " + name);

        //add the functionality to tell the overview the quizName
        Button button = go.GetComponent<Button>();
        button.onClick.AddListener(delegate { overview.SetQuizName(name); });

        //add functionality to enlarge the overview window, update it and open it
        button.onClick.AddListener(delegate { overviewResizer.SetLarger(); });
        button.onClick.AddListener(delegate { overview.UpdateOverview(); });
        button.onClick.AddListener(delegate { windowManager.OpenWindow(overviewWindow); });

        return go;
    }

    private void SetColor(GameObject go, bool reverse=false)
    {
        //is every even or odd field supposed to have the contrast color?
        int pick;
        if (!reverse) { pick = 1; }
        else { pick = 0; }

        int n = go.transform.GetSiblingIndex();
        if (n % 2 == pick) { go.GetComponent<Image>().color = contrastColor; }
        else { go.GetComponent<Image>().color = defaultColor; }
    }

    public void RemoveFieldNamedText(Text txt) { RemoveField(txt.text); }
    private void RemoveField(string remField)
    {
        foreach (string key in quizzesDict.Keys)
        {
            Debug.Log("\'"+key+"\'");
        }
        Debug.Log("tried to find \'" + remField + "\'");

        GameObject objToDestroy = quizzesDict[remField];
        try { quizzesDict.Remove(remField); }
        catch(Exception e) { Debug.Log(e.Message); }
        GameObject.Destroy(objToDestroy);

        //this code does not get executed when deleting the last field
        int index = quizNames.BinarySearch(remField);
        quizNames.Remove(remField);
        Debug.Log(quizNames[index]);

        RecolorFromIndex(index, true);

        string fullPath = gm.quizzesDir + "/" + remField;
        Debug.Log("tried to delete path: " + fullPath);
        Directory.Delete(fullPath, true);
    }

    private void RecolorFromIndex(int index, bool b=false)
    {
        for (int n = index; n != quizNames.Count<string>(); n++)
        {
            string str = quizNames.ElementAt<string>(n);
            //Debug.Log("recoloring: " + str + " at index: " + n.ToString());
            SetColor(quizzesDict[str], b);
        }
    }

    /*private void Update() //TEST
    {
        string strToDel = TEST_DeletionInputField.text;
        if (quizNames.Contains(strToDel)) { RemoveField(strToDel); }
    }*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public List<GameObject> windows = new List<GameObject>();
    private Dictionary<string, GameObject> windowDict = new Dictionary<string, GameObject>();

    [SerializeField] private GameObject initialWindow;

    private void Awake()
    {
        //populate the windows list
        foreach (GameObject go in windows)
        {
            windowDict.Add(go.name, go);
        }
    }

    private void Start()
    { 
        //the 1st window to be displayed
        foreach (GameObject go in windowDict.Values)
        {
            if (go.name == initialWindow.name) 
            { 
                OpenWindow(initialWindow);
            }
            else { CloseWindow(go); }
        }
    }

    public void OpenWindow(GameObject go)
    {
        string str = go.name;
        if (windowDict.ContainsKey(str))
        {
            windowDict[str].gameObject.SetActive(true);
            return;
        } else { Debug.Log("There is no window called " + str + " in Window Manager'd windows array."); }
    }

    public void CloseWindow(GameObject go)
    {
        string str = go.name;
        if (windowDict.ContainsKey(str))
        {
            windowDict[str].gameObject.SetActive(false);
            return;
        } else { Debug.Log("There is no window called " + str + " in Window Manager's windows array."); }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileCreator : MonoBehaviour
{
    void Start()
    {
        string path = Application.dataPath + "/Resources/Kvízy/Kvíz_1/Question 1.json";

        if (!File.Exists(path))
        {
            File.WriteAllText(path, "{\n\n}");
        }

        
        else { string str = "I was here\n"; File.AppendAllText(path, str); }
    }
}

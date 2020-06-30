using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class TEST_ReadInfo : MonoBehaviour
{
    public Text inputField;
    public Text nameDisplay;
    public Text infoDisplay;
    private StorageSys st;
    private GameManager gm;

    private void Start()
    {
        st = GameObject.FindGameObjectWithTag("GameManager").GetComponent<StorageSys>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        string quizToLoad = inputField.text;
        string path = gm.quizzesDir + "/" + quizToLoad;
        if (Directory.Exists(path))
        {
            string infoFilePath = path + "/info.json";

            st.LoadInfo(infoFilePath);
            nameDisplay.text = st.info.nazov;
            infoDisplay.text = st.info.opis;
        }
    }
}

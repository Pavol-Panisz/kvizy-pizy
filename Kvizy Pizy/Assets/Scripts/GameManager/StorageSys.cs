using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class StorageSys : MonoBehaviour
{
    public Info info;
    public Otazka otazka;

    //The following two methods are necessary like this, because Buttons can only call methods with 1 parameter
    public void SaveInfo(string path) { SaveStruct<Info>(info, path); } 
    public void SaveQuestion(string path) { SaveStruct<Otazka>(otazka, path); }

    //didn't know how to make a generic LoadStruct function: the casting of a generic type to a specific one bamboozled me
    public void LoadInfo(string path)
    {
        string textInFile = File.ReadAllText(path);
        info = JsonUtility.FromJson<Info>(textInFile);
    }
    public void LoadQuestion(string path)
    {
        string textInFile = File.ReadAllText(path);
        otazka = JsonUtility.FromJson<Otazka>(textInFile);
    }

    private void SaveStruct<T>(T storageStruct, string path)
    {
        string jsonText;
        jsonText = JsonUtility.ToJson(storageStruct, true);
        File.WriteAllText(path, jsonText);
    }

    public Otazka LoadAndReturnQuestion(string path)
    {
        LoadQuestion(path);
        Otazka ot;
        ot.cas = otazka.cas;
        ot.cislo = otazka.cislo;
        ot.otazka = otazka.otazka;
        ot.spravnaOdpoved = otazka.spravnaOdpoved;
        
        ot.odpovedA = otazka.odpovedA;
        ot.odpovedB = otazka.odpovedB;
        ot.odpovedC = otazka.odpovedC;
        ot.odpovedD = otazka.odpovedD;

        return ot;
    }
}

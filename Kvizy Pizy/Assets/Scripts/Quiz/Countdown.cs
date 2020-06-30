using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    [SerializeField] RectTransform barRectTr;
    private Transform tr;
    private float widthSquared;
    
    private Vector3 initial, target, translationVector;
    private bool doMove = false;

    [SerializeField] QuestionManager questionManager;

    private void Awake()
    {
        tr = barRectTr.transform;
        initial = tr.position;
        float width = barRectTr.rect.width;
        target = new Vector3(tr.position.x - width, tr.position.y, tr.position.z);
        widthSquared = barRectTr.rect.width * barRectTr.rect.width;
    }

    public void Restart(float t)
    {
        tr.position = initial;
        translationVector = (target - initial) / t;
        doMove = true;
    }

    private void Update()
    {
        if (doMove)
        {
            tr.Translate(translationVector * Time.deltaTime);
            Debug.Log(tr.position.x + " " + target.x);
            if ((tr.position.x - target.x) <= 0f) //not the most precise, but good enough for this scenario
            {
                doMove = false;
                questionManager.ShowNext();
            }
        }
    }



    public void Toggle()
    {
        if (doMove) { doMove = false; }
        else { doMove = true; }
    }
}

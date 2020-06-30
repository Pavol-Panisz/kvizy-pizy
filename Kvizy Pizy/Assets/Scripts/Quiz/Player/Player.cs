using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    CanvasGroup canvasGroup;
    private PointChecker pointChecker;
    private Transform freeSpace;
    private Image image;

    private Transform homePos, lastParent;

    private List<PlayerDocker> playerDockerScripts = new List<PlayerDocker>();

    public Dictionary<string, GameObject> TESTdct = new Dictionary<string, GameObject>();

    private int score = 0;
    private string address;

    private void Awake()
    {
        pointChecker = GetComponent<PointChecker>();
        image = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
        //freeSpace = GameObject.FindGameObjectWithTag("FreeSpace").GetComponent<Transform>();

        //var playerDockers = GameObject.FindGameObjectsWithTag("PlayerDocker");
        //foreach (GameObject go in playerDockers) { playerDockerScripts.Add(go.GetComponent<PlayerDocker>()); }
    }

    public void Setup()
    {
        var playerDockers = GameObject.FindGameObjectsWithTag("PlayerDocker");
        foreach (GameObject go in playerDockers) { playerDockerScripts.Add(go.GetComponent<PlayerDocker>()); }

        freeSpace = GameObject.FindGameObjectWithTag("FreeSpace").GetComponent<Transform>();
    }

    public void StartDrag()
    {
        canvasGroup.blocksRaycasts = false;
        SetParent(freeSpace);
    }

    public void StopDrag()
    {
        canvasGroup.blocksRaycasts = true;

        RectTransform triggerRectTr;
        foreach (PlayerDocker pd in playerDockerScripts)
        {
            

            triggerRectTr = pd.GetTriggerTransform();

            if (pointChecker.IsPointInside(Input.mousePosition, triggerRectTr))
            {
                Transform pdTr = pd.gameObject.transform;
                SetParent(pdTr);

                //lastParent = pdTr; //Bad for user experience. It's better to put the player home if they're dropped into empty space
                return;
            }
        }
        SetParent(lastParent);
    }

    public void Dragging()
    {
        transform.position = Input.mousePosition;
    }

    public void SetParent(Transform tr)
    {
        transform.SetParent(tr);
        transform.SetSiblingIndex(0);
    }

    public void SetVisible(bool f)
    {
        image.enabled = f;
    }

    public void IncreaseScore() { score += 1; }
    public void ResetScore() { score = 0; }

    public void SetAddress(string str) { address = str;}

    public void SetCurrentParentAsHome() { 
        homePos = transform.parent;
        lastParent = homePos;
    }

    public void GoHome() { SetParent(homePos); }
}

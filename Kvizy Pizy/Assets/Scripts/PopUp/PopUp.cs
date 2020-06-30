using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    private Text heading, content;

    [SerializeField] private GameObject window;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float duration;
    
    [Header("Shade")]
    [SerializeField] Image shadeImage;
    Color transparent;
    [SerializeField] Color fullyShaded;

    private bool doMove = false;
    private Transform winT, tarT;
    private Vector3 vel;

    private void Start()
    {
        winT = window.GetComponent<Transform>();
        tarT = targetTransform;
        vel = Vector3.zero;
        ClosePopUp();
    }

    public void ShowPopUp(string h, string c)
    {
        Debug.Log(heading + "|" + content);
        heading.text = h;
        content.text = c;
        window.SetActive(true);
        shadeImage.gameObject.SetActive(true);
    }

    public void ClosePopUp()
    {
        Debug.Log("closed popup");
        window.SetActive(false);
        shadeImage.gameObject.SetActive(false);
    }
}
 
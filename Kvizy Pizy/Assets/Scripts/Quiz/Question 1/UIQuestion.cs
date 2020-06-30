using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIQuestion : MonoBehaviour
{
    private GameObject selected;
    [SerializeField] private GameObject playerContainer;
    private Transform pcgTransform;
    private PointChecker pointChecker;

    private void Awake()
    {
        pointChecker = GetComponent<PointChecker>();
    }

    private void Start()
    {
        pcgTransform = GameObject.FindGameObjectWithTag("PlayerContainerGroup").GetComponent<Transform>();
        ExitButton();
    }

    public void ExitButton()
    {
        if (!pointChecker.IsPointInside(Input.mousePosition)) { playerContainer.transform.SetParent(pcgTransform); /*Debug.Log("mouse isnt inside this rect");*/ }
        else { /*Debug.Log("mouse IS inside this rect"); */}
        
    }

    public void EnteredButton()
    {

        playerContainer.transform.SetParent(this.gameObject.transform);
        playerContainer.transform.SetSiblingIndex(1);
    }

}

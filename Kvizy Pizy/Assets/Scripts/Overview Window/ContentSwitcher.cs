using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSwitcher : MonoBehaviour
{
    [SerializeField] private RectTransform smaller, larger;
    private float sHeight, lHeight;

    [SerializeField] GameObject extraContentPanel;

    void Start()
    {
        sHeight = smaller.rect.height;
        lHeight = larger.rect.height;
        SetSmaller();
    }

    public void SetLarger() 
    {
        extraContentPanel.SetActive(true);
        gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, lHeight);
    }

    public void SetSmaller()
    {
        extraContentPanel.SetActive(false);
        gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, sHeight);
    }

}

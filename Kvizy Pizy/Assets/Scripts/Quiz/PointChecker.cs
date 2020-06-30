using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointChecker : MonoBehaviour
{
    public bool IsPointInside(Vector3 point, RectTransform thisRectTr=null)
    {
        //Debug.Log("pointPos: " + point);

        if (!thisRectTr) { thisRectTr = GetComponent<RectTransform>(); }
        Vector3[] fourCorners = new Vector3[4];
        thisRectTr.GetWorldCorners(fourCorners);
        Vector3 bottomLeft = fourCorners[0];
        Vector3 topRight = fourCorners[2];
        //Debug.Log(gameObject.name + "2 corners: " + bottomLeft + " | " + topRight);

        bool isInX = false;
        if ((point.x >= bottomLeft.x) && (point.x <= topRight.x)) { isInX = true; }

        bool isInY = false;
        if ((point.y >= bottomLeft.y) && (point.y <= topRight.y)) { isInY = true; }

        if (isInX && isInY) { return true; }
        else { return false; }
    }
}

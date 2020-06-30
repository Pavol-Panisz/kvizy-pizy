using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * HOW TO USE A PLAYERDOCKER:
 * 1.) Attach this script to the gameObject, to which the Player should parent when dropped on.
 * 2.) Assign the tag "PlayerDocker" to this gameObject
 * 3.) Assign the triggerTransform to this gameObject. 
 *       When the player gets dropped into the triggerTransform rect,
 *       they will be parented to this gameObject.
 */

public class PlayerDocker : MonoBehaviour
{
    [SerializeField] private RectTransform triggerTransform;

    private void Awake()
    {
        
    }

    private void Start()
    {
        if (this.tag != "PlayerDocker") { Debug.Log("You forgot to assign the tag \'PlayerDocker\' to " + gameObject.name); }
        if (!triggerTransform) { Debug.Log("You forgot to assign the triggerTransform to " + gameObject.name); }
    }

    public RectTransform GetTriggerTransform() { return triggerTransform; }
}

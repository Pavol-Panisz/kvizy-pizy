using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmoothDamp : MonoBehaviour
{
    public GameObject start, finish, stopwatch, velDisplay;
    public float duration, timePassed;
    private Vector3 vel;

    private void Start()
    {
        vel = new Vector3(0f, 0f, 0f);
    }

    private void Update()
    {
        timePassed += Time.deltaTime;
        transform.position = Vector3.SmoothDamp(transform.position, finish.transform.position, ref vel, duration);
        stopwatch.GetComponent<Text>().text = timePassed.ToString();
        velDisplay.GetComponent<Text>().text = vel.ToString();
    }
}

/**
 * Ardity (Serial Communication for Arduino + Unity)
 * Author: Daniel Wilches <dwilches@gmail.com>
 *
 * This work is released under the Creative Commons Attributions license.
 * https://creativecommons.org/licenses/by/2.0/
 */



using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class SampleMessageListener : MonoBehaviour
{
    #region Variables, Messages and Enums
    private SerialController sc;
    private enum Modes {NoReceiver, Setup, Receiving}
    private Modes currentMode = Modes.NoReceiver;

    [Tooltip("The seconds spent listening to each serial port when searching for a receiver")]
    public float listeningTime;

    [Tooltip("The message Unity recognizes as the receiver")]
    public string RECEIVER_IDENTIFICATOR;

    [Tooltip("The message Unity sends to the receiver upon recognizing it")]
    public string RECEIVER_RECOGNIZED;
    #endregion

    private void Start()
    {
        sc = GetComponent<SerialController>();
    }

    public void OnPressedConnect()
    {
        sc.StopListeningToCurrentPort();
        StopCoroutine("SearchForReceiver");
        StartCoroutine("SearchForReceiver");
    }

//Listens to each available port for (listeningTime) seconds.
//If during this time the metho OnMessageArrived() gets 
//called, we check whether this message is equal to the 
//RECEIVER_IDENTIFICATOR string. If yes, we listen to only 
//this port from now on.
    private IEnumerator SearchForReceiver()
    {
        currentMode = Modes.Setup;
        bool foundReceiver = false;

        foreach (string port in SerialPort.GetPortNames())
        {
            Debug.Log("Listening to port " + port);

            sc.StartListeningToPort(port);
            yield return new WaitForSeconds(listeningTime);

            //if during the listeningTime, OnMessageArrived was called, 
            //currentMode will have been set to Modes.Receiving
            if (currentMode == Modes.Receiving)
            {
                foundReceiver = true;
                break;
            }
            else { sc.StopListeningToCurrentPort(); }
        }

        if (foundReceiver)
        {
            Debug.Log("We found a receiver!");
            sc.SendSerialMessage(RECEIVER_RECOGNIZED);
        }
        else
        {
            Debug.Log("No receiver was found.");
            currentMode = Modes.NoReceiver;
        }
    }

    //called by the SerialController
    void OnMessageArrived(string msg)
    {   
        Debug.Log("Message arrived |" + msg + "|");

        switch (currentMode)
        {
            case (Modes.Receiving):
                break;

            case (Modes.Setup):
                if (msg == RECEIVER_IDENTIFICATOR)
                {
                    currentMode = Modes.Receiving;
                }
                break;

            default:
                break;
        }

    }

    void OnConnectionEvent(bool success)
    {
        if (success)
            Debug.Log("Connection established");
        else
            Debug.Log("Connection attempt failed or disconnection detected");
    }

}

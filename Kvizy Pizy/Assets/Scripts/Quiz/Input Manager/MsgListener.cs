using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
public class MsgListener: MonoBehaviour
{

    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        Debug.Log("Arrived: " + msg);
    }
    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device connected" : "Device disconnected");
    }

    private void Start()
    {
        string[] ports = SerialPort.GetPortNames();
        foreach (string port in ports) { Debug.Log(port); }
    }
}
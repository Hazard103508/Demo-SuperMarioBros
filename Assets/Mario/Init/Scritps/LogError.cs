using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogError : MonoBehaviour
{
    void Awake()
    {
        Application.logMessageReceived += HandleException;
        DontDestroyOnLoad(gameObject);
    }

    void HandleException(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Exception)
        {
            string path = $"C:\\Libraries\\Agustin\\Desktop\\Logs\\{DateTime.Now.ToString("HHmmssffff")}.txt";
            string message = logString + "\r\n" + stackTrace;

            UnityShared.Files.Files.Save(message, path);
        }
    }

}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public struct MyLogType
{
    
}

public class LogManager
{
    private static LogManager _instance;
    private static Text _logText;

    public static LogManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new LogManager();
                _logText = GameObject.FindGameObjectWithTag("MyLogText").GetComponent<Text>();
                LogManager.Instance.Log("Logger Initialized");
            }
            return _instance;
        }
    }

    public void Log( string text)
    {
        _logText.text += text;
        _logText.text += "\n";
    }
}
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public struct MyLogType
{
    
}

public class LogManager : MonoBehaviour {

    private static Text _logText;

    public static LogManager Instance { get; private set;}
	
    void Awake () {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
        _logText = gameObject.GetComponent<Text>();

        LogManager.Instance.Log("Logger Initialized");
        DontDestroyOnLoad(gameObject);
	}

    void Update()
    {
        
    }

    public void Log( string text)
    {
        _logText.text += text;
        _logText.text += "\n";
    }

}

using UnityEngine;
using System.Collections.Generic;


public class CharacterFocusChangeEvent : IEvent
{
    private KeyCode _keycode;
    private string _tag;

    public KeyCode TriggedKey { get { return _keycode; }}
    public string Tag { get { return _tag; }}

    public CharacterFocusChangeEvent(KeyCode keycode, string tag)
    {
        _keycode = keycode;
        _tag = tag;
    }

    public void LogEvent() { LogManager.Instance.Log("Input event"); }
}

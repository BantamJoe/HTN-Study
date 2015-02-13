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

    public void LogEvent() { LogManager.Instance.Log("Input Event"); }
}

public class ThoughtChangeEvent  : IEvent
{
    private Actor _actorTarget;
    private Thought _thought;

    public Thought Thought { get { return _thought; } }
    public Actor ActorTarget { get { return _actorTarget; } }

    public ThoughtChangeEvent(Thought thought, Actor target)
    {
        _actorTarget = target;
        _thought = thought;
    }
    
    public void LogEvent() { LogManager.Instance.Log("Thought Change Event"); }
}

public class HealthChangeEvent : IEvent
{
    private Actor _actorTarget;
    private int _amount;

    public int Amount { get { return _amount; } }
    public Actor ActorTarget { get { return _actorTarget; } }

    public HealthChangeEvent(int amount, Actor target)
    {
        _actorTarget = target;
        _amount = amount;
    }

    public void LogEvent() { LogManager.Instance.Log("Health Change Event"); }
}

public class StatusChangeEvent : IEvent
{
    private Actor _actorTarget;
    private string _status;

    public string Status{ get { return _status; } }
    public Actor ActorTarget { get { return _actorTarget; } }

    public StatusChangeEvent(string status, Actor target)
    {
        _actorTarget = target;
        _status = status;
    }

    public void LogEvent() { LogManager.Instance.Log("Status Change Event"); }
}

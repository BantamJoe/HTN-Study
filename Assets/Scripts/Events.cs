using UnityEngine;
using System.Collections.Generic;

public class GoalChangeTriggerEvent : IEvent
{
    private Goal _newGoal;
    private ID _npc;

    public Goal NewGoal { get { return _newGoal; } }
    public ID CallerID { get { return _npc; } }

    public GoalChangeTriggerEvent(Goal goal, ID npc)
    {
        _npc = npc;
        _newGoal = goal;
    }

    public void LogEvent() { Debug.Log("GoalChangeTriggerEvent"); }
}

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

    public void LogEvent() { Debug.Log("Input Event"); }
}

public class ThoughtChangeEvent  : IEvent
{
    private ID _actorTarget;
    private Thought _thought;

    public Thought Thought { get { return _thought; } }
    public ID ActorTarget { get { return _actorTarget; } }

    public ThoughtChangeEvent(Thought thought, ID target)
    {
        _actorTarget = target;
        _thought = thought;
    }

    public void LogEvent() { Debug.Log("Thought Change Event"); }
}

public class HealthChangeEvent : IEvent
{
    private ID _actorTarget;
    private int _amount;

    public int Amount { get { return _amount; } }
    public ID ActorTarget { get { return _actorTarget; } }

    public HealthChangeEvent(int amount, ID target)
    {
        _actorTarget = target;
        _amount = amount;
    }

    public void LogEvent() { Debug.Log("Health Change Event"); }
}

public class StatusChangeEvent : IEvent
{
    private ID _actorTarget;
    private string _status;

    public string Status{ get { return _status; } }
    public ID ActorTarget { get { return _actorTarget; } }

    public StatusChangeEvent(string status, ID target)
    {
        _actorTarget = target;
        _status = status;
    }

    public void LogEvent() { Debug.Log("Status Change Event"); }
}

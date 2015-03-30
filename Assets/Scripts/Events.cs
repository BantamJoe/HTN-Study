using UnityEngine;
using System.Collections.Generic;

public class PlayerDeadEvent : IEvent
{
    public void LogEvent() { }
}
public class PlayerDamageEvent: IEvent
{
    public void LogEvent() { }
}
public class BeginNpcAttackEvent : IEvent
{
    private NPC _npc;
    public NPC NPCRef { get { return _npc; } }
    
    public BeginNpcAttackEvent(string id)
    {
        ID parsedID = EnumerationParse.GetEnumEquivalent<ID>(id);
        _npc = NPCManager.Instance.GetNPCFromID(parsedID);
    }
    public void LogEvent() { }
}

public class EndNpcAttackEvent : IEvent
{
    private NPC _npc;
    public NPC NPCRef { get { return _npc; } }
    
    public EndNpcAttackEvent(string id)
    {
        _npc = NPCManager.Instance.GetNPCFromID(EnumerationParse.GetEnumEquivalent<ID>(id));
    }
    public void LogEvent() { }
}

public class UpdateFocusNPCEvent : IEvent
{
    private ID _npc;

    public ID CallerID { get { return _npc; } }

    public UpdateFocusNPCEvent(ID npc)
    {
        _npc = npc;
    }

    public void LogEvent() { }
}

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

    public void LogEvent() { }
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

    public void LogEvent() { }
}


public class AmoSupplyUIChangeEvent : IEvent
{
    private ID _caller;
    private string _amoValue;

    public string AmoValue { get { return _amoValue; } }
    public ID NPC { get { return _caller; } }

    public AmoSupplyUIChangeEvent(ID caller, int amoVal)
    {
        _caller = caller;
        _amoValue = amoVal.ToString();
    }

    public void LogEvent() { }
}

public class HealthChangeEvent : IEvent
{
    private ID _npc;
    public ID NPC { get { return _npc; } }

    public HealthChangeEvent(ID npc)
    {
        _npc = npc;
    }

    public void LogEvent() { }
}

public class HealthUIChangeEvent : IEvent
{
    private ID _actorTarget;
    private int _amount;

    public int Amount { get { return _amount; } }
    public ID NPC { get { return _actorTarget; } }

    public HealthUIChangeEvent(int amount, ID target)
    {
        _actorTarget = target;
        _amount = amount;
    }

    public void LogEvent() { }
}

public class StatusUIChangeEvent : IEvent
{
    private ID _actorTarget;
    private string _status;

    public string Status{ get { return _status; } }
    public ID NPC { get { return _actorTarget; } }

    public StatusUIChangeEvent(string status, ID target)
    {
        _actorTarget = target;
        _status = status;
    }

    public void LogEvent() { }
}

public class InitializeStartLocationEvent : IEvent
{
    private Location _npc1Loc, _npc2Loc, _npc3Loc;

    public Location NPC1_StartLoc { get { return _npc1Loc; } }
    public Location NPC2_StartLoc { get { return _npc2Loc; } }
    public Location NPC3_StartLoc { get { return _npc3Loc; } }

    public InitializeStartLocationEvent(string npc1Loc, string npc2Loc, string npc3Loc)
    {
        _npc1Loc = ConvertToLocation(npc1Loc);
        _npc2Loc = ConvertToLocation(npc2Loc);
        _npc3Loc = ConvertToLocation(npc3Loc);
    }

    private Location ConvertToLocation(string location)
    {
        int x = (int)char.GetNumericValue(location[0]);
        int y = (int)char.GetNumericValue(location[1]);

        return new Location(x, y, Destination.None);
    }

    public void LogEvent() {  }
}

public class NPCLocationUpdateEvent : IEvent
{
    private Location _newLoc;
    private ID _id;

    public Location NewLocation { get { return _newLoc; } }
    public ID NPC { get { return _id; } }
    public NPCLocationUpdateEvent(Location newLoc, ID id)
    {
        _newLoc = newLoc;
        _id = id;
    }
    public void LogEvent() { }
}

public class ZeroNpcAmoSupplyEvent : IEvent
{
    private ID _npc;
    public ID CallerID { get { return _npc; } }

    public ZeroNpcAmoSupplyEvent(ID npc)
    {
        _npc = npc;
    }

    public void LogEvent() { }
}

public class ZeroWorldAmoSupplyEvent : IEvent { public void LogEvent() { } }
public class PlayerLocationUpdateEvent : IEvent
{
    private Location _newLoc;

    public Location NewLocation { get { return _newLoc; } }
    public PlayerLocationUpdateEvent(Location newLoc)
    {
        _newLoc = newLoc;
    }
    public void LogEvent() { }
}


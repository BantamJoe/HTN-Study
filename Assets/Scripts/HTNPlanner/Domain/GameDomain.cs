using System.Collections.Generic;
using System.Reflection;
using System;

public class GameDomain
{
    /// <summary>
    /// Function for getting all methods of this domain, so the HTN planner can use it. This is mandatory for the HTN planner to work.
    public Dictionary<string, MethodInfo[]> GetMethodsDict()
    {
        Dictionary<string, MethodInfo[]> domainMethDict = new Dictionary<string, MethodInfo[]>();

        //Intialize all methods here
        MethodInfo[] methInfo = new MethodInfo[] { this.GetType().GetMethod("Attack_m") };
        domainMethDict.Add("Attack", methInfo);

        methInfo = new MethodInfo[] { this.GetType().GetMethod("ReadyWeapon_m") };
        domainMethDict.Add("ReadyWeapon", methInfo);

        methInfo = new MethodInfo[] { this.GetType().GetMethod("Aim_m") };
        domainMethDict.Add("Aim", methInfo);

        methInfo = new MethodInfo[] { this.GetType().GetMethod("ReadyAmo_m") };
        domainMethDict.Add("ReadyAmo", methInfo);

        methInfo = new MethodInfo[] { this.GetType().GetMethod("FindAmo_m") };
        domainMethDict.Add("FindAmo", methInfo);

        methInfo = new MethodInfo[] { this.GetType().GetMethod("FocusTarget_m") };
        domainMethDict.Add("FocusTarget", methInfo);

        return domainMethDict;
    }

    private static void AddTask(List<List<string>> returnVal, params string[] values)
    {
        returnVal.Add(new List<string>(values));
    }

    // METHODS (NON-PRIMITIVE TASKS)
    // An HTN method decomposes a non-primitive task into subtasks
    /**
     * Attack:
     * Ready Weapon
     * Aim
     * Ready Amo
     * Find Amo
     * Focus Target
     * */

    public static List<List<string>> Attack_m(State state)
    {
        List<List<string>> returnVal = new List<List<string>>();

        AddTask(returnVal, "ReadyWeapon_m");
        AddTask(returnVal, "Aim_m");
        AddTask(returnVal, "Fire");

        return returnVal;
    }

    public static List<List<string>> ReadyWeapon_m(State state)
    {
        List<List<string>> returnVal = new List<List<string>>();

        if (!state.CheckVar("has", "amo"))
            AddTask(returnVal, "ReadyAmo_m");
       
        return returnVal;
    }

    public static List<List<string>> Aim_m(State state)
    {
        List<List<string>> returnVal = new List<List<string>>();
        AddTask(returnVal, "MoveTo", "Player");
        return returnVal;
    }

    public static List<List<string>> ReadyAmo_m(State state)
    {
        List<List<string>> returnVal = new List<List<string>>();
        AddTask(returnVal, "Find", "Amo");
        AddTask(returnVal, "Reload");
        return returnVal;
    }

    public static List<List<string>> Find_m(State state, string item)
    {
        List<List<string>> returnVal = new List<List<string>>();
        AddTask(returnVal, "MoveTo", item);
        AddTask(returnVal, "PickUp", item);
        return returnVal;
    }

    // OPERATORS (PRIMITIVE TASKS)
    // A primitive task affects the internal state of the planner directly and cannot be decomposed into other subtasks
    public static State MoveTo(State state, string dest)
    {
        State newState = state;
        Destination destination = (Destination)Enum.Parse(typeof(Destination), dest);

        Location loc = LocationManager.Instance.FindDestinationLoc(destination);

        //log message that NPC has arrived to destination
        return newState;
    }

    public static State PickUp(State state, string item)
    {
        State newState = state;
        newState.Holds("have", item);
        return newState;
    }

    public static State Reload(State state)
    {
        State newState = state;

        newState.Remove("have", "amo");
        return newState;
    }

    public static State Fire(State state)
    {
        State newState = state;
         return newState;
    }

    public static State Drop(State state, string item)
    {
        State newState = state;

        if (state.CheckVar("have", item))
        {
            newState.Remove("have", item);
        }
        return newState;
    }
}
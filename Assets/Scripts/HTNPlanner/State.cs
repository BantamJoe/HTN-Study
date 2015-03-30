using System;
using System.Collections.Generic;

public class State
{

    /// <summary>
    /// Dictionary containing variables. A variable can be represented as: "have(money)", "name(John)", or "likes(Mary)".
    /// </summary>
    private Dictionary<string, List<string>> stateVariables = new Dictionary<string, List<string>>();

    /// <summary>
    /// Dictionary containing variables in the form of relations. This allows for more complex planning behavior.
    /// A relation can be represented as: "parentOf(Glóin, Gimli)", "sees(ammo, weapon)" (i.e. agent sees "ammo" of type "weapon"), 
    /// or "adjecent(roomOne, roomTwo)".
    /// </summary>
    private Dictionary<string, Dictionary<string, List<string>>> stateRelations = new Dictionary<string, Dictionary<string, List<string>>>();

    public string StateName { get; set; }

    public State(string name) { this.StateName = name; }
    
    public State(State state)
    {
        this.StateName = state.StateName;
        this.stateVariables = state.GetVariablesCopy();
        this.stateRelations = state.GetRelationsCopy();
    }

    public void Add(string variable, string innerState)
    {
        if (stateVariables.ContainsKey(variable))
        {
            stateVariables[variable].Add(innerState);
        }
        else
        {
            stateVariables.Add(variable, new List<string>(new string[] { innerState }));
        }
    }

    public void Remove(string variable, string innerState)
    {
        if (stateVariables.ContainsKey(variable))
        {
            stateVariables[variable].Remove(innerState);
            if (stateVariables[variable].Count == 0)
            {
                stateVariables.Remove(variable);
            }
        }
    }


    //public List<string> GetStateOfVar(string variable)
    //{
    //    if (ContainsVariable(variable))
    //        return stateVariables[variable];
    //    return null;
    //}


    public bool ContainsVariable(string variable) { return stateVariables.ContainsKey(variable); }

    public bool CheckVariable(string variable, string innerState)
    {
        if (stateVariables.ContainsKey(variable) && stateVariables[variable].Contains(innerState))
            return true;
        return false;
    }


    public bool Holds(string variable, string innerState) { return CheckVariable(variable, innerState); }


    /// <summary>
    /// Adds a relation to the internal state of the planner.
    /// </summary>
    /// <param name="variable">The relation name</param>
    /// <param name="elementOne">The first element of the relation</param>
    /// <param name="elementTwo">The second element of the relation</param>
    public void Add(string variable, string elementOne, string elementTwo)
    {
        if (stateRelations.ContainsKey(variable))
        {
            if (stateRelations[variable].ContainsKey(elementOne))
            {
                stateRelations[variable][elementOne].Add(elementTwo);
            }
            else
            {
                stateRelations[variable].Add(elementOne, new List<string>(new string[] { elementTwo }));
            }
        }
        else
        {
            stateRelations.Add(variable, new Dictionary<string, List<string>>());
            stateRelations[variable].Add(elementOne, new List<string>(new string[] { elementTwo }));
        }
    }

    /// <summary>
    /// Removes a relation from the internal state of the planner.
    /// </summary>
    /// <param name="variable">The relation name</param>
    /// <param name="elementOne">The first element of the relation</param>
    /// <param name="elementTwo">The second element of the relation</param>
    public void Remove(string variable, string elementOne, string elementTwo)
    {
        if (stateRelations.ContainsKey(variable) && stateRelations[variable].ContainsKey(elementOne))
        {
            stateRelations[variable][elementOne].Remove(elementTwo);
            if (stateRelations[variable][elementOne].Count == 0)
            {
                stateRelations[variable].Remove(elementOne);
                if (stateRelations[variable].Count == 0)
                {
                    stateRelations.Remove(variable);
                }
            }
        }
    }

    //public Dictionary<string, List<string>> GetStateOfRelation(string variable)
    //{
    //    return stateRelations[variable];
    //}

    //public List<string> GetStateOfRelation(string variable, string elementOne)
    //{
    //    return stateRelations[variable][elementOne];
    //}

    public bool ContainsRelation(string variable)
    {
        return stateRelations.ContainsKey(variable);
    }


    public bool ContainsRelation(string variable, string elementOne)
    {
        return (stateRelations.ContainsKey(variable) && stateRelations[variable].ContainsKey(elementOne));
    }

    public bool ContainsRelationMatch(string variable, string elementTwo)
    {
        if (!stateRelations.ContainsKey(variable))
            return false;

        foreach (KeyValuePair<string, List<string>> pair in stateRelations[variable])
        {
            if (pair.Value.Contains(elementTwo))
                return true;
        }

        return false;
    }


    //public List<string> UnifyRelation(string variable, string elementTwo)
    //{
    //    List<string> unificationList = new List<string>();

    //    if (!stateRelations.ContainsKey(variable))
    //        return unificationList;

    //    foreach (KeyValuePair<string, List<string>> pair in stateRelations[variable])
    //    {
    //        if (pair.Value.Contains(elementTwo))
    //            unificationList.Add(pair.Key);
    //    }

    //    return unificationList;
    //}


    public bool CheckRelation(string variable, string elementOne, string elementTwo)
    {
        if (stateRelations.ContainsKey(variable) && stateRelations[variable].ContainsKey(elementOne)
            && stateRelations[variable][elementOne].Contains(elementTwo))
            return true;
        return false;
    }

    public bool Holds(string variable, string elementOne, string elementTwo)
    {
        return CheckRelation(variable, elementOne, elementTwo);
    }

    private Dictionary<string, Dictionary<string, List<string>>> GetRelationsCopy()
    {
        Dictionary<string, Dictionary<string, List<string>>> copy = new Dictionary<string, Dictionary<string, List<string>>>();
        foreach (KeyValuePair<string, Dictionary<string, List<string>>> pair in stateRelations)
        {
            copy.Add(pair.Key, new Dictionary<string, List<string>>());
            foreach (KeyValuePair<string, List<string>> otherPair in pair.Value)
            {
                copy[pair.Key].Add(otherPair.Key, new List<string>(otherPair.Value));
            }
        }
        return copy;
    }


    private Dictionary<string, List<string>> GetVariablesCopy()
    {
        Dictionary<string, List<string>> copy = new Dictionary<string, List<string>>();
        foreach (KeyValuePair<string, List<string>> pair in stateVariables)
        {
            copy.Add(pair.Key, new List<string>(pair.Value));
        }
        return copy;
    }
}
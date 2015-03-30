using System;
using System.Collections.Generic;
using UnityEngine;

public class GamePlanner
{

    private HTNPlanner planner;
    private bool doneSearching;
    private bool searchSuccess;

    private Dictionary<Goal, string> _goalMap;
    
    public GamePlanner()
    {
        planner = new HTNPlanner(typeof(GameDomain), new GameDomain().GetMethodsDict(), typeof(GameDomain));

        _goalMap = new Dictionary<Goal, string>
        {
            {Goal.Attack, "Attack"},
            {Goal.Explore, "Explore"},
            {Goal.Flee, "Flee"}
        };

    }

    public List<string> GetPlan(Goal goal, State requestorState)
    {
        List<List<string>> goalsTasks = new List<List<string>>();
        goalsTasks.Add(new List<string>(new string[1]{_goalMap[goal]}));

        return planner.SolvePlanningProblem(requestorState, goalsTasks);
    }

    public void CancelSearch()
    {
        planner.CancelSearch = true;
    }

    public bool IsDoneSearching()
    {
        return doneSearching;
    }

    public bool IsSearchSuccessful()
    {
        return searchSuccess;
    }

}
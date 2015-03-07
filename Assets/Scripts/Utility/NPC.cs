using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class NPC
{
    private ID _id;
    private GamePlanner _planner;
    private Goal _currGoal;

    public Goal CurrentGoal { get { return _currGoal; } set { _currGoal = value; } }
    public ID ID { get { return _id; } }

    public NPC(ID id)
    {
        _id = id;
        _planner = new GamePlanner();
    }

    public void RequestPlan(Goal goal)
    {
        _planner.GetPlan(goal);

        _currGoal = goal;
    }
}
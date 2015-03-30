using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class NPC
{
    private ID _id;
    private GamePlanner _planner;
    private Goal _currGoal;
    private Location _currLocation;
    private State _state;
    private int amoAmount, health;

    public Location CurrentLocation { get { return _currLocation; } set { _currLocation = value; } }
    public Goal CurrentGoal { get { return _currGoal; } set { _currGoal = value; } }
    public ID ID { get { return _id; } }
    public State State { get { return _state; } }
    public int AmoSupply { get { return amoAmount; } set { amoAmount = value; } }
    public int Health { get { return health; } set { health = value; } }

    public NPC(ID id)
    {
        _id = id;
        _planner = new GamePlanner();
        _state = new State(id.ToString());

        //defaults
        amoAmount = 0;
        health = 100; 
    }

    public void RequestPlan(Goal goal)
    {
        _planner.GetPlan(goal, _state);

        _currGoal = goal;
    }

}
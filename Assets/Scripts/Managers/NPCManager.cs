using System;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    private static Dictionary<ID, NPC> _npcMap;
    private static NPCManager instance;

    public static NPCManager Instance
    {
        get
        {
            if (instance == null)
                instance = new NPCManager();
            return instance;
        }
    }

    void Start()
    {
        InternalEventManager.Instance.AddListener<GoalChangeTriggerEvent>(RequestPlan);
        InternalEventManager.Instance.AddListener<InitializeStartLocationEvent>(OnInitializeLocation);
        InternalEventManager.Instance.AddListener<HealthChangeEvent>(OnHealthChange);
        _npcMap = new Dictionary<ID, NPC>
        {
            {ID.NPC1, new NPC(ID.NPC1)},
            {ID.NPC2, new NPC(ID.NPC2)},
            {ID.NPC3, new NPC(ID.NPC3)}

        };
    }

    public NPC GetNPCFromID(ID id)
    {
        return _npcMap[id];
    }

    private void OnInitializeLocation(InitializeStartLocationEvent e)
    {
        _npcMap[ID.NPC1].CurrentLocation = e.NPC1_StartLoc;
        Debug.Log("NPC1 Location Set to: [" + _npcMap[ID.NPC1].CurrentLocation.X + "," + _npcMap[ID.NPC1].CurrentLocation.Y + "]");
        InternalEventManager.Instance.Raise(new NPCLocationUpdateEvent(e.NPC1_StartLoc, ID.NPC1));

        _npcMap[ID.NPC2].CurrentLocation = e.NPC2_StartLoc;
        Debug.Log("NPC2 Location Set to: [" + _npcMap[ID.NPC2].CurrentLocation.X + "," + _npcMap[ID.NPC2].CurrentLocation.Y + "]");
        InternalEventManager.Instance.Raise(new NPCLocationUpdateEvent(e.NPC2_StartLoc, ID.NPC2));

        _npcMap[ID.NPC3].CurrentLocation = e.NPC3_StartLoc;
        Debug.Log("NPC3 Location Set to: [" + _npcMap[ID.NPC3].CurrentLocation.X + "," + _npcMap[ID.NPC3].CurrentLocation.Y + "]");
        InternalEventManager.Instance.Raise(new NPCLocationUpdateEvent(e.NPC3_StartLoc, ID.NPC3));
    }

    private void RequestPlan(GoalChangeTriggerEvent e)
    {
        NPC requester = _npcMap[e.CallerID];
        Debug.Log(requester.ID.ToString() + " has requested a new plan based on the goal: " + e.NewGoal.ToString());

        InternalEventManager.Instance.Raise(new UpdateFocusNPCEvent(e.CallerID));
        requester.RequestPlan(e.NewGoal);
    }

    private void OnHealthChange(HealthChangeEvent e)
    {
        NPC npc = _npcMap[e.NPC];
        npc.Health -= 5;

        if (npc.Health < 30 )
        {
            if (npc.State.CheckVariable("is", "firing"))
            {
                InternalEventManager.Instance.Raise(new EndNpcAttackEvent(npc.ToString()));
            }

            InternalEventManager.Instance.Raise(new GoalChangeTriggerEvent(Goal.Flee, npc.ID));
            Debug.Log(npc.ID.ToString() + " health is below 30 and is now fleeing");
        }

        if(npc.Health <= 0 )
        {
            if (npc.State.CheckVariable("is", "firing"))
            {
                InternalEventManager.Instance.Raise(new EndNpcAttackEvent(npc.ToString()));
            }

            InternalEventManager.Instance.Raise(new StatusUIChangeEvent("Dead", npc.ID));
        }

        InternalEventManager.Instance.Raise(new HealthUIChangeEvent(npc.Health, e.NPC));
    }
}
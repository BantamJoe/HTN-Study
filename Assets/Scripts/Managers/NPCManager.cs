using System;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    private Dictionary<ID, NPC> _npcMap;

    void Start()
    {
        InternalEventManager.Instance.AddListener<GoalChangeTriggerEvent>(RequestPlan);

        _npcMap = new Dictionary<ID, NPC>
        {
            {ID.AI1, new NPC(ID.AI1)},
            {ID.AI2, new NPC(ID.AI2)},
            {ID.AI3, new NPC(ID.AI3)}

        };
    }

    private void RequestPlan(GoalChangeTriggerEvent e)
    {
        NPC requester = _npcMap[e.CallerID];
        requester.RequestPlan(e.NewGoal);
    }
}
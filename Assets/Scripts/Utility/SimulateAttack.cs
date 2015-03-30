using System;
using UnityEngine;
using System.Collections;

public class SimulateAttack : MonoBehaviour
{
    private static SimulateAttack instance;
    private bool playerDead = false;

    public static SimulateAttack Instance
    {
        get
        {
            if (instance == null)
                instance = new SimulateAttack();

            return instance;
        }
    }

    void Start()
    {
        InternalEventManager.Instance.AddListener<BeginNpcAttackEvent>(OnBeginAttack);
        InternalEventManager.Instance.AddListener<EndNpcAttackEvent>(OnEndAttack);
        InternalEventManager.Instance.AddListener<PlayerDeadEvent>(OnPlayerDead);
    }

    private void OnPlayerDead(PlayerDeadEvent e)
    {
        playerDead = true;
    }

    public void StartPlayerAttack()
    {
        ID targetNpc = LocationManager.Instance.FindClosestNPC();
        StartCoroutine(SimulatePlayerFire(NPCManager.Instance.GetNPCFromID(targetNpc)));
    }

    private void OnBeginAttack(BeginNpcAttackEvent e)
    {
        Debug.Log(e.NPCRef.ID.ToString() + " has started firing");
        InternalEventManager.Instance.Raise(new StatusUIChangeEvent("Attacking", e.NPCRef.ID));

        StartCoroutine(SimulateFire(e.NPCRef));
    }

    private IEnumerator SimulateFire(NPC npc)
    {
        int amoSupply = npc.AmoSupply;

        for (; amoSupply >= 0; amoSupply--)
        {
            InternalEventManager.Instance.Raise(new PlayerDamageEvent());
            InternalEventManager.Instance.Raise(new AmoSupplyUIChangeEvent(npc.ID, amoSupply));
            yield return new WaitForSeconds(1.1f);
        }

        //When there is no more amo, we flee
        Debug.Log(npc.ID.ToString() + " has no more amo and is now fleeing");
        InternalEventManager.Instance.Raise(new GoalChangeTriggerEvent(Goal.Flee, npc.ID));
        yield return new WaitForSeconds(2f);
        
    }

    private IEnumerator SimulatePlayerFire(NPC target)
    {
        while(target.Health > 0 || (!playerDead))
        {
            InternalEventManager.Instance.Raise(new HealthChangeEvent(target.ID));
            yield return new WaitForSeconds(1.1f);
        }

        //if player isn't dead that means the target has died and we need to choose a new target
        if (!playerDead)
            StartPlayerAttack();

        yield return new WaitForSeconds(2f);
    }

    private void OnEndAttack(EndNpcAttackEvent e)
    {
        NPC npc = e.NPCRef;
        StopCoroutine(SimulateFire(e.NPCRef));
        Debug.Log(npc.ID.ToString() + " has stopped firing");
    }
}


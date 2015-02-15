using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BehaviourUIManager : MonoBehaviour
{
    private static BehaviourUIManager _instance = null;
    private BehaviourUILib _uiLib = null;

    public static BehaviourUIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BehaviourUIManager();
                LogManager.Instance.Log("BehaviourUIManager initialized.");
            }

            return _instance;
        }
    }

    void Start()
    {
        InternalEventManager.Instance.AddListener<ThoughtChangeEvent>(OnThoughtDisplayChange);
        InternalEventManager.Instance.AddListener<HealthChangeEvent>(OnHealthDisplayChange);
        InternalEventManager.Instance.AddListener<StatusChangeEvent>(OnStatusDisplayChange);

        _uiLib = new BehaviourUILib();
        LogManager.Instance.Log("BehaviourUIManager initialized.");
    }

    private void OnThoughtDisplayChange(ThoughtChangeEvent e)
    {
        _uiLib.SetActorThoughtSprite(e.ActorTarget, e.Thought);
        e.LogEvent();
    }

    private void OnHealthDisplayChange(HealthChangeEvent e)
    {
        _uiLib.SetActorHealth(e.ActorTarget, e.Amount);
        e.LogEvent();
    }

    private void OnStatusDisplayChange(StatusChangeEvent e)
    {
        _uiLib.SetActorStatus(e.ActorTarget, e.Status);
        e.LogEvent();
    }

    void OnDestroy()
    {
        InternalEventManager.Instance.RemoveListener<ThoughtChangeEvent>(OnThoughtDisplayChange);
        InternalEventManager.Instance.RemoveListener<HealthChangeEvent>(OnHealthDisplayChange);
        InternalEventManager.Instance.RemoveListener<StatusChangeEvent>(OnStatusDisplayChange);
    }
}


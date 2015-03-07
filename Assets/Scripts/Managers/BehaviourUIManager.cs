using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BehaviourUIManager : MonoBehaviour
{
    private static BehaviourUIManager _instance = null;

    private Dictionary<ID, Slider> _healthBars = new Dictionary<ID, Slider>();
    private Dictionary<ID, Image> _thoughts = new Dictionary<ID, Image>();
    private Dictionary<ID, Text> _status = new Dictionary<ID, Text>();

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

        InitializeUIComponents();
        LogManager.Instance.Log("BehaviourUIManager initialized.");
    }

    private void InitializeUIComponents()
    {
        InitializeActor(ID.AI1);
        InitializeActor(ID.AI2);
        InitializeActor(ID.AI3);
        InitializeActor(ID.Player);
    }

    private void InitializeActor(ID id)
    {
        GameObject temp = GameObject.FindGameObjectWithTag(id.ToString());

        Slider slider = temp.GetComponent<Slider>();
        //Image thought;

        _healthBars.Add(id, slider);
    }

    private void OnThoughtDisplayChange(ThoughtChangeEvent e)
    {
        //_uiLib.SetActorThoughtSprite(e.ActorTarget, e.Thought);
        e.LogEvent();
    }

    private void OnHealthDisplayChange(HealthChangeEvent e)
    {
        //_uiLib.SetActorHealth(e.ActorTarget, e.Amount);
        Slider slider = _healthBars[e.ActorTarget];
        slider.value = e.Amount;
        e.LogEvent();
    }

    private void OnStatusDisplayChange(StatusChangeEvent e)
    {
        //_uiLib.SetActorStatus(e.ActorTarget, e.Status);
        e.LogEvent();
    }

    void OnDestroy()
    {
        InternalEventManager.Instance.RemoveListener<ThoughtChangeEvent>(OnThoughtDisplayChange);
        InternalEventManager.Instance.RemoveListener<HealthChangeEvent>(OnHealthDisplayChange);
        InternalEventManager.Instance.RemoveListener<StatusChangeEvent>(OnStatusDisplayChange);
    }
}


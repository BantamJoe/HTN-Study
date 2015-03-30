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
    private Dictionary<ID, Text> _amoSupply = new Dictionary<ID, Text>();

    private Dictionary<Goal, Sprite> _thoughtImages = new Dictionary<Goal, Sprite>();
    
    public static BehaviourUIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BehaviourUIManager();
            }

            return _instance;
        }
    }

    void Start()
    {
        InternalEventManager.Instance.AddListener<HealthUIChangeEvent>(OnHealthDisplayChange);
        InternalEventManager.Instance.AddListener<StatusUIChangeEvent>(OnStatusDisplayChange);
        InternalEventManager.Instance.AddListener<AmoSupplyUIChangeEvent>(OnAmoSupplyChangeEvent);
        InternalEventManager.Instance.AddListener<GoalChangeTriggerEvent>(OnThoughtDisplayChange);
        
        InitializeUIComponents();
        LoadThoughts();

        Debug.Log("BehaviourUIManager initialized.");
    }

    private void InitializeUIComponents()
    {
        InitializeNpcUI(ID.NPC1);
        InitializeNpcUI(ID.NPC2);
        InitializeNpcUI(ID.NPC3);
        InitializeNpcUI(ID.Player);
    }

    private void InitializeNpcUI(ID id)
    {
        Slider slider = GameObject.FindGameObjectWithTag(id.ToString()).GetComponentInChildren<Slider>();
        _healthBars.Add(id, slider);

        if(!(id == ID.Player))
        {
            Image thought = GameObject.FindGameObjectWithTag(id.ToString() + "Thought").GetComponent<Image>();
            _thoughts.Add(id, thought);

            Text status = GameObject.FindGameObjectWithTag(id.ToString() + "Status").GetComponent<Text>();
            _status.Add(id, status);

            Text amoSupply = GameObject.FindGameObjectWithTag(id.ToString() + "Amo").GetComponent<Text>();
            _amoSupply.Add(id, amoSupply);
        }
    }

    private void LoadThoughts()
    {
        Sprite[] thoughts = Resources.LoadAll<Sprite>("Thoughts/");

        _thoughtImages.Add(Goal.Attack, thoughts[0]);
        _thoughtImages.Add(Goal.Explore, thoughts[1]);
        _thoughtImages.Add(Goal.Flee, thoughts[2]);
    }

    private void OnAmoSupplyChangeEvent(AmoSupplyUIChangeEvent e)
    {
        _amoSupply[e.NPC].text = e.AmoValue;
    }

    private void OnThoughtDisplayChange(GoalChangeTriggerEvent e)
    {
        _thoughts[e.CallerID].sprite = _thoughtImages[e.NewGoal];
    }

    private void OnHealthDisplayChange(HealthUIChangeEvent e)
    {
        if(e.Amount != 0)
        {
            Slider slider = _healthBars[e.NPC];
            slider.value = e.Amount;
        }
    }

    private void OnStatusDisplayChange(StatusUIChangeEvent e)
    {
        _status[e.NPC].text = e.Status;

    }

    void OnDestroy()
    {
        //InternalEventManager.Instance.RemoveListener<HealthChangeEvent>(OnHealthDisplayChange);
        //InternalEventManager.Instance.RemoveListener<StatusChangeEvent>(OnStatusDisplayChange);
    }
}


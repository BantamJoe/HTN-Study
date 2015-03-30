using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/** also includes player  obj **/

public class WorldStateManager : MonoBehaviour
{
    private static Player _player;

    public InputField AmoAmmount;
    public InputField PlayerLoc;
    public InputField Goal;

    public InputField NPC1Loc, NPC2Loc, NPC3Loc;

    public Slider PlayerHealthBar;

    public Image start;
    private Button StartButton;

    void Start()
    {
        StartButton = start.GetComponent<Button>();
        StartButton.onClick.AddListener(() => { StartWorld(); });

        InternalEventManager.Instance.AddListener<PlayerDamageEvent>(OnPlayerDamage);
        InternalEventManager.Instance.AddListener<ZeroWorldAmoSupplyEvent>(OnZeroAmoLeft);
    }

    public Player PlayerRef { get { return _player; } }

    public void OnPlayerDamage(PlayerDamageEvent e)
    {
        _player.Health -= 1;
        PlayerHealthBar.value = _player.Health;

        if(_player.Health == 0)
        {
            Debug.Log("Player has died");
            StopAllCoroutines();
            //InternalEventManager.Instance.Raise(new GoalChangeTriggerEvent(Goal.))
            //InternalEventManager.Instance.Raise(new PlayerDeadEvent());
        }
    }

    private void OnZeroAmoLeft(ZeroWorldAmoSupplyEvent e)
    {
        Debug.Log("There is no more amo left in world!");
        StopAllCoroutines();
    }

    private void StartWorld()
    {
        SetAmo();
        SetPlayerLocation();
        SetNPCLocations();
        SetGoal();
    }

    private void SetAmo()
    {
        int amoAmount = Int16.Parse(AmoAmmount.text.ToString());
        LocationManager.Instance.SetAmoLocations(amoAmount);
    }

    private void SetGoal()
    {
        Goal goal = (Goal)Enum.Parse(typeof(Goal), Goal.text.ToString());

        InternalEventManager.Instance.Raise(new GoalChangeTriggerEvent(goal, ID.NPC1));
        InternalEventManager.Instance.Raise(new GoalChangeTriggerEvent(goal, ID.NPC2));
        InternalEventManager.Instance.Raise(new GoalChangeTriggerEvent(goal, ID.NPC3));
    }

    public void SetPlayerLocation()
    {
        string playLoc = PlayerLoc.text.ToString();
        int x = (int)char.GetNumericValue(playLoc[0]);
        int y = (int)char.GetNumericValue(playLoc[1]);

        _player = new Player();
        _player.location = new Location(x, y, Destination.None);

        InternalEventManager.Instance.Raise(new PlayerLocationUpdateEvent(_player.location));
        Debug.Log("Player Location Set to: [" + x + "," + y + "]");
    }

    public void SetNPCLocations()
    {
        string npc1 = NPC1Loc.text.ToString();
        string npc2 = NPC2Loc.text.ToString();
        string npc3 = NPC3Loc.text.ToString();

        InternalEventManager.Instance.Raise(new InitializeStartLocationEvent(npc1, npc2, npc3));
    }

}


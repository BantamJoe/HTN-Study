using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEditor.Sprites;
using UnityEngine;

/* this is super hacky **/
public class BehaviourUILib
{
    private Dictionary<Actor, string> _actorUILib;
    private ThoughtSpriteLib _thoughtLib;

    public BehaviourUILib()
    {
        _actorUILib = new Dictionary<Actor, string>();
        _thoughtLib = new ThoughtSpriteLib();

        generateActorUILib();
    }

    private GameObject shiftUIElementFocus(Actor actor, ActorUIElement element)
    {
        string focusTag;
        _actorUILib.TryGetValue(actor, out focusTag);
        
        if(focusTag != null)
        {
            return GameObject.FindGameObjectWithTag(focusTag + element.ToString() );
        }

        Debug.Log("Actor UI Element retrieval failed!");
        return null;
    }

    public void SetActorThoughtSprite(Actor actor, Thought thought)
    { 
        GameObject thoughtUI = shiftUIElementFocus(actor, ActorUIElement.Thought);
        Image thoughtImg = thoughtUI.GetComponent<Image>();

        thoughtImg.sprite = _thoughtLib.GetThoughtSprite(thought);
    }

    public void SetActorHealth(Actor actor, int amount)
    {
         GameObject healthUI = shiftUIElementFocus(actor, ActorUIElement.Health);
         Slider healthSlider = healthUI.GetComponent<Slider>();

        /**change health values here **/
         healthSlider.value = amount;

    }

    public void SetActorStatus(Actor actor, string status)
    {
         GameObject statusUI = shiftUIElementFocus(actor, ActorUIElement.Status);
         Text statusText = statusUI.GetComponent<Text>();

        /**change status text here **/
         statusText.text = status;
    }

    private void generateActorUILib()
    {
        _actorUILib.Add(Actor.AI1, "AI1_");
        _actorUILib.Add(Actor.AI2, "AI2_");
        _actorUILib.Add(Actor.AI3, "AI3_");
        _actorUILib.Add(Actor.Player, "Player_");
    }

}

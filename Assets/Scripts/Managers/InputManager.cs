using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

//public class InputDataEvent : IEvent
//{
//    private InputField _input;
//    public InputField Input { get { return _input; } }

//    public InputDataEvent(InputField input)
//    {
//        _input = input;
//    }
//}

public class InputManager : IPointerDownHandler, ISelectHandler, IDeselectHandler
{
    private InputField _input;

    public void OnPointerDown(PointerEventData eventData)
    {
        string name = eventData.pointerEnter.name;
        //UIManager.Instance.InvokeAction(name);
    }

    public void OnSelect(BaseEventData eventData)
    {
        _input = eventData.selectedObject.GetComponent<InputField>();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (_input != null)
        {
            //Debug.Log(_input.name);
            ProcessInputData(_input);
            //InternalEventManager.Instance.Raise(new InputDataEvent(_input));
            _input = null;
        }
    }

    private void ProcessInputData(InputField input)
    {
        //string data = input.text;
        //string name = input.name;

        //switch (name)
        //{
        //    case "AmoAmount": break;
        //    case "PlayerLocation": break;
        //    case "Goal": break;
        //    default:
        //        if (name.Contains("StartLocation"))
        //        {
        //            InternalEventManager.Instance.Raise();
        //        }
        //        break;
        //}
    }

}
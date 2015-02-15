using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Manager<ClassType> where ClassType : new()
{
    private static ClassType _instance;

    public static ClassType Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new ClassType();
#if UNITY_EDITOR
                Debug.Log(typeof(ClassType).ToString() + " was initialized.");
#endif
            }
            return _instance;
        }
    }

    
    /* destructors needed possibly?
     * */
}

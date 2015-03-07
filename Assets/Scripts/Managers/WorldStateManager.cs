using System;
using System.Collections.Generic;

public class WorldStateManager 
{
    private static WorldStateManager _instance;

    public static WorldStateManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new WorldStateManager();

            return _instance;
        }
    }

    public State GetCurrentState()
    {
        return null;
    }

    private void SetAmoLocations()
    {

    }

}


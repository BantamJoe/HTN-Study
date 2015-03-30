using System;
using UnityEngine;
using System.Collections.Generic;
using System.Security.Cryptography;

public enum Territory { Desert, Forest, Lake, Warehouse }

public enum Destination { Commrades, Amo, Player, Random, None}

public class Location
{
    private int _x, _y;
    private Destination _dest;

    //This is temporary, obviously amo amount shouldn't live here
    private int _amoAmount;

    public int X { get { return _x; } }
    public int Y { get { return _y; } }
    public Destination Dest { get { return _dest; } }
    public int AmoAmount { get { return _amoAmount; } }

    public Location(int x, int y)
    {
        _x = x;
        _y = y;
    }
    public Location(int x, int y, Destination dest)
    {
        _x = x;
        _y = y;
        _dest = dest;
    }

    public Location(int x, int y, Destination dest, int amoAmount)
    {
        _x = x;
        _y = y;
        _dest = dest;
        _amoAmount = amoAmount;
    }
}

public class LocationManager
{
    private List<Location> _amoLocation;
    private static Location _playerCurrLocation;
    private CryptoRandom rng = new CryptoRandom();
    private static ID _caller;

    private static Dictionary<ID, Location> _commradeLoc = new Dictionary<ID, Location>
    {
        {ID.NPC1, new Location(0,0)},
        {ID.NPC2, new Location(0,0)},
        {ID.NPC3, new Location(0,0)}
    };

    private Dictionary<Territory, int[,]> _territoryMap = new Dictionary<Territory,int[,]>
    {
        {Territory.Desert, new int[,]{{0,5}, {0,6}, {0,7}, 
                                      {1,5}, {1,6}, {1,7},
                                      {2,5}, {2,6}, {2,7},
                                      {3,5}, {3,6}, {3,7}
        }},
        {Territory.Forest, new int[,]{{4,3}, {4,4}, {4,5}, {4,6}, {4,7},
                                      {5,3}, {5,4}, {5,5}, {5,6}, {5,7},
                                      {6,3}, {6,4}, {6,5}, {6,6}, {6,7},
                                      {7,3}, {7,4}, {7,5}, {7,6}, {7,7}
        }},
        {Territory.Lake, new int[,]{{4,0}, {4,1}, {4,2},
                                    {5,0}, {5,1}, {5,2},
                                    {6,0}, {6,1}, {6,2},
                                    {7,0}, {7,1}, {7,2}
        }},
        {Territory.Warehouse, new int[,]{{0,0}, {0,1}, {0,2}, {0,3}, {0,4},
                                         {1,0}, {1,1}, {1,2}, {1,3}, {1,4},
                                         {2,0}, {2,1}, {2,2}, {2,3}, {2,4},
                                         {3,0}, {3,1}, {3,2}, {3,3}, {3,4}
        
        }}
    };

    private static LocationManager _instance;

    public static LocationManager Instance 
    {
        get
        {
            if (_instance == null)
            {
                _instance = new LocationManager();
                Initialize();
            }
            return _instance;
        }
    }

    public static void Initialize()
    {
        InternalEventManager.Instance.AddListener<NPCLocationUpdateEvent>(OnNPCLocationUpdateEvent);
        InternalEventManager.Instance.AddListener<PlayerLocationUpdateEvent>(OnPlayerLocationUpdateEvent);
        InternalEventManager.Instance.AddListener<UpdateFocusNPCEvent>(OnUpdateFocusNPC);
    }

    private static void OnUpdateFocusNPC(UpdateFocusNPCEvent e)
    {
        _caller = e.CallerID;
    }

    private static void OnNPCLocationUpdateEvent(NPCLocationUpdateEvent e)
    {
        _commradeLoc[e.NPC] = e.NewLocation;
    }

    private static void OnPlayerLocationUpdateEvent(PlayerLocationUpdateEvent e)
    {
        _playerCurrLocation = e.NewLocation;
    }

    public Location FindDestinationLoc(Destination dest)
    {
        switch(dest)
        {
            case Destination.Amo: return FindAmo();
            case Destination.Commrades: return FindCommrades();
            case Destination.Player: return FindPlayer();
            case Destination.Random: return FindRandom();
        }

        Debug.Log("Could not find destination: " + dest.ToString());
        return null;
    }


    private Location FindPlayer()
    {
        Location callerLoc = _commradeLoc[_caller];
        //Checks if player is within range

        if (CheckLocationRange(callerLoc, _playerCurrLocation))
        {
            return _playerCurrLocation;
        }

        //Returns null if player is not close enough for npc to find, this will be need to rewritten (logically doesnt make sense)
        return null;
    }
    private Location FindRandom()
    {
        return new Location(getRandom(), getRandom(), Destination.Random);
    }

    public void SetAmoLocations(int amount)
    {
        _amoLocation = new List<Location>();


         Debug.Log("Total Amo[" + amount + "] has been allocated to the following locations: ");
        for(int i = 0; i < amount; i++)
        {
            _amoLocation.Add(new Location(getRandom(), getRandom(), Destination.None, getRandomAmoAmount()));
            Debug.Log("[" + _amoLocation[i].X + "," + _amoLocation[i].Y + "] - holding [" + _amoLocation[i].AmoAmount +"] rounds");
        }

    }

    public ID FindClosestNPC()
    {
        foreach (var pair in _commradeLoc)
        {
            Location candidateLoc = _commradeLoc[pair.Key];
            if (CheckLocationRange(_playerCurrLocation, candidateLoc))
            {
                return pair.Key;
            }
        }

        return ID.Player;
    }

    private Location FindCommrades()
    {
        Location callerLoc = _commradeLoc[_caller];
        //looks for closest NPC
        foreach(var pair in _commradeLoc)
        {
            if (pair.Key != _caller)
            {
                Location candidateLoc = _commradeLoc[pair.Key];
                if (CheckLocationRange(callerLoc, candidateLoc))
                {
                    return candidateLoc;
                }
            }
        }
        
        //returns null if no commrades are close by
        return null;
    }

    private bool CheckLocationRange(Location caller, Location candidate)
    {
        int startX = caller.X - 1, startY = caller.Y - 1;
        int endX = caller.X + 1, endY = caller.Y + 1;

        if (candidate.X >= startX && candidate.X <= endX)
            if (candidate.Y >= startY && candidate.Y <= endY)
                return true;

        return false;
    }

    private Location FindAmo()
    {
        //looks for closest amo, ughhh... will just choose the first amo location found in list 

        if (_amoLocation.Count == 0)
        {
            InternalEventManager.Instance.Raise(new ZeroWorldAmoSupplyEvent());
            return null;
        }

        Location amoLoc = _amoLocation[0];
        _amoLocation.RemoveAt(0);
        NPCManager.Instance.GetNPCFromID(_caller).AmoSupply = amoLoc.AmoAmount;

        return amoLoc;
    }

    private int getRandom()
    {
        return rng.Next(0, 9);
    }

    private int getRandomAmoAmount()
    {
        return rng.Next(10, 31);
    }
}

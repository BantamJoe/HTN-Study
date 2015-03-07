using System;
using System.Collections.Generic;

public enum Territory { Desert, Forest, Lake, Warehouse }

public enum Destination { Commrades, Amo, Player, Random}

public class Location
{
    private int _x, _y;
    private Destination _dest;

    public int X { get { return _x; } }
    public int Y { get { return _y; } }
    public Destination Dest { get { return _dest; } }
    public Location(int x, int y, Destination dest)
    {
        _x = x;
        _y = y;
        _dest = dest;
    }
}

public class LocationManager
{
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

    public static LocationManager Instance { get { return _instance; } }

    public LocationManager()
    {
        if (_instance == null)
            _instance = new LocationManager();
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

        LogManager.Instance.Log("Could not find destination: " + dest.ToString());
        return null;
    }

    private Location FindPlayer()
    {
        
        return null;
    }

    private Location FindRandom()
    {
        //returns random location
        return null;
    }

    private Location FindCommrades()
    {
        //looks for closest NPC
        return null;
    }

    private Location FindAmo()
    {
        //looks for closest amo
        return null;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Player
{
    public Location location;
    private int _health;
    
    public Location Location { get { return location; } set { location = value; } }
    public int Health { get { return _health; } set { _health = value; } }

    public Player ()
    {
        _health = 100;
    }
}


using System;
using System.Collections.Generic;

public static class EnumerationParse
{
    public static T GetEnumEquivalent<T>(string str)
    {
        return (T)Enum.Parse(typeof(T), str);
    }
}


public enum ID { NPC1, NPC2, NPC3, Player};

public enum ActorUIElement { Health, Status, Thought };

public enum Goal { Attack, Explore, Flee}
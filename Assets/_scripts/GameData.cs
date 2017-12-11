using System;
using System.Collections.Generic;

public static class GameData {
    public static int LeftSideScore { get; set; }

    public static int RightSideScore { get; set; }
    
    public static Dictionary<int, Stats> BoxScore = new Dictionary<int, Stats>();
}

public class Stats
{
    // player hits the ball
    public int hits;

    // player dashes into ball
    public int dashHits;

    // player hits ball into the ground 
    public int points;
    
    // player hits ball on own side
    public int ownGoals;

    public Stats()
    {
        hits = 0;
        dashHits = 0;
        points = 0;
        ownGoals = 0;
    }

    public int TotalHits()
    {
        return hits + dashHits;
    }

    public override string ToString()
    {
        return String.Format("{0}\n{1}\n{2}", points, hits, dashHits);
    }
}

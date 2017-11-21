using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData {
    private static int leftSideScore;
    private static int rightSideScore;

    public static int LeftSideScore {
        get {
            return leftSideScore;
        }
        set {
        leftSideScore = value;
        }
    }

	public static int RightSideScore {
        get {
            return rightSideScore;
        }
        set {
            rightSideScore = value;
        }
    }
}

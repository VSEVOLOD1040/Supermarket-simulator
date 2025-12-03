using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public float Balance;
    public int UpgradePoints;
    public GameData(float balance, int UpgradePoints)
    {
        Balance = balance;
        this.UpgradePoints = UpgradePoints;
    }

    
}

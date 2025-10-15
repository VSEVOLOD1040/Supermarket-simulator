using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public int Balance;

    public GameData(int balance)
    {
        Balance = balance;
    }
}

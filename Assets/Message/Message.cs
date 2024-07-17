// LevelUpMessage.cs
using System;

public class LevelUpMessage : Message
{
    public int NewLevel { get; }

    public Action<int> Callback;

    public LevelUpMessage(int newLevel) : base()
    {
        NewLevel = newLevel;
    }
    public LevelUpMessage(int newLevel, Action<int> call) : base()
    {
        NewLevel = newLevel;
        Callback = call;
    }
}

// PlayerHealthChangedMessage.cs
public class PlayerHealthChangedMessage : Message
{
    public float NewHealth { get; }

    public PlayerHealthChangedMessage(float newHealth) : base()
    {
        NewHealth = newHealth;
    }
}

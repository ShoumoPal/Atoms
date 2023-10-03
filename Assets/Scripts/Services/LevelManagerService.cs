using System;
using UnityEngine;

public enum LevelStatus
{
    LOCKED,
    UNLOCKED,
    COMPLETED
}

[Serializable]
public class Level
{
    public string Name;
    public Vector3 StartPosition;
}

public class LevelManagerService : GenericMonoSingleton<LevelManagerService>
{
    public Level[] Levels;

    public void SetLevelStatus(string _levelName, LevelStatus _status)
    {
        Level level = Array.Find(Levels, i => i.Name == _levelName);
    }
}

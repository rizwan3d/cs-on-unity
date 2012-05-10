using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UserCommand
{
    FORWARD,                // go forward
    BACKWARD,               // go backward
    LEFT,                   // go left
    RIGHT,                  // go right
    CHANGE_DIRECTION,       // change the direction    
    SHOT,                   // a bullet/grenade has shot/throw
    JUMP,                   // jump (just jump, not move)
    LONG_JUMP,              // jump higher
    BOW,                    // cui' dau`
    CHANGE_WEAPON           // change the weapon
}

public enum ServerNotice
{
    POSITION,               // notice for FORWARD, BACKWARD, LEFT, RIGHT, JUMP, LONG_JUMP
    SHOT,                   // notice a shoot
    BOW,                    // notice user has changed bowing
    CHANGE_WEAPON           // notice user has changed weapon
}





public class Command
{
    UserCommand commandType;
    float happenedTime;         // the point of time when the command happened
    object commandData;         // data of the command

    public UserCommand CommandType
    {
        get { return commandType; }
        set { commandType = value;}
    }

    public float HappenedTime
    {
        get { return happenedTime; }
        set { happenedTime = value; }
    }

    public object CommandData
    {
        get { return commandData; }
        set { commandData = value; }
    }
}

public class ClientSnapshot
{
    string clientId;
    List<Command> commands;

    public ClientSnapshot()
    {
        clientId = "";
        commands = new List<Command>();
    }
}

public class Notice
{
    string clientId;                    // id of client that was changed
    ServerNotice noticeType;
    float happenedTime;
    object noticeData;

    public string ClientId
    {
        get { return clientId; }
        set { clientId = value; }
    }
    public ServerNotice NoticeType
    {
        get { return noticeType; }
        set { noticeType = value; }
    }
    public float HappenedTime
    {
        get { return happenedTime; }
        set { happenedTime = value; }
    }

    public object NoticeData
    {
        get { return noticeData; }
        set { noticeData = value; }
    }
}

public class ServerSnapshot
{
    List<Notice> notices;

    public ServerSnapshot()
    {
        notices = new List<Notice>();
    }
}



public enum GameState
{
    INIT_GENERAL,
    INIT_SERVER,
    INIT_CLIENT,
    PLAYING,
    RESETTING,
    PAUSE
}

public enum PlayerState
{
    DEATH,				// new to game or 
    PLAYING
}

public enum Team
{
    Counters,
    Terriors
}

public class CSCharacter
{
    public GameObject _model;
    public string _playerId;
    public string _playerName;
    public PlayerState _playerState;
}




public class CSCharacterPrefab
{
    public GameObject _model;
    public string _name;
}

public struct CSCharactersName
{
    public static string player = "Player",
        urban = "Urban";
}


public class CSMapPrefab
{
    public GameObject _model;
    public string _name;

    // return the generator in each map (the position where player will be spawn)
    static System.Random ran = new System.Random();
    public static Transform GetGenerator(Team _team, string mapName)
    {
        ran.Next(0, 1);
        Transform generator;
        GameObject model = PrefabLoader.GetMapModel(mapName);
        if (_team == Team.Counters)
        {
            Transform generators = model.transform.FindChild("CounterGenerates");
            int index = ran.Next(0, generators.childCount);
            generator = generators.GetChild(index);
        }
        else
        {
            Transform generaters = model.transform.FindChild("TerriorGenerates");
            int index = ran.Next(0, generaters.childCount);
            generator = generaters.GetChild(index);
        }
        return generator;
    }
}
public struct CSMapsName
{
    public static string cs_mansion = "CSMansion";
}


public class CSWeaponPrefab
{
    public GameObject _model;
    public string _name;
}
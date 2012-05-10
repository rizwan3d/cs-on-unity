using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;



public class GameController : MonoBehaviour {
	public int				_maxPlayers;
	public int				_port;
	
	
	public string			_playerName;
	public Team				_team;
	public GameObject 		_player;
	public GameObject 		_choosingMap;
	public List<CSCharacter> _lstCounters;
	public List<CSCharacter> _lstTerriors;
	
	public static GameState	_gameState;
	
	int choosingMapIndex;
	public int MaxConnections
	{
		get{return _maxPlayers - 1;}
	}
	
	
	// Use this for initialization
	void Start () {
		_lstCounters = new List<CSCharacter>();
		_lstTerriors = new List<CSCharacter>();
		_playerName = "0160";
		_maxPlayers = 8;
		_port = 6969;
		_team = Team.Counters;
		_gameState = GameState.INIT_GENERAL;
		Screen.showCursor = true;
		
		choosingMapIndex = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(Network.peerType == NetworkPeerType.Server)
		{
			ServerUpdate();
		}else if(Network.peerType == NetworkPeerType.Client)
		{
			ClientUpdate();
		}
	}
	
	void OnGUI()
	{	
		switch(_gameState)
		{
		case GameState.INIT_GENERAL:
			
			GUI.BeginGroup(new Rect (Screen.width / 2 - 150, Screen.height / 2 - 50, 300, Screen.height / 2));
			
			GUILayout.BeginVertical(GUILayout.MinWidth(300));
			// player name
			GUILayout.BeginHorizontal();
			GUILayout.Label("Player name ");
			_playerName = GUILayout.TextField(_playerName);
			GUILayout.EndHorizontal();	
			// team
			GUILayout.BeginHorizontal();
			GUILayout.Label("Choosing team ");
			GUILayout.Label(_team == Team.Counters?"COUNTER":"TERRIOR");
			if(GUILayout.Button("Counter"))
			{
				_team = Team.Counters;
			}
			if(GUILayout.Button("Terrior"))
			{
				_team = Team.Terriors;
			}
			GUILayout.EndHorizontal();
			
			// map
			GUILayout.Label("Choose map ");
			GUILayout.BeginHorizontal();			
			if(GUILayout.Button("<<"))
			{
				choosingMapIndex--;
				if(choosingMapIndex < 0)
					choosingMapIndex = 0;
                else if (choosingMapIndex >= PrefabLoader.maps.Count && PrefabLoader.maps.Count > 0)
                    choosingMapIndex = PrefabLoader.maps.Count - 1;					
			}
            GUILayout.Label(PrefabLoader.maps[choosingMapIndex]._name);
			if(GUILayout.Button(">>"))
			{
				choosingMapIndex++;
				if(choosingMapIndex < 0)
					choosingMapIndex = 0;
                else if (choosingMapIndex >= PrefabLoader.maps.Count && PrefabLoader.maps.Count > 0)
                    choosingMapIndex = PrefabLoader.maps.Count - 1;
			}
			GUILayout.EndHorizontal();
			
			// number of players
			GUILayout.BeginHorizontal();
			GUILayout.Label("Number of players ");
			_maxPlayers = int.Parse(GUILayout.TextField(_maxPlayers.ToString().Length <= 0?"0":_maxPlayers.ToString()));
			
			GUILayout.EndHorizontal();
			
			if(GUILayout.Button("Create new game server"))
			{					
				_gameState = GameState.INIT_SERVER;
			}
			
			GUILayout.Label("Or join to one of following servers ");
			GUILayout.BeginHorizontal();
			GUILayout.Label("server01  1/10");
			if(GUILayout.Button("Join"))
			{
				_gameState = GameState.INIT_CLIENT;
			}			
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			GUILayout.Label("server02  1/10");
			if(GUILayout.Button("Join"))
			{
				_gameState = GameState.INIT_CLIENT;
			}			
			GUILayout.EndHorizontal();			
			
			GUILayout.BeginHorizontal();
			GUILayout.Label("server03  1/10");
			if(GUILayout.Button("Join"))
			{
				_gameState = GameState.INIT_CLIENT;
			}			
			GUILayout.EndHorizontal();
			
			GUILayout.EndVertical();
			GUI.EndGroup();
			break;
		case GameState.INIT_SERVER:
			InitServer();
			break;
		case GameState.INIT_CLIENT:
			InitClient();
			break;
		}
	}
	
/*
 *	CLIENT ------------------------------------------------------------------------------ 
 * 
*/ 
	protected void ClientUpdate()
	{
	}
	
	protected void InitClient()
	{
		
	}
	
/*
 *	SERVER ------------------------------------------------------------------------------ 
 * 
*/ 	
	protected void ServerUpdate()
	{
	}
	
	
	protected void InitServer()
	{		
		if(Network.InitializeServer(MaxConnections, _port) == NetworkConnectionError.NoError)
		{
			//init map
            _choosingMap = PrefabLoader.GetMapModel(CSMapsName.cs_mansion);
			Instantiate(_choosingMap, Vector3.zero, Quaternion.LookRotation(Vector3.forward, Vector3.up));
			
			//init player			
            Transform generator = CSMapPrefab.GetGenerator(_team, CSMapsName.cs_mansion);
            _player = PrefabLoader.GetCharacterModel(CSCharactersName.player);
            Instantiate(_player, generator.transform.position, generator.rotation);

			Screen.showCursor = false;
			_gameState = GameState.PLAYING;
		}
	}
}

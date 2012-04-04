using UnityEngine;
using System.Collections;

public enum ClientMessages
{
	UPDATE_POSITION,
	FIRE
}

public enum ServerMessages
{
	UPDATE_CLIENT_POSITION,
	NOTICE_KILLED_CLIENTS,
	NOTICE_FIRED_CLIENTS
}


public struct CSMessage
{
	public int _messageType;
	public float _happenedTime;
	public object _data;
};


public class GameNetworkController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

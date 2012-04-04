// ----------------------------------------------------------------------------
// <copyright file="Enums.cs" company="Exit Games GmbH">
//   PhotonNetwork Framework for Unity - Copyright (C) 2011 Exit Games GmbH
// </copyright>
// <summary>
//   
// </summary>
// <author>developer@exitgames.com</author>
// ----------------------------------------------------------------------------

public enum ConnectionState
{
    Disconnected,
    Connecting,
    Connected,
    Disconnecting,
    InitializingApplication
}

public enum PeerState
{
    Uninitialized,
    PeerCreated,
    Connecting,
    Connected,
    Queued,
    Authenticated,
    JoinedLobby,
    DisconnectingFromMasterserver,
    ConnectingToGameserver,
    ConnectedToGameserver,
    Joining,
    Joined,
    Leave,
    Leaving,
    Left,
    DisconnectingFromGameserver,
    ConnectingToMasterserver,
    ConnectedComingFromGameserver,
    QueuedComingFromGameserver,
    Disconnect,
    Disconnecting,
    Disconnected,
    ConnectedToMaster
}

public enum JoinType
{
    CreateGame,
    JoinGame,
    JoinRandomGame
}


// Photon properties, internally set by PhotonNetwork (PhotonNetwork builtin properties)

/// <summary>
/// This enum makes up the set of MonoMessages sent by Photon Unity Networking.
/// Implement any of these constant names as method and it will be called
/// in the respective situation.
/// </summary>
/// <example>
/// Implement: 
/// public void OnLeftRoom() { //some work }
/// </example>
public enum PhotonNetworkingMessage
{
    /// <summary>
    /// Called as soon as PhotonNetwork succeeds to connect to the photon server. (This is not called for transitions from the masterserver to game servers, which is hidden for PUN users)
    /// Example: void OnConnectedToPhoton(){ ... }
    /// </summary>
    OnConnectedToPhoton,
    /// <summary>
    /// Called once the local user left a room.
    /// Example: void OnLeftRoom(){ ... }
    /// </summary>
    OnLeftRoom,
    /// <summary>
    /// Called -after- switching to a new MasterClient because the previous MC left the room. The last MC will already be removed at this points.
    /// Example: void OnMasterClientSwitched(PhotonPlayer newMasterClient){ ... }
    /// </summary>
    OnMasterClientSwitched,
    /// <summary>
    /// Called if a CreateRoom() call failed. Most likely because the room name is already in use.
    /// Example: void OnPhotonCreateRoomFailed(){ ... }
    /// </summary>
    OnPhotonCreateRoomFailed,
    /// <summary>
    /// Called if a JoinRoom() call failed. Most likely because the room does not exist or the room is full.
    /// Example: void OnPhotonJoinRoomFailed(){ ... }
    /// </summary>
    OnPhotonJoinRoomFailed,
    /// <summary>
    /// Called after a CreateRoom() succeeded creating a room. Note that this implies the local client is the MasterClient. OnJoinedRoom is always called after OnCreatedRoom.
    /// Example: void OnCreatedRoom(){ ... }
    /// </summary>
    OnCreatedRoom,
    /// <summary>
    /// Called after connecting to the master server. While in the lobby, the roomlist is automatically updated.
    /// Example: void OnJoinedLobby(){ ... }
    /// </summary>
    OnJoinedLobby,
    /// <summary>
    /// Called after leaving the lobby
    /// Example: void OnLeftLobby(){ ... }
    /// </summary>
    OnLeftLobby,
    /// <summary>
    /// Called after disconnecting from the Photon server
    /// Example: void OnDisconnectedFromPhoton(){ ... }
    /// </summary>
    OnDisconnectedFromPhoton,
    /// <summary>
    /// Called after a connect call to the Photon server failed
    /// Example: void OnFailedToConnectToPhoton(ExitGames.Client.Photon.StatusCode code){ ... }
    /// </summary>
    OnFailedToConnectToPhoton,
    /// <summary>
    /// Called after receiving the room list for the first time. Only possible in the Lobby state.
    /// Example: void OnReceivedRoomList(){ ... }
    /// </summary>
    OnReceivedRoomList,
    /// <summary>
    /// Called after receiving a room list update. Only possible in the Lobby state.
    /// Example: void OnReceivedRoomListUpdate(){ ... }
    /// </summary>
    OnReceivedRoomListUpdate,
    /// <summary>
    /// Called after joining a room. Called on all clients (including the Master Client)
    /// Example: void OnJoinedRoom(){ ... }
    /// </summary>
    OnJoinedRoom,
    /// <summary>
    /// Called after a remote player connected to the room. This PhotonPlayer is already added to the playerlist at this time.
    /// Example: void OnPhotonPlayerConnected(PhotonPlayer newPlayer){ ... }
    /// </summary>
    OnPhotonPlayerConnected,
    /// <summary>
    /// Called after a remote player disconnected from the room. This PhotonPlayer is already removed from the playerlist at this time.
    /// Example: void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer){ ... }
    /// </summary>
    OnPhotonPlayerDisconnected,
    /// <summary>
    /// Called after a JoinRandom() call failed. Most likely all rooms are full or no rooms are available.
    /// Example: void OnPhotonRandomJoinFailed(){ ... }
    /// </summary>
    OnPhotonRandomJoinFailed,
    /// <summary>
    /// Called after the connection to the master is established and authenticated but only when PhotonNetwork.AutoJoinLobby is false.
    /// If AutoJoinLobby is false, the list of available rooms won't become available but you could join (random or by name) and create rooms anyways.
    /// Example: void OnConnectedToMaster(){ ... }
    /// </summary>
    OnConnectedToMaster,
    /// <summary>
    /// Called every network 'update' if this MonoBehaviour is being observed by a PhotonView.
    /// Example: void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){ ... }
    /// </summary>
    OnPhotonSerializeView,
    /// <summary>
    /// Called on all scripts on a GameObject(and it's children) that have been spawned using PhotonNetwork.Instantiate
    /// Example: void OnPhotonInstantiate(PhotonMessageInfo info){ ... }
    /// </summary>
    OnPhotonInstantiate
}
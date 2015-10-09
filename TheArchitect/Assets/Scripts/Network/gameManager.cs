using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hashtable = ExitGames.Client.Photon.Hashtable; //Replace default Hashtables with Photon hashtables

public class GameManager : PhotonHelper {
	
	public static int m_view = -1;
	public static bool isAlive = false;
	[HideInInspector]
	public GameObject OurPlayer;
	/// <summary>
	/// Player Prefabs ArchiTect
	/// </summary>
	public GameObject Architect;
	/// <summary>
	/// Player Prefabs for Players
	/// </summary>
	public GameObject Player;
	/// <summary>
	/// List with all Players in Current Room
	/// </summary>
	public List<PhotonPlayer> connectedPlayerList = new List<PhotonPlayer>();
	/// <summary>
	/// Camera Preview
	/// </summary>
	public Camera m_RoomCamera;
	/// <summary>
	/// Spawn Points Players
	/// </summary>
	public Transform[] PlayerSpawnPoints;
	/// <summary>
	/// Spawn Points for Architect
	/// </summary>
	public Transform[] ArchitectSpawnPoint;
	[Space(5)]

	public Canvas mOverlayCanvas = null;
	
	/// <summary>
	/// 
	/// </summary>
	protected override void Awake()
	{
		base.Awake();
		PhotonNetwork.isMessageQueueRunning = true;
	}
	
	/// <summary>
	/// Spawn Player Function
	/// </summary>
	/// <param name="t_team"></param>
	public void SpawnPlayer(Team t_team)
	{
		if (OurPlayer != null)
		{
			PhotonNetwork.Destroy(OurPlayer);
		}
		
		
		Hashtable PlayerTeam = new Hashtable();
		PlayerTeam.Add("Team", t_team.ToString());
		PhotonNetwork.player.SetCustomProperties(PlayerTeam);
		
		
		if (t_team == Team.Architect)
		{
			OurPlayer = PhotonNetwork.Instantiate(Architect.name, GetSpawn(ArchitectSpawnPoint), Quaternion.identity, 0);
		}
		else if (t_team == Team.BasicPlayer)
		{
			OurPlayer = PhotonNetwork.Instantiate(Player.name, GetSpawn(PlayerSpawnPoints), Quaternion.identity, 0);
		}
		else
		{

		}

		m_RoomCamera.gameObject.SetActive(false);
		if (mOverlayCanvas != null)
		{
			Camera cam = GameObject.FindWithTag("WeaponCam").GetComponent<Camera>();
			mOverlayCanvas.worldCamera = cam;
		}
	}
	/// <summary>
	/// If Player exist, them destroy
	/// </summary>
	public void DestroyPlayer()
	{
		if (OurPlayer != null)
		{
			PhotonNetwork.Destroy(OurPlayer);
		}
	}
	/// <summary>
	/// 
	/// </summary>
	/// <param name="list"></param>
	/// <returns></returns>
	public Vector3 GetSpawn(Transform[] list)
	{
		int random = Random.Range(0, list.Length);
		Vector3 s = Random.insideUnitSphere * list[random].GetComponent<SpawnPoint>().SpawnSpace;
		Vector3 pos = list[random].position + new Vector3(s.x, 0, s.z);
		return pos;
	}
	
	//This is called only when the current gameobject has been Instantiated via PhotonNetwork.Instantiate
	public override void OnPhotonInstantiate(PhotonMessageInfo info)
	{
		base.OnPhotonInstantiate(info);
		Debug.Log("New object instantiated by " + info.sender);
	}
	
	public override void OnMasterClientSwitched(PhotonPlayer newMaster)
	{
		base.OnMasterClientSwitched(newMaster);
		Debug.Log("The old masterclient left, we have a new masterclient: " + newMaster);
	}
	
	public override void OnDisconnectedFromPhoton()
	{
		base.OnDisconnectedFromPhoton();
		Debug.Log("Clean up a bit after server quit");
		
		/* 
        * To reset the scene we'll just reload it:
        */
		PhotonNetwork.isMessageQueueRunning = false;
		Application.LoadLevel("MainMenu");
	}
	//PLAYER EVENTS
	public override void OnPhotonPlayerConnected(PhotonPlayer player)
	{
		base.OnPhotonPlayerConnected(player);
		Debug.Log("Player connected: " + player);
	}
	
	public override void OnReceivedRoomListUpdate()
	{
		base.OnReceivedRoomListUpdate();
	}
	public override void OnPhotonPlayerDisconnected(PhotonPlayer player)
	{
		base.OnPhotonPlayerDisconnected(player);
		Debug.Log("Player disconnected: " + player);
		
	}
	public override void OnFailedToConnectToPhoton(DisconnectCause Cause)
	{
		base.OnFailedToConnectToPhoton(Cause);
		Debug.Log("OnFailedToConnectToPhoton "+Cause);
		
		// back to main menu or fisrt scene       
		Application.LoadLevel(0);
	}
	
}		
using UnityEngine;
using System.Collections;

public class playerInGame : Photon.MonoBehaviour {

	public GameObject playerPrefab;
    public GameObject archPrefab;
    public GameObject spawnPoint;
	public GameObject archSpawnPoint;
	public GameObject introCamera;
	public GameObject[] startUI;
	
	public void Awake()
	{
		// in case we started this demo with the wrong scene being active, simply load the menu scene
		if (!PhotonNetwork.connected)
		{
			Application.LoadLevel(launchGame.SceneNameMenu);
			return;
		}
        PhotonNetwork.sendRate = 30;
        PhotonNetwork.sendRateOnSerialize = 30;
		// we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate

	}

	public void AddPlayer()
	{
		RemoveLevelComponents();
		PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.transform.position, Quaternion.identity, 0);
	}

	public void AddArchitect()
	{
		RemoveLevelComponents();
		PhotonNetwork.Instantiate(archPrefab.name, archSpawnPoint.transform.position, Quaternion.identity, 0);
	}

	public void AbandonShip()
	{
		PhotonNetwork.LeaveRoom();  // we will load the menu level when we successfully left the room
	}

	public void RemoveLevelComponents()
	{
		introCamera.SetActive(false);
	}
	
	public void OnMasterClientSwitched(PhotonPlayer player)
	{
		Debug.Log("OnMasterClientSwitched: " + player);
		
		string message;
		InRoomChat chatComponent = GetComponent<InRoomChat>();  // if we find a InRoomChat component, we print out a short message
		
		if (chatComponent != null)
		{
			// to check if this client is the new master...
			if (player.isLocal)
			{
				message = "You are Master Client now.";
			}
			else
			{
				message = player.name + " is Master Client now.";
			}
			
			
			chatComponent.AddLine(message); // the Chat method is a RPC. as we don't want to send an RPC and neither create a PhotonMessageInfo, lets call AddLine()
		}
	}
	
	public void OnLeftRoom()
	{
		Debug.Log("OnLeftRoom (local)");
		
		// back to main menu        
		Application.LoadLevel(launchGame.SceneNameMenu);
	}
	
	public void OnDisconnectedFromPhoton()
	{
		Debug.Log("OnDisconnectedFromPhoton");
		
		// back to main menu        
		Application.LoadLevel(launchGame.SceneNameMenu);
	}
	
	public void OnPhotonInstantiate(PhotonMessageInfo info)
	{
		Debug.Log("OnPhotonInstantiate " + info.sender);    // you could use this info to store this or react
	}
	
	public void OnPhotonPlayerConnected(PhotonPlayer player)
	{
		Debug.Log("OnPhotonPlayerConnected: " + player);
	}
	
	public void OnPhotonPlayerDisconnected(PhotonPlayer player)
	{
		Debug.Log("OnPlayerDisconneced: " + player);
	}
	
	public void OnFailedToConnectToPhoton()
	{
		Debug.Log("OnFailedToConnectToPhoton");
		
		// back to main menu        
		Application.LoadLevel(launchGame.SceneNameMenu);
	}
}

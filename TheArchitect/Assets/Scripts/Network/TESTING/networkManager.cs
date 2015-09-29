using UnityEngine;
using System.Collections;

public class networkManager : MonoBehaviour {
	const string VERSION = "v0.1";
	public string roomName = "The Architect";
	public GameObject playerPrefab;
	public GameObject architectPrefab;
	public Transform spawnPoint;
	public Transform spawnPoint2;

	void Start () {
		PhotonNetwork.ConnectUsingSettings(VERSION);
		Debug.Log ("Started");
	}

	void OnJoinedLobby() {
		RoomOptions roomOptions  = new RoomOptions() {isVisible = true, maxPlayers = 4};
		PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
		Debug.Log ("Joined Lobby");
	}

	void OnJoinedRoom() {
		Debug.Log ("JoinedRoom");
		if (PhotonNetwork.playerList.Length <= 1)
		{
			PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation, 0);
//			playerPrefab.GetComponentInChildren<SkinnedMeshRenderer>().materials[0].color = Color.red;
			spawnPoint.position += new Vector3(10,0,0);
		}
		else if (PhotonNetwork.playerList.Length <= 2)
		{
			PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation, 1);
//			playerPrefab.GetComponentInChildren<SkinnedMeshRenderer>().materials[0].color = Color.yellow;
			spawnPoint.position += new Vector3(10,0,0);
		}
		else if (PhotonNetwork.playerList.Length <= 3)
		{
			PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation, 2);
//			playerPrefab.GetComponentInChildren<SkinnedMeshRenderer>().materials[0].color = Color.green;
			spawnPoint.position += new Vector3(10,0,0);
		}
		else {
			PhotonNetwork.Instantiate (architectPrefab.name, spawnPoint2.position, spawnPoint2.rotation, 3);
		}
	}
}
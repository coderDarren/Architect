using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;

public class PhotonHelper : Photon.PunBehaviour {
	
	protected string myTeam = string.Empty;

	protected virtual void Awake()
	{
		if (!PhotonNetwork.connected)
			return;
		
		myTeam = (string)PhotonNetwork.player.customProperties[PropertiesKeys.TeamKey];
	}
	/// <summary>
	/// Find a player gameobject by the viewID 
	/// </summary>
	/// <param name="view"></param>
	/// <returns></returns>
	public GameObject FindPlayerRoot(int view)
	{
		PhotonView m_view = PhotonView.Find(view);
		
		if (m_view != null)
		{
			return m_view.gameObject;
		}
		else
		{
			return null;
		}
	}
	/// <summary>
	///  get a photonView by the viewID
	/// </summary>
	/// <param name="view"></param>
	/// <returns></returns>
	public PhotonView FindPlayerView(int view)
	{
		PhotonView m_view = PhotonView.Find(view);
		
		if (m_view != null)
		{
			return m_view;
		}
		else
		{
			return null;
		}
	}
	/// <summary>
	/// 
	/// </summary>
	/// <param name="go"></param>
	/// <returns></returns>
	public PhotonView GetPhotonView(GameObject go)
	{
		PhotonView view = go.GetComponent<PhotonView>();
		if (view == null)
		{
			view = go.GetComponentInChildren<PhotonView>();
		}
		return view;
	}
	/// <summary>
	/// 
	/// </summary>
	public Transform Root
	{
		get
		{
			return transform.root;
		}
	}
	/// <summary>
	/// 
	/// </summary>
	public Transform Parent
	{
		get
		{
			return transform.parent;
		}
	}
	
	/// <summary>
	/// True if the PhotonView is "mine" and can be controlled by this client.
	/// </summary>
	/// <remarks>
	/// PUN has an ownership concept that defines who can control and destroy each PhotonView.
	/// True in case the owner matches the local PhotonPlayer.
	/// True if this is a scene photonview on the Master client.
	/// </remarks>
	public bool isMine
	{
		get
		{
			return (this.photonView.ownerId == PhotonNetwork.player.ID) || (!this.photonView.isOwnerActive && PhotonNetwork.isMasterClient);
		}
	}
	/// <summary>
	/// Get Photon.connect
	/// </summary>
	public bool isConnected
	{
		get
		{
			return PhotonNetwork.connected;
		}
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <param name="p"></param>
	/// <returns></returns>
	public GameObject FindPhotonPlayer(PhotonPlayer p)
	{
		GameObject player = GameObject.Find(p.name);
		if (player == null)
		{
			return null;
		}
		return player;
	}
	/// <summary>
	/// Get the team of players
	/// </summary>
	/// <param name="p"></param>
	/// <returns></returns>
	public string GetTeam(PhotonPlayer p)
	{
		if (p == null || !isConnected)
			return null;
		
		string t = (string)p.customProperties[PropertiesKeys.TeamKey];
		return t;
	}
	
	public string LocalName
	{
		get
		{
			if (PhotonNetwork.player != null && isConnected)
			{
				string n = PhotonNetwork.player.name;
				return n;
			}
			else
			{
				return "None";
			}
		}
	}
	
	/// <summary>
	/// Get All Player in Room
	/// </summary>
	public List<PhotonPlayer> AllPlayerList
	{
		get
		{
			List<PhotonPlayer> p = new List<PhotonPlayer>();
			
			foreach (PhotonPlayer pp in PhotonNetwork.playerList)
			{
				p.Add(pp);
			}
			return p;
		}
	}
	/// <summary>
	/// Get All Player in Room of a specific team
	/// </summary>
	/// <param name="team"></param>
	/// <returns></returns>
	public List<PhotonPlayer> GetPlayersInTeam(string team)
	{
		List<PhotonPlayer> p = new List<PhotonPlayer>();
		
		foreach (PhotonPlayer pp in PhotonNetwork.playerList)
		{
			if ((string)pp.customProperties[PropertiesKeys.TeamKey] == team)
			{
				p.Add(pp);
			}
		}
		return p;
	}
}
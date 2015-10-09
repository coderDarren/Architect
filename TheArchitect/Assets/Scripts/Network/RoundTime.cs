using UnityEngine;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable; //Replace default Hashtables with Photon hashtables
using UnityEngine.UI;

public class RoundTime : MonoBehaviour {
	
	public GUISkin Style;
	/// <summary>
	/// expected duration in round (automatically obtained)
	/// </summary>
	public int RoundDuration;
	public GameManager m_Manager = null;
	[HideInInspector]
	public float CurrentTime;
	[System.Serializable]
	public class UI_
	{
		public Text TimeText;
	}
	public UI_ UI;
	//private
	private const string StartTimeKey = "RoomTime";       // the name of our "start time" custom property.
	private float m_Reference;
	private int m_countdown = 10;
	private bool isFinish = false;
	private SettingProperties m_propiertis;
	private RoomMenu RoomMenu;
	
	void Awake()
	{
		if (!PhotonNetwork.connected)
		{
			Application.LoadLevel(0);
			return;
		}
		
		GetTime();
		m_propiertis = this.GetComponent<SettingProperties>();
		RoomMenu = this.GetComponent<RoomMenu>();
	}
	/// <summary>
	/// get the current time and verify if it is correct
	/// </summary>
	void GetTime()
	{
		RoundDuration = (int)PhotonNetwork.room.customProperties[PropertiesKeys.TimeRoomKey];
		if (PhotonNetwork.isMasterClient)
		{
			m_Reference = (float)PhotonNetwork.time;
			
			Hashtable startTimeProp = new Hashtable();  // only use ExitGames.Client.Photon.Hashtable for Photon
			startTimeProp.Add(StartTimeKey, m_Reference);
			PhotonNetwork.room.SetCustomProperties(startTimeProp);
		}
		else
		{
			m_Reference = (float)PhotonNetwork.room.customProperties[StartTimeKey];
		}
	}
	
	void FixedUpdate()
	{
		float t_time = RoundDuration - ((float)PhotonNetwork.time - m_Reference);
		if (t_time > 0)
		{
			CurrentTime = t_time;
		}
		else if (t_time <= 0.001 && GetTimeServed == true)//Round Finished
		{
			CurrentTime = 0;
			
			EventHandler.OnRoundEndEvent();
			if (!isFinish)
			{
				isFinish = true;
				RoomMenu.isFinish = true;
				InvokeRepeating("countdown", 1, 1);
			}
		}
		else//even if I do not photonnetwork.time then obtained to regain time
		{
			Refresh();
		}
	}
	
	void OnGUI()
	{
		GUI.skin = Style;
		//Display Time Round
		int normalSecons = 60;
		float remainingTime = Mathf.CeilToInt(CurrentTime);
		int m_Seconds = Mathf.FloorToInt(remainingTime % normalSecons);
		int m_Minutes = Mathf.FloorToInt((remainingTime / normalSecons) % normalSecons);
		string t_time = UtilityHelper.GetTimeFormat(m_Minutes, m_Seconds);
		
		//OnGUI version
		/*GUILayout.BeginArea(new Rect(Screen.width / 2 - 100, 0, 200, 35));
        GUILayout.Box("Remaing \n"+t_time,TimeStyle,GUILayout.Height(35));
        GUILayout.EndArea();*/
		
		if (UI.TimeText != null)
		{
			UI.TimeText.text = "<size=9>Remaining</size> \n" + t_time;
		}
	}
	
	public GUIStyle TimeStyle
	{
		get
		{
			if (Style != null)
			{
				return Style.customStyles[1];
			}
			else
			{
				return null;
			}
		}
	}
	/// <summary>
	/// with this fixed the problem of the time lag in the Photon
	/// </summary>
	void Refresh()
	{
		if (PhotonNetwork.isMasterClient)
		{
			m_Reference = (float)PhotonNetwork.time;
			
			Hashtable startTimeProp = new Hashtable();  // only use ExitGames.Client.Photon.Hashtable for Photon
			startTimeProp.Add(StartTimeKey, m_Reference);
			PhotonNetwork.room.SetCustomProperties(startTimeProp);
		}
		else
		{
			m_Reference = (float)PhotonNetwork.room.customProperties[StartTimeKey];
		}
	}
	
	void countdown()
	{
		m_countdown--;
		if (m_countdown <= 0)
		{
			CancelInvoke("countdown");
			m_countdown = 10;
		}
	}
	
	bool GetTimeServed
	{
		get
		{
			bool m_bool = false ;
			if (Time.timeSinceLevelLoad > 7)
			{
				m_bool = true;
			}
			return m_bool;
		}
	}
	
}
using UnityEngine;
using System.Collections;

public class SettingProperties : MonoBehaviour {
		
		public GUIStyle Style;
		///OnGUI settings
		/*public Texture2D DeltaBox;
    public Texture2D ReconBox;*/
		public Texture2D FFABox;
		public GameObject CTFObjects;
		//Private
		private int Team_1_Score;
		private int Team_2_Score;
		private float UpdatePing = 5;//Update Player Ping each 5s
		private bool isFinish = false;
		private string FinalRoundText = string.Empty;
		private RoomMenu RMenu;
		private RoundTime RTime;
		/// <summary>
		/// 
		/// </summary>
		void Awake()
		{
			
			RMenu = base.GetComponent<RoomMenu>();
			RTime = base.GetComponent<RoundTime>();
//			SettingPropiertis();
		}
		/// <summary>
		/// 
		/// </summary>
//		public void SettingPropiertis()
//		{
//			//Initialize new properties where the information will stay Room
//			if (PhotonNetwork.isMasterClient)
//			{
//				Hashtable setTeamScore = new Hashtable();
//				setTeamScore.Add(PropertiesKeys.Team1Score, 0);
//				PhotonNetwork.room.SetCustomProperties(setTeamScore);
//				
//				Hashtable setTeam2Score = new Hashtable();
//				setTeam2Score.Add(PropertiesKeys.Team2Score, 0);
//				PhotonNetwork.room.SetCustomProperties(setTeam2Score);
//			}
//			//Initialize new properties where the information will stay Players
//			Hashtable PlayerTeam = new Hashtable();
//			PlayerTeam.Add(PropertiesKeys.TeamKey, Team.All.ToString());
//			PhotonNetwork.player.SetCustomProperties(PlayerTeam);
//			
//			Hashtable PlayerKills = new Hashtable();
//			PlayerKills.Add(PropertiesKeys.KillsKey, 0);
//			PhotonNetwork.player.SetCustomProperties(PlayerKills);
//			
//			Hashtable PlayerDeaths = new Hashtable();
//			PlayerDeaths.Add(PropertiesKeys.DeathsKey, 0);
//			PhotonNetwork.player.SetCustomProperties(PlayerDeaths);
//			
//			Hashtable PlayerScore = new Hashtable();
//			PlayerScore.Add(PropertiesKeys.ScoreKey, 0);
//			PhotonNetwork.player.SetCustomProperties(PlayerScore);
//		}
		/// <summary>
		/// 
		/// </summary>
		void OnEnable()
		{
			InvokeRepeating("GetPing", 1, UpdatePing);
			EventHandler.OnRoundEnd += this.OnRoundEnd;
		}
		/// <summary>
		/// 
		/// </summary>
		void OnDisable()
		{
			CancelInvoke("GetPing");
			EventHandler.OnRoundEnd -= this.OnRoundEnd;
		}
		/// <summary>
		/// 
		/// </summary>
		void Update()
		{

		}
		/// <summary>
		/// 
		/// </summary>
		void OnGUI()
		{
			//OnGUI version of RoomScore
			/*if (m_GameMode == GameMode.TDM || m_GameMode == GameMode.CTF)
        {
            //Room Score for TDM
            if (PhotonNetwork.room != null)
            {
                Team_1_Score = (int)PhotonNetwork.room.customProperties[PropertiesKeys.Team1Score];
                Team_2_Score = (int)PhotonNetwork.room.customProperties[PropertiesKeys.Team2Score];
            }
            GUILayout.BeginArea(new Rect(Screen.width / 2 - 100, 35, 202, 50));
            GUILayout.BeginHorizontal();
            GUILayout.Box("<size=30>"+bl_UtilityHelper.GetThreefoldChar(Team_1_Score)+"</size>\n<size=12>Delta</size>", Team_1_Style, GUILayout.Width(100), GUILayout.Height(50));
            GUILayout.Box("<size=30>" + bl_UtilityHelper.GetThreefoldChar(Team_2_Score) + "</size>\n<size=12>Recon</size>", Team_2_Style, GUILayout.Width(100), GUILayout.Height(50));
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }

        if (m_GameMode == GameMode.FFA)
        {
            string t_PlayerStart = m_Menu.PlayerStar;
            GUI.Box(new Rect((Screen.width / 2 - 100), 35, 200, 50),"Player Star:\n<size=14><color=orange>"+t_PlayerStart+"</color></size>",FFA_Style);
            
        }*/
		}
		/// <summary>
		/// 
		/// </summary>
		void OnRoundEnd()
		{
			isFinish = true;
			StartCoroutine(DisableUI());
		}
		//OnGUI settings active if needed
		/*public GUIStyle Team_1_Style
    {
        get
        {
            GUIStyle t_style = new GUIStyle();
            t_style.font = Style.font;
            t_style.normal.textColor = Style.normal.textColor;
            t_style.fontSize = Style.fontSize;
            t_style.alignment = Style.alignment;
            t_style.normal.background = DeltaBox;

            return t_style;
        }
    }
    public GUIStyle Team_2_Style
    {
        get
        {
            GUIStyle t_style = new GUIStyle();
            t_style.font = Style.font;
            t_style.normal.textColor = Style.normal.textColor;
            t_style.fontSize = Style.fontSize;
            t_style.alignment = Style.alignment;
            t_style.normal.background = ReconBox;

            return t_style;
        }
    }*/
		public GUIStyle FFA_Style
		{
			get
			{
				GUIStyle t_style = new GUIStyle();
				t_style.font = Style.font;
				t_style.normal.textColor = Style.normal.textColor;
				t_style.fontSize = Style.fontSize;
				t_style.alignment = Style.alignment;
				t_style.normal.background = FFABox;
				t_style.richText = true;
				return t_style;
			}
		}
		public RoomMenu m_Menu
		{
			get
			{
				if (GetComponent<RoomMenu>() != null)
				{
					return GetComponent<RoomMenu>();
				}
				else
				{
					return null;
				}
			}
		}
		IEnumerator DisableUI()
		{
			yield return new WaitForSeconds(10);
			isFinish = false;
		}
	}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class RoomMenu : PhotonHelper
{
	[HideInInspector]
	public bool isPlaying = false;
	[HideInInspector]
	public float m_sensitive = 2.0f;
	[HideInInspector]
	public bool ShowWarningPing = false;
	[HideInInspector]
	public List<PhotonPlayer> m_playerlist = new List<PhotonPlayer>();
	[HideInInspector]
	public string PlayerStar = "";
	[HideInInspector]
	public bool showMenu = true;
	[HideInInspector]
	public bool isFinish = false;
	
	public bool AutoTeamSelection = false;
	
	public GameManager GM;
	/// <summary>
	/// When ping is > at this, them show a message
	/// </summary>
	public int MaxPing = 500;
	/// <summary>
	/// When ping is too high show this message
	/// </summary>
	public string MsnMaxPing = "Your <color=yellow>ping is too high</color> \n <size=12>check your local coneccion.</size>";
	/// <summary>
	/// Rotate room camera?
	/// </summary>
	public bool RotateCamera = true;
	/// <summary>
	/// Rotation Camera Speed
	/// </summary>
	public float RotSpeed = 5;
	[Space(5)]
	[Header("GUI")]
	public GUISkin SKin;
//	public Texture2D WarningPing;
//	public Texture2D FadeBlackTexture;
//	public Image VignetteImage = null;
//	public GameObject ButtonsClassPlay = null;
//	public Canvas m_CanvasRoot = null;
//	[Range(0.0f,1.0f)]
//	public float VigAlpha = 0.8f;
//	public bool Use_Vignette = true;
//	public static float m_alphafade = 3;
	//Private
	private float m_volume = 1.0f;
	private float m_currentQuality = 2;
	private string[] m_stropicOptions = new string[] { "Disable", "Enable", "Force Enable" };
	private int m_stropic = 0;
	private int m_window;
	private Vector2 scroll_1;
	private Vector2 scroll_2;
	private bool CanSpawn = false;
	private bool AlredyAuto = false;
	private bool m_showScoreBoard = false;
	private bool m_showbuttons = false;
	
	protected override void Awake()
	{
		base.Awake();
		if (!isConnected)
			return;
		
		m_window = 1;
		showMenu = true;
		if (AutoTeamSelection)
		{
			StartCoroutine(CanSpawnIE());
		}
		GetPrefabs();
		
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			showMenu = true;
			m_showScoreBoard = false;
		}
		if (Input.GetKeyDown(KeyCode.N) && !showMenu)
		{
			m_showScoreBoard = true;
		}
		else if(Input.GetKeyUp(KeyCode.N) || showMenu)
		{
			m_showScoreBoard = false;
		}
		if (RotateCamera)
		{
			this.transform.Rotate(Vector3.up * Time.deltaTime * RotSpeed);
		}
		
		if (AutoTeamSelection && !AlredyAuto)
		{
//			AutoTeam();
		}
	}
	
	void MainMenu()
	{

	}
	
	void SettingMenu()
	{
		GUILayout.BeginArea(new Rect(Screen.width / 2 - 250, Screen.height / 2 - 150, 500, 350), "", "window");
		GUILayout.Space(10);
		GUILayout.Box("Settings");
		GUILayout.Label("Quality Level");
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("<<"))
		{
			if (m_currentQuality < QualitySettings.names.Length)
			{
				m_currentQuality--;
				if (m_currentQuality < 0)
				{
					m_currentQuality = QualitySettings.names.Length - 1;
					
				}
			}
		}
		GUILayout.Box(QualitySettings.names[(int)m_currentQuality]);
		if (GUILayout.Button(">>"))
		{
			if (m_currentQuality < QualitySettings.names.Length)
			{
				m_currentQuality++;
				if (m_currentQuality > (QualitySettings.names.Length - 1))
				{
					m_currentQuality = 0;
				}
			}
		}
		GUILayout.EndHorizontal();
		GUILayout.Label("Anisotropic Filtering");
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("<<"))
		{
			if (m_stropic < m_stropicOptions.Length)
			{
				m_stropic--;
				if (m_stropic < 0)
				{
					m_stropic = m_stropicOptions.Length - 1;
					
				}
			}
		}
		GUILayout.Box(m_stropicOptions[m_stropic]);
		if (GUILayout.Button(">>"))
		{
			if (m_stropic < m_stropicOptions.Length)
			{
				m_stropic++;
				if (m_stropic > (m_stropicOptions.Length - 1))
				{
					m_stropic = 0;
				}
			}
		}
		GUILayout.EndHorizontal();
		GUILayout.Label("Sound Volume");
		GUILayout.BeginHorizontal();
		m_volume = GUILayout.HorizontalSlider(m_volume, 0.0f, 1.0f);
		GUILayout.Label((m_volume * 100).ToString("000"), GUILayout.Width(30));
		GUILayout.EndHorizontal();
		GUILayout.Label("Sensitivity");
		GUILayout.BeginHorizontal();
		m_sensitive = GUILayout.HorizontalSlider(m_sensitive, 0.0f, 100.0f);
		GUILayout.Label(m_sensitive.ToString("000"), GUILayout.Width(30));
		GUILayout.EndHorizontal();
		if (GUILayout.Button("Apply"))
		{
			ApplySave();
		}
		GUILayout.EndArea();
	}
	
	void ApplySave()
	{
		QualitySettings.SetQualityLevel((int)m_currentQuality);
		AudioListener.volume = m_volume;
		if (m_stropic == 0)
		{
			QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
		}
		else if (m_stropic == 1)
		{
			QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
		}
		else
		{
			QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
		}
		//Save
		PlayerPrefs.SetFloat("volumen", m_volume);
		PlayerPrefs.SetFloat("sensitive", m_sensitive);
		PlayerPrefs.SetInt("quality", (int)m_currentQuality);
		PlayerPrefs.SetInt("anisotropic", m_stropic);
		Debug.Log("Save Done!");
	}
	
	void GetPrefabs()
	{
		if (PlayerPrefs.HasKey("volumen"))
		{
			m_volume = PlayerPrefs.GetFloat("volumen");
			AudioListener.volume = m_volume;
		}
		if (PlayerPrefs.HasKey("sensitive"))
		{
			m_sensitive = PlayerPrefs.GetFloat("sensitive");
		}
		if (PlayerPrefs.HasKey("quality"))
		{
			m_currentQuality = PlayerPrefs.GetInt("quality");
		}
		if (PlayerPrefs.HasKey("anisotropic"))
		{
			m_stropic = PlayerPrefs.GetInt("anisotropic");
		}
	}

	/// <summary>
	/// Get All Player in Room List
	/// </summary>
	public List<PhotonPlayer> GetPlayerList
	{
		get
		{
			List<PhotonPlayer> list = new List<PhotonPlayer>();
			foreach (PhotonPlayer players in PhotonNetwork.playerList)
			{
				list.Add(players);
			}
			return list;
		}
	}
	
	IEnumerator CanSpawnIE()
	{
		yield return new WaitForSeconds(3);
		CanSpawn = true;
	}

	public override void OnLeftRoom()
	{
		base.OnLeftRoom();
		Debug.Log("OnLeftRoom (local)");
		
		// back to main menu        
		Application.LoadLevel(0);
	}
}
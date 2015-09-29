using UnityEngine;
using System.Collections;

public class testArchPowers : MonoBehaviour {
		
	[System.Serializable]
	public class FireBallPower
	{
		public float recharge = 4f;
		public float rechargeTimer = 0;
		public float fireSpeed = 10;
		public string INPUT_AXIS = "Fire1";
		[HideInInspector]
		public float input = 0;
		public GameObject FireBall;
		public string ACTIVATE_INPUT = "Power 1";
		public GameObject PowerSelectedArrow;
	}
	
	[System.Serializable]
	public class TileDrop
	{
		public float recharge = 4f;
		public float rechargeTimer = 0;
		public string INPUT_AXIS = "Fire1";
		public Color tileHighlight = Color.white;
		[HideInInspector]
		public float input = 0;
		[HideInInspector]
		public GameObject selectedTile;
		public string ACTIVATE_INPUT = "Power 2";
		public GameObject PowerSelectedArrow;
	}
	
	[System.Serializable]
	public class BombTrap
	{
		public float recharge = 4f;
		public float rechargeTimer = 0;
		public float fireSpeed = 10;
		public string INPUT_AXIS = "Fire1";
		[HideInInspector]
		public float input = 0;
		public GameObject Bomb;
		public string ACTIVATE_INPUT = "Power 3";
		public GameObject PowerSelectedArrow;
	}
	
	public enum ActivePower { FireBall, TileDrop, BombTrap };
	public ActivePower activePower;
	public Camera myCamera;
	
	public FireBallPower firePower = new FireBallPower();
	public TileDrop tileDrop = new TileDrop();
	public BombTrap bombTrap = new BombTrap();
	Color initialTileColor;
	
	GameObject ArchitectCanvas;
	
	void Start() 
	{
		activePower = ActivePower.FireBall;
		firePower.PowerSelectedArrow.SetActive(true);
		tileDrop.PowerSelectedArrow.SetActive(false);
		bombTrap.PowerSelectedArrow.SetActive(false);
		ArchitectCanvas = transform.GetChild(0).gameObject;
		ArchitectCanvas.transform.parent = null;
	}
	
	void GetInput()
	{
		firePower.input = Input.GetAxis(firePower.INPUT_AXIS);
		tileDrop.input = Input.GetAxis(tileDrop.INPUT_AXIS);
		bombTrap.input = Input.GetAxis(bombTrap.INPUT_AXIS);
		if (Input.GetAxis(firePower.ACTIVATE_INPUT) > 0)
		{
			activePower = ActivePower.FireBall;
			
			if (tileDrop.selectedTile)
				tileDrop.selectedTile.GetComponent<Renderer>().materials[0].color = tileDrop.selectedTile.GetComponent<TileBehavior>().initialColor;
			
			firePower.PowerSelectedArrow.SetActive(true);
			tileDrop.PowerSelectedArrow.SetActive(false);
			bombTrap.PowerSelectedArrow.SetActive(false);
		}
		else if (Input.GetAxis(tileDrop.ACTIVATE_INPUT) > 0)
		{
			activePower = ActivePower.TileDrop;
			firePower.PowerSelectedArrow.SetActive(false);
			tileDrop.PowerSelectedArrow.SetActive(true);
			bombTrap.PowerSelectedArrow.SetActive(false);
		}
		else if (Input.GetAxis(bombTrap.ACTIVATE_INPUT) > 0)
		{
			activePower = ActivePower.BombTrap;
			
			if (tileDrop.selectedTile)
				tileDrop.selectedTile.GetComponent<Renderer>().materials[0].color = tileDrop.selectedTile.GetComponent<TileBehavior>().initialColor;
			
			firePower.PowerSelectedArrow.SetActive(false);
			tileDrop.PowerSelectedArrow.SetActive(false);
			bombTrap.PowerSelectedArrow.SetActive(true);
		}
	}
	
	void UpdateTimers()
	{
		firePower.rechargeTimer += Time.deltaTime;
		tileDrop.rechargeTimer += Time.deltaTime;
		bombTrap.rechargeTimer += Time.deltaTime;
	}
	
	void Update()
	{
		GetInput();
		UpdateTimers();
		
		switch(activePower)
		{
		case ActivePower.FireBall: UpdateFirePower(); break;
		}
	}
	
	void UpdateFirePower()
	{
		if (firePower.rechargeTimer > firePower.recharge) //if recharge time is met
		{
			if (firePower.input > 0) //if we click
			{
				Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit)) //if our click registers an actual object
				{
					if (PhotonNetwork.connectionStateDetailed != PeerState.Joined)
					{
						// only use PhotonNetwork.Instantiate while in a room.
						return;
					}
					
					//					GameObject fireBall = PhotonNetwork.Instantiate(Fireball, Camera.main.transform.position, Quaternion.identity, 0);
					//					GameObject fireBall = PhotonNetwork.InstantiateSceneObject(firePower.FireBall.name, myCamera.transform.position, Quaternion.identity, 0, null);
					//GameObject fireBall = Instantiate(firePower.FireBall.name, myCamera.transform.position, Quaternion.identity,0);
					GameObject fireBall = Instantiate(firePower.FireBall, myCamera.transform.position, Quaternion.identity) as GameObject;
					//                    GameObject fireBall = GameObject.Instantiate(firePower.FireBall, Camera.main.transform.position, Quaternion.identity) as GameObject;
					Fireball ball = fireBall.GetComponent<Fireball>();
					ball.speed = firePower.fireSpeed;
					ball.direction = hit.point - myCamera.transform.position;
					Vector3.Normalize(ball.direction);
					firePower.rechargeTimer = 0;
				}
			}
		}
	}
			
}
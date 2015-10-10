using UnityEngine;
using System.Collections;

public class PlayerHealth : Photon.MonoBehaviour {

	//Health Variables
	public float health = 100.0f;

	//Respawn Variables
	public GameObject reSpawnPoint;

	//Timer Variables
	public float waitTime = 5.0f;
	public bool startTimer;
	public float originalTime;

	//Rag Doll Variables
	public GameObject playerBody;
	public GameObject RagDoll;
	public GameObject playerGun;

	void Start()
	{
		originalTime = waitTime;
		reSpawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
	}

	void OnTriggerEnter(Collider coll)
	{
		if (coll.tag == "Fireball")
		{
			Debug.Log ("Projectile Entered");
			TakeProjectileDamage(coll.gameObject);
		}
		if (coll.tag == "TrapBomb")
		{
			Debug.Log ("Bomb Entered");
			TakeMineDamage(coll.gameObject);
		}
	}

	void TakeProjectileDamage(GameObject fireBall)
	{
		PhotonNetwork.RPC(this.gameObject.GetComponent<PhotonView>(), "SyncDamage", PhotonTargets.All, false, fireBall.GetComponent<Fireball>().fireBallDamage);
//		health -= fireBall.GetComponent<Fireball>().fireBallDamage;
		Debug.Log ("Taking Fireball damage");
	}

	void TakeMineDamage(GameObject bomb)
	{
		PhotonNetwork.RPC(this.gameObject.GetComponent<PhotonView>(), "SyncDamage", PhotonTargets.All, false, bomb.GetComponent<Trap_Bomb>().bombDamage);
//		health -= bomb.GetComponent<Trap_Bomb>().bombDamage;
		Debug.Log ("Taking Bomb damage");
	}

	void Death()
	{
		startTimer = true;
		DisablePlayerBody();
	}

	void Respawn()
	{
		this.transform.position = reSpawnPoint.transform.position;
	}

	void DisablePlayerBody()
	{
        Debug.Log("disabling player");
		playerGun.SetActive(false);
		playerBody.SetActive(false);
	}

	void ReinstatePlayerBody()
	{
		GameObject ragDoll = PhotonNetwork.Instantiate(RagDoll.name, playerBody.transform.position, Quaternion.identity, 0);
		playerBody.SetActive(true);
		playerGun.SetActive(true);
	}

	void Update()
	{
		if(health <= 0)
		{
			if (!startTimer)
			{
				Death ();
			}
		}
		if (startTimer)
		{
			waitTime -= Time.deltaTime;
		}
		if (waitTime <= 0)
		{
			Respawn();
			ReinstatePlayerBody();
			startTimer = false;
			waitTime = originalTime;
			health = 100;
		}
	}

	[PunRPC]
	void SyncDamage(float t_damage)
	{
		this.health -= t_damage;
	}
}

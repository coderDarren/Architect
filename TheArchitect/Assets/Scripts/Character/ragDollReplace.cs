using UnityEngine;
using System.Collections;

public class ragDollReplace : Photon.MonoBehaviour {

	public GameObject playerBody;
	public GameObject RagDoll;
	public GameObject playerGun;
	public float downTime;
	private float originalValue;
	public bool timerRunning = false;

	void Start ()
	{
		float originalValue = downTime;
	}

	void Update()
	{
		if (timerRunning)
		{
			downTime -= Time.deltaTime;
			if (downTime <= 0)
			{
				ReinstatePlayerBody();
				timerRunning = false;
			}
		}
	}

	void OnTriggerEnter (Collider coll)
	{
		if (coll.tag == "Fireball")
		{
			playerGun.SetActive(false);
			playerBody.SetActive(false);
			GameObject ragDoll = PhotonNetwork.Instantiate(RagDoll.name, playerBody.transform.position, Quaternion.identity, 0) as GameObject;
			timerRunning = true;
		}
	}

	void ReinstatePlayerBody()
	{
		RagDoll.GetComponent<RagDollDestroy>().DestroyRagDoll();
		RagDoll = Resources.Load("RagDoll", typeof(GameObject)) as GameObject;
		playerBody.SetActive(true);
		playerGun.SetActive(true);
		downTime = originalValue;
	}
}

using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ragDollReplace : Photon.MonoBehaviour {

	public GameObject playerBody;
	public GameObject RagDoll;
	public GameObject ragDoll;
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
			GameObject ragDoll = GameObject.Instantiate(RagDoll, playerBody.transform.position, Quaternion.identity) as GameObject;
			timerRunning = true;
		}
	}

		public void ReinstatePlayerBody()
	{
		DestroyImmediate(RagDoll);
		RagDoll = Resources.Load("RagDoll", typeof(GameObject)) as GameObject;
		playerBody.SetActive(true);
		playerGun.SetActive(true);
		downTime = originalValue;
	}
}

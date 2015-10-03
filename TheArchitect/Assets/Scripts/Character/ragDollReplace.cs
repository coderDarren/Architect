using UnityEngine;
using System.Collections;

public class ragDollReplace : MonoBehaviour {

	public GameObject playerBody;
	public GameObject ragDoll;
	public GameObject playerGun;
	public float downTime;
	private float originalValue;
	public bool timerRunning;

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
			}
		}
	}

	public void ReinstatePlayerBody()
	{
		ragDoll.SetActive(false);
		playerBody.SetActive(true);
		playerGun.SetActive(true);
		downTime = originalValue;
	}

	void OnTriggerEnter (Collider coll)
	{
		if (coll.tag == "Fireball")
		{
			playerGun.SetActive(false);
			playerBody.SetActive(false);
			ragDoll.SetActive (true);
			timerRunning = true;
		}
	}

}

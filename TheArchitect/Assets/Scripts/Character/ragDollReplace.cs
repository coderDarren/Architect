using UnityEngine;
using System.Collections;

public class ragDollReplace : MonoBehaviour {

	public GameObject playerBody;
	public GameObject ragDoll;

	void OnTriggerEnter (Collider coll)
	{
		if (coll.tag == "DetTest")
		{
			playerBody.SetActive(false);
			ragDoll.SetActive (true);
		}
	}

}

using UnityEngine;
using System.Collections;

public class networkPlayer : Photon.MonoBehaviour {
	public GameObject myCamera;

	void Start () {
		if(photonView.isMine) {
			GetComponent<CharacterController>().enabled = true;

		}
	}
	

	void Update () {
	
	}
}

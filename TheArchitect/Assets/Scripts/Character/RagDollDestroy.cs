using UnityEngine;
using System.Collections;

public class RagDollDestroy : Photon.MonoBehaviour {

	public void DestroyRagDoll()
	{
		PhotonNetwork.Destroy(GetComponent<PhotonView>());
	}
}

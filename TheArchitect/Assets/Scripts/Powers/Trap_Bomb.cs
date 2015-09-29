using UnityEngine;
using System.Collections;

public class Trap_Bomb : Photon.MonoBehaviour {

    public GameObject sparksOnCollision;
	void OnTriggerEnter(Collider col)
    {
        string tag = col.gameObject.tag;
        if (tag == "Player" || tag == "Projectile")
        {
            GameObject go = Instantiate(sparksOnCollision, this.transform.position, Quaternion.identity) as GameObject;

            Destroy(gameObject);
            PhotonNetwork.Destroy(GetComponent<PhotonView>());
        }
    }
}

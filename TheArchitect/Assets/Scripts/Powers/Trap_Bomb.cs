using UnityEngine;
using System.Collections;

public class Trap_Bomb : Photon.MonoBehaviour {

	public float bombDamage = 20.0f;
    public GameObject sparksOnCollision;

	void OnTriggerEnter(Collider col)
    {
        string tag = col.gameObject.tag;
        if (tag == "Player" )
        {
            KillBomb();
        }
    }

    public void KillBomb()
    {
        GameObject go = Instantiate(sparksOnCollision, this.transform.position, Quaternion.identity) as GameObject;
        PhotonNetwork.Destroy(GetComponent<PhotonView>());
    }
}

using UnityEngine;
using System.Collections;

public class OnDamageInfo : MonoBehaviour {
	public float mDamage = 10;
	public string mFrom = "";
	public string mWeapon = "";
	public Vector3 mDirection = Vector3.zero;
	public bool mHeatShot = false;
	public int mWeaponID = 5;
	public PhotonPlayer mActor;
}
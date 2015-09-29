using UnityEngine;
using System.Collections;

public class detPointsTEST : MonoBehaviour {

	public Transform rightDet,leftDet,frontDet,backDet;
	public Object boomer;

	public void ReloadLevel() {
		Application.LoadLevel (Application.loadedLevel);
	}

	public void explodeLeft() {
		GameObject bomb1 = Instantiate(boomer,leftDet.position,Quaternion.identity) as GameObject;
	}

	public void explodeRight() {
		GameObject bomb2 = Instantiate(boomer,rightDet.position,Quaternion.identity) as GameObject;
	}

	public void explodeFront() {
		GameObject bomb3 = Instantiate(boomer,frontDet.position,Quaternion.identity) as GameObject;
	}

	public void explodeBack() {
		GameObject bomb4 = Instantiate(boomer,backDet.position,Quaternion.identity) as GameObject;
	}

	public void blowAll() {
		GameObject bomb1 = Instantiate(boomer,leftDet.position,Quaternion.identity) as GameObject;
		GameObject bomb2 = Instantiate(boomer,rightDet.position,Quaternion.identity) as GameObject;
		GameObject bomb3 = Instantiate(boomer,frontDet.position,Quaternion.identity) as GameObject;
		GameObject bomb4 = Instantiate(boomer,backDet.position,Quaternion.identity) as GameObject;
	}
}

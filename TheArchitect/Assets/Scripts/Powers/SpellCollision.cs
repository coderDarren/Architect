using UnityEngine;
using System.Collections;

public class SpellCollision : MonoBehaviour {

	public float life = 0.5f;

	float timer = 0;

	void Update () {

		timer += Time.deltaTime;
		if (timer >= life)
			Destroy(gameObject);
	}
}

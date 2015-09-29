using UnityEngine;
using System.Collections;

public class ProjectileLife : MonoBehaviour {

    float lifeSpan = 10;
    float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > lifeSpan)
        {
            Destroy(gameObject);
            PhotonNetwork.Destroy(GetComponent<PhotonView>());
        }
    }
}

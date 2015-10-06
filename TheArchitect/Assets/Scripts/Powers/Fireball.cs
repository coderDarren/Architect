using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour
{
    Vector3 syncPos;

    public GameObject sparksOnCollision;
    public float speed = 4;
    //[HideInInspector]
    public Vector3 direction;

	
    void Update()
    {
        TranslatePos();
    }

    void TranslatePos()
    {
            transform.Translate(direction * speed * Time.deltaTime);
    }

	
    void OnTriggerEnter(Collider col)
    {
        string tag = col.gameObject.tag;
        if (tag != "Fireball" && tag != "Player")
        {
//            GameObject go = Instantiate(sparksOnCollision as GameObject) as GameObject;
			GameObject go = PhotonNetwork.Instantiate(sparksOnCollision.name, this.transform.position, Quaternion.identity, 0);
                
            go.transform.position = transform.position;
			PhotonNetwork.Destroy(GetComponent<PhotonView>());

            Destroy(gameObject);
        }
        if (tag == "Enemy")
        {
            //col.gameObject.GetComponent<EnemyAttributes>().TakeDamage(damage);
        }
    }
}
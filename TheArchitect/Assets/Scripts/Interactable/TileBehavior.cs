using UnityEngine;
using System.Collections;

public class TileBehavior : MonoBehaviour {

    float deathTimer = 0;
    float shakeTimer = 0;
    Vector3 rotation;
    [HideInInspector]
    public bool dropping = false;
    float shakeDuration, fallSpeed, timeToDeath;
    public Color initialColor;

    void Start()
    {
        rotation = transform.eulerAngles;
        initialColor = GetComponent<Renderer>().materials[0].color;
    }

    void Update()
    {
        if (dropping)
        {
            DropTile(shakeDuration, fallSpeed, timeToDeath);
        }
    }

	public void DropTile(float shakeDuration, float fallSpeed, float timeToDeath)
    {
        this.shakeDuration = shakeDuration;
        this.fallSpeed = fallSpeed;
        this.timeToDeath = timeToDeath;
        dropping = true;
        shakeTimer += Time.deltaTime;
        deathTimer += Time.deltaTime;
        if (shakeTimer > shakeDuration)
            transform.position += Vector3.down * fallSpeed *Time.deltaTime;

        Shake();

        if (deathTimer > timeToDeath)
        {
            Destroy(gameObject);
            PhotonNetwork.Destroy(GetComponent<PhotonView>());
        }
    }

    void Shake()
    {
        //rotation.x += Mathf.PingPong(Time.time, 5);
        //rotation.z = Mathf.PingPong(Time.time, 3);
        rotation.y += Mathf.PingPong(Time.time, 5);
        transform.eulerAngles = rotation;
    }
}

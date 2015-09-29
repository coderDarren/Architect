using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JoinGameTimer : MonoBehaviour {

    public delegate void JoinTimer();
    public static event JoinTimer AssignPlayer;

    Text t;
    float timeToJoin = 5;
    float joinTimer = 0;

    void Start()
    {
        t = GetComponent<Text>();
        joinTimer = timeToJoin;
    }

    void Update()
    {
        joinTimer -= Time.deltaTime;
        t.text = "" + (int)joinTimer;
        if (joinTimer <= 0)
        {
			Debug.Log("Assigning Player");
            AssignPlayer();
            Destroy(transform.parent.gameObject);
        }
    }

}

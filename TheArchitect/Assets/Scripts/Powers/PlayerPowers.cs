using UnityEngine;
using System.Collections;

public class PlayerPowers : MonoBehaviour {

	[System.Serializable]
    public class Throw
    {
        public float recharge = 4f;
        public float rechargeTimer = 0;
        public float fireSpeed = 1;
        public string INPUT_AXIS = "Fire1";
        [HideInInspector]
        public float input = 0;
        public GameObject Projectile;
        public Transform spawnPos;
        public float throwForce = 250;
        public string ACTIVATE_INPUT = "Power 2";
        public GameObject PowerSelectedArrow;
    }

    [System.Serializable]
    public class Attack
    {
        public float recharge = 4f;
        public float rechargeTimer = 0;
        public float fireSpeed = 1;
        public string INPUT_AXIS = "Fire1";
        [HideInInspector]
        public float input = 0;
        public string ACTIVATE_INPUT = "Power 2";
        public GameObject PowerSelectedArrow;
    }

    public enum ActivePower { Throw, Attack };
    public ActivePower activePower;

    public Throw throwPower = new Throw();
    public Attack attackPower = new Attack();

    void Start()
    {
        activePower = ActivePower.Throw;
        throwPower.PowerSelectedArrow.SetActive(true);
        attackPower.PowerSelectedArrow.SetActive(false);
    }

    void GetInput()
    {
        throwPower.input = Input.GetAxis(throwPower.INPUT_AXIS);
        attackPower.input = Input.GetAxis(attackPower.INPUT_AXIS);
        if (Input.GetAxis(throwPower.ACTIVATE_INPUT) > 0)
        {
            activePower = ActivePower.Throw;

            throwPower.PowerSelectedArrow.SetActive(true);
            attackPower.PowerSelectedArrow.SetActive(false);
        }
        else if (Input.GetAxis(attackPower.ACTIVATE_INPUT) > 0)
        {
            activePower = ActivePower.Attack;
            throwPower.PowerSelectedArrow.SetActive(false);
            attackPower.PowerSelectedArrow.SetActive(true);
        }
    }

    void UpdateTimers()
    {
        throwPower.rechargeTimer += Time.deltaTime;
        attackPower.rechargeTimer += Time.deltaTime;
    }

    void Update()
    {
        GetInput();
        UpdateTimers();

        switch (activePower)
        {
            case ActivePower.Throw: UpdateThrowPower(); break;
            case ActivePower.Attack: UpdateAttackPower(); break;
        }
    }

    void UpdateThrowPower()
    {
        if (throwPower.rechargeTimer > throwPower.recharge) //if recharge is done
        {
            if (throwPower.input > 0) //if we click
            {
                //spawn a new projectile
                //add force to the projectile
                //the projectile should be able to kill itself overtime
                //GameObject go = Instantiate(throwPower.Projectile, throwPower.spawnPos.position, Quaternion.identity) as GameObject;
                GameObject go = PhotonNetwork.Instantiate(throwPower.Projectile.name, throwPower.spawnPos.position, Quaternion.identity, 0);

                throwPower.rechargeTimer = 0;
                go.GetComponent<Rigidbody>().AddForce(transform.forward*throwPower.throwForce);
            }
        }
    }

    void UpdateAttackPower()
    {
        if (attackPower.rechargeTimer > attackPower.recharge) //if recharge is done
        {
            if (attackPower.input > 0) //if we click
            {
                //do sphere collision check to find all attackable objects
                //for the first attackable object
                    //access the health component and do damage to it
                    //this object should be able to terminate itself
            }
        }
    }
}

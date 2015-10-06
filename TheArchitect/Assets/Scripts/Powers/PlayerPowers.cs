using UnityEngine;
using System.Collections;

public class PlayerPowers : MonoBehaviour {

	[System.Serializable]
    public class Laser
    {
        public float recharge = 4f;
        public float rechargeTimer = 0;
        public float fireSpeed = 1;
        public string INPUT_AXIS = "Fire1";
        [HideInInspector]
        public float input = 0;
        public GameObject LaserGun;
        public float throwForce = 250;
        public string ACTIVATE_INPUT = "Power 1";
    }

    public float rotateSmooth = 5;
    public float range = 20;
    public Camera cam;

    Quaternion initialRotation;
    Quaternion targetRotation;
    Ray ray;
    RaycastHit hitInfo;

    public enum ActivePower { Laser };
    public ActivePower activePower;

    public Laser laser = new Laser();

    void Start()
    {
        activePower = ActivePower.Laser;
        initialRotation = laser.LaserGun.transform.rotation;
    }

    void GetInput()
    {
        laser.input = Input.GetAxis(laser.INPUT_AXIS);
    }

    void UpdateTimers()
    {
        laser.rechargeTimer += Time.deltaTime;
    }

    void Update()
    {
        GetInput();
        UpdateTimers();

        switch (activePower)
        {
            case ActivePower.Laser: 
                UpdateLaserPower(); break;
        }
        RotateLaser();
    }

    void UpdateLaserPower()
    {
        if (laser.rechargeTimer > laser.recharge) //if recharge is done
        {
            if (laser.input > 0) //if we click
            {
                //spawn a new projectile
                //add force to the projectile
                //the projectile should be able to kill itself overtime
                //GameObject go = Instantiate(throwPower.Projectile, throwPower.spawnPos.position, Quaternion.identity) as GameObject;
                
                laser.rechargeTimer = 0;
            }
        }
    }

    void RotateLaser()
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitInfo)) //if we are hovering over something
        {
            if (Vector3.Distance(hitInfo.point, transform.position) <= range) //if we are close enough to the thing we are hovering over
            {
                //find target rotation

                targetRotation = Quaternion.LookRotation(hitInfo.point - transform.position);
            }
            else
            {
                //set rotation to initial
                targetRotation = initialRotation;
            }
        }
        else
        {
            //set rotation to initial
            targetRotation = initialRotation;
        }

        laser.LaserGun.transform.rotation = Quaternion.Slerp(laser.LaserGun.transform.rotation, targetRotation, rotateSmooth * Time.deltaTime);

    }
}

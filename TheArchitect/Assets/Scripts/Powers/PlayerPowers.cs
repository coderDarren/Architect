using UnityEngine;
using System.Collections;

public class PlayerPowers : MonoBehaviour {

	[System.Serializable]
    public class Laser
    {
        public float recharge = 0f;
        public float rechargeTimer = 0;
        public float fireSpeed = 1;
        public float rotateSmooth = 5;
        public float range = 20;
        public string INPUT_AXIS = "Fire1";
        [HideInInspector]
        public float input = 0;
        public GameObject LaserGun;
        public GameObject LaserObject; //the line renderer object
        public Transform LaserSpawn;
        public LayerMask laserHitLayer;
        public GameObject laserSparks;
        public string ACTIVATE_INPUT = "Power 1";
        public GameObject PowerSelectedArrow;
    }

    [System.Serializable]
    public class Smoke
    {
        public float recharge = 15f;
        public float rechargeTimer = 0;
        public string INPUT_AXIS = "Fire1";
        [HideInInspector]
        public float input = 0;
        public GameObject SmokeObject;
        public string ACTIVATE_INPUT = "Power 2";
        public GameObject PowerSelectedArrow;
    }

    
    public Camera cam;

    Quaternion initialRotation;
    Quaternion targetRotation;
    Ray ray;
    RaycastHit hitInfo;
    LineRenderer laserLine;
    GameObject laserSparks;

    GameObject PlayerCanvas;

    public enum ActivePower { Laser, Smoke };
    public ActivePower activePower;

    public Laser laser = new Laser();
    public Smoke smoke = new Smoke();

    void Start()
    {
        activePower = ActivePower.Laser;
        initialRotation = laser.LaserGun.transform.rotation;
        GameObject go = PhotonView.Instantiate(laser.LaserObject);
        go.transform.position = laser.LaserGun.transform.position;
        laserLine = go.GetComponent<LineRenderer>();
        laserLine.SetPosition(0, laser.LaserSpawn.position);
        laserLine.SetPosition(1, go.transform.position);
        laserSparks = null;

        laser.PowerSelectedArrow.SetActive(true);
        smoke.PowerSelectedArrow.SetActive(false);

        PlayerCanvas = transform.GetChild(0).gameObject;
        PlayerCanvas.transform.SetParent(null);
    }

    void GetInput()
    {
        laser.input = Input.GetAxis(laser.INPUT_AXIS);
        smoke.input = Input.GetAxis(smoke.INPUT_AXIS);

        if (Input.GetAxis(laser.ACTIVATE_INPUT) > 0)
        {
            activePower = ActivePower.Laser;

            laser.PowerSelectedArrow.SetActive(true);
            smoke.PowerSelectedArrow.SetActive(false);
        }
        else if (Input.GetAxis(smoke.ACTIVATE_INPUT) > 0)
        {
            activePower = ActivePower.Smoke;
            smoke.PowerSelectedArrow.SetActive(true);
            laser.PowerSelectedArrow.SetActive(false);
        }
    }

    void UpdateTimers()
    {
        laser.rechargeTimer += Time.deltaTime;
        smoke.rechargeTimer += Time.deltaTime;
    }

    void Update()
    {
        GetInput();
        UpdateTimers();

        switch (activePower)
        {
            case ActivePower.Laser: 
                UpdateLaserPower();
                break;
            case ActivePower.Smoke:
                UpdateSmoke();
                break;
        }
        RotateLaser();
    }

    void UpdateLaserPower()
    {
        /*if (laser.rechargeTimer > laser.recharge) //if recharge is done
        {
        }*/
        if (laser.input > 0) //if we click
        {
            RaycastHit hit;
            if (Physics.Raycast(laser.LaserSpawn.position, laser.LaserSpawn.forward, out hit, laser.range, laser.laserHitLayer)) //position laser based on gun's forward vector
            {
                if (!laserSparks) //only if we have not created the sparks or we set them back to null
                {
                    //spawn sparks
                    laserSparks = PhotonNetwork.Instantiate(laser.laserSparks.name, hit.point, Quaternion.identity, 0);
                }
                else
                {
                    //position sparks
                    laserSparks.transform.position = hit.point-((hit.point-laser.LaserSpawn.position)*0.02f);
                }
                laserLine.SetPosition(0, laser.LaserSpawn.position);
                laserLine.SetPosition(1, hit.point);

                //check if the laser is hitting a bomb
                if (hit.collider.gameObject.tag.Equals("Bomb"))
                {
                    hit.collider.GetComponent<Trap_Bomb>().KillBomb();
                }
            }
            else //if while dragging we go off the laser hit layer
            {
                laserLine.SetPosition(0, laser.LaserSpawn.position);
                laserLine.SetPosition(1, laser.LaserSpawn.position);
                if (laserSparks)
                {
                    PhotonNetwork.Destroy(laserSparks.GetComponent<PhotonView>());
                    laserSparks = null;
                }
            }
            
            //cause damage to hitInfo target
        }
        else
        {
            laserLine.SetPosition(0, laser.LaserSpawn.position);
            laserLine.SetPosition(1, laser.LaserSpawn.position);
            if (laserSparks)
            {
                PhotonNetwork.Destroy(laserSparks.GetComponent<PhotonView>());
                laserSparks = null;
            }
        }
    }

    void RotateLaser()
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitInfo, laser.range, laser.laserHitLayer)) //if we are hovering over something
        {
            if (Vector3.Distance(hitInfo.point, transform.position) <= laser.range) //if we are close enough to the thing we are hovering over
            {
                //find target rotation
                if (laser.input > 0)
                    targetRotation = Quaternion.LookRotation(hitInfo.point - laser.LaserGun.transform.position);
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

        laser.LaserGun.transform.rotation = Quaternion.Lerp(laser.LaserGun.transform.rotation, targetRotation, laser.rotateSmooth * Time.deltaTime);

    }

    void UpdateSmoke()
    {
        if (smoke.rechargeTimer > smoke.recharge)
        {
            if (smoke.input > 0)
            {
                GameObject smokeBomb = PhotonNetwork.Instantiate(smoke.SmokeObject.name, laser.LaserSpawn.position, Quaternion.identity, 0);
                smokeBomb.GetComponent<Rigidbody>().AddForce(laser.LaserSpawn.forward * 400);
                smoke.rechargeTimer = 0;
            }
        }
    }

}

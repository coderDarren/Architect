using UnityEngine;
using System.Collections;

public class thirdPersonNetwork : Photon.MonoBehaviour {

        public GameObject cam;
		StandardCamera cameraScript;
		CharacterMovement controllerScript;
        
		
		void Awake()
		{

            cameraScript = cam.GetComponent<StandardCamera>();
            controllerScript = GetComponent<CharacterMovement>();
			
			if (photonView.isMine)
			{
				//MINE: local player, simply enable the local scripts
                if (cam.GetComponent<RTSCamera>())
                {

                }
                else
                {
                    cameraScript.enabled = true;
                    controllerScript.enabled = true;
                    cameraScript.Initialize(transform);
                    cam.transform.parent = null;
                }
			}
			else
			{
                if (cam.GetComponent<RTSCamera>())
                {
                    Destroy(gameObject);
                }
                else
                {
                    Destroy(cameraScript.gameObject);
                    cameraScript.enabled = false;
                }
				controllerScript.enabled = true;
                controllerScript.GetComponent<PlayerPowers>().enabled = false;
				controllerScript.isControllable = false;
			}
			
			gameObject.name = gameObject.name + photonView.viewID;
           
		}
		
		void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
		{
			if (stream.isWriting)
			{
				//We own this player: send the others our data
				//stream.SendNext((int)controllerScript._characterState);
                //stream bool for grounded, float for running
                if (!cam.GetComponent<RTSCamera>())
                {
                    stream.SendNext(controllerScript.forwardInput);
                    stream.SendNext(controllerScript.grounded);
                    stream.SendNext(transform.position);
                    stream.SendNext(transform.rotation);
                    stream.SendNext(controllerScript.velocity);
                }
			}
			else
			{
				//Network player, receive data
				//controllerScript._characterState = (CharacterState)(int)stream.ReceiveNext();
                //stream bool for grounded, float for running
                forwardInput = (float)stream.ReceiveNext();
                grounded = (bool)stream.ReceiveNext();
				correctPlayerPos = (Vector3)stream.ReceiveNext();
				correctPlayerRot = (Quaternion)stream.ReceiveNext();
                correctPlayerVelocity = (Vector3)stream.ReceiveNext();
			}
            
		}
		
		private Vector3 correctPlayerPos = Vector3.zero; //We lerp towards this
		private Quaternion correctPlayerRot = Quaternion.identity; //We lerp towards this
        float forwardInput = 0;
        bool grounded = false;
        Vector3 correctPlayerVelocity = Vector3.zero;
		
		void FixedUpdate()
		{
			if (!photonView.isMine)
			{
				//Update remote player (smooth this, this looks good, at the cost of some accuracy)
				transform.position = Vector3.Lerp(transform.position, correctPlayerPos, Time.deltaTime * 5);
				transform.rotation = Quaternion.Lerp(transform.rotation, correctPlayerRot, Time.deltaTime * 5);
                controllerScript.forwardInput = forwardInput;
                controllerScript.grounded = grounded;
                controllerScript.velocity = correctPlayerVelocity;
			}
		}
		
	}
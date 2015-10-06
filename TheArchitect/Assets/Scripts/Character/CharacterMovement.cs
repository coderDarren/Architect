using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

    //public static CharacterMovement Instance;

    public Vector3 playerScale= Vector3.zero; //due to strange parenting bug - used in Rotater script

    [System.Serializable]
    public class MoveSettings
    {
        public float forwardVel = 12;
        public float rotateVel = 3;
        public float mouseRotateVel = 0.1f;
        public float jumpVel = 5;
        public float distToGrounded = 0.1f;
        public Transform[] groundCheckPoints;
        public LayerMask ground;
    }

    [System.Serializable]
    public class PhysSettings
    {
        public float downAccel = 0.3f;
    }

    [System.Serializable]
    public class InputSettings
    {
        public float inputDelay = 0.1f;
        public string FORWARD_AXIS = "Vertical";
        public string TURN_AXIS = "Horizontal";
        public string JUMP_AXIS = "Jump";
        public string WALK_AXIS = "Walk";
    }
    
    public MoveSettings moveSetting = new MoveSettings();
    public PhysSettings physSetting = new PhysSettings();
    public InputSettings inputSetting = new InputSettings();

    //[HideInInspector]
    public Vector3 velocity = Vector3.zero;
    [HideInInspector]
    public Quaternion targetRotation;
    Rigidbody rBody;
    [HideInInspector]
    public float forwardInput, turnInput, jumpInput, walkInput;
    Vector3 previousMousePos = Vector3.zero;
    Vector3 currentMousePos = Vector3.zero;
    Ray groundCheckRay;

    float _forwardVel = 0;
    float _rotateVel = 0;
    float _jumpVel = 0;
    float _downAccel = 0;
    float reducedSpeed = 1;

    public bool isControllable = true;
    public bool grounded = false;
    bool paused = false;
    Vector3 pausePos;

    public Quaternion TargetRotation { get { return targetRotation; } }
    public bool Paused { get { return paused; } }

    //public to be used by animator controller
    public bool CheckGrounded()
    {
        foreach (Transform t in moveSetting.groundCheckPoints)
        {
            Debug.DrawLine(t.position, t.position + Vector3.down * moveSetting.distToGrounded, Color.green);
        }

        foreach (Transform t in moveSetting.groundCheckPoints)
        {
            Debug.DrawLine(t.position, t.position + Vector3.down * moveSetting.distToGrounded, Color.green);
            if (Physics.Raycast(t.position, Vector3.down, moveSetting.distToGrounded, moveSetting.ground))
                return true;
        }
        return false;
    }
    public float ReducedSpeed { get { return reducedSpeed; } }


    void Start()
    {
        /*if (Instance != null)
        {
            Instance.gameObject.transform.position = Vector3.zero; //reset player position on restart
            Instance.gameObject.transform.eulerAngles = Vector3.zero;
            Instance.targetRotation = Instance.gameObject.transform.rotation;
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        */
        playerScale = transform.localScale;
        targetRotation = transform.rotation;
        if (GetComponent<Rigidbody>())
            rBody = GetComponent<Rigidbody>();
        else
            Debug.LogError("The character needs a rigidbody.");

        forwardInput = turnInput = jumpInput = walkInput = 0;

        _forwardVel = moveSetting.forwardVel;
        _rotateVel = moveSetting.rotateVel;
        _jumpVel = moveSetting.jumpVel;
        _downAccel = physSetting.downAccel;
    }

    //floats returned for the animator controller
    public float GetRunInput() { return forwardInput; }
    public float GetWalkInput() { return walkInput; }

    void GetInput()
    {
        forwardInput = Input.GetAxisRaw(inputSetting.FORWARD_AXIS); 
        turnInput = Input.GetAxisRaw(inputSetting.TURN_AXIS); 
        jumpInput = Input.GetAxisRaw(inputSetting.JUMP_AXIS); 
        walkInput = Input.GetAxisRaw(inputSetting.WALK_AXIS);
    }

    void ZeroAllInput()
    {
        forwardInput = 0;
        turnInput = 0;
        jumpInput = 0;
        walkInput = 0;
    }

    
    //called from state handler
    public void UpdateCharacterInput()
    {
        if (isControllable)
        {

            grounded = CheckGrounded();
            if (!paused)
                GetInput();
            else
                ZeroAllInput();
        }
    }

    //goes in fixedupdate
    //called from state handler
    public void FixedUpdate()
    {
        if (isControllable)
        {
            UpdateCharacterInput();

            previousMousePos = currentMousePos;
            currentMousePos = Input.mousePosition;

            if (!paused)
            {
                Run();
                Turn();
                Jump();
                rBody.velocity = transform.TransformDirection(velocity);
            }
            else
            {
                transform.position = pausePos;
                rBody.velocity = Vector3.zero;
            }

        }
    }

    void Run()
    {
        if (forwardInput > inputSetting.inputDelay)
        {
            //move
            velocity.z = _forwardVel * forwardInput;
            if (walkInput > 0)
                velocity.z /= 2.5f;
        }
        else
            //zero velocity
            velocity.z = 0;

    }

    void Turn()
    {
        if (Mathf.Abs(turnInput) > inputSetting.inputDelay)
        {
            targetRotation *= Quaternion.AngleAxis(_rotateVel * turnInput, Vector3.up);
        }
        transform.rotation = targetRotation;
    }

    void Jump()
    {
        if (jumpInput > 0 && grounded)
        {
            //jump
            velocity.y = _jumpVel;
        }
        else if (jumpInput == 0 && grounded)
        {
            //zero out our velocity.y
            velocity.y = 0;
        }
        else
        {
            //decrease velocity.y
            if (velocity.y > -10)
                velocity.y -= _downAccel;
        }
    }

    void PausePlayer(bool paused)
    {
        this.paused = paused;
        pausePos = transform.position;
    }
}

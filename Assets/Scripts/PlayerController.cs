using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float initialSpeed;
    private Rigidbody rb;

    private Vector3 touchOrigin, touchCurrent, playerOrigin;

    public float sensibility = 1.0f;
    private float convertPixelsToUnits = 6.6f / Screen.width;

    private bool enteredCollision = false;
    private float valueOfAcceleration = 5.0f;

    private bool canStart = false;

    private bool hitObstacle = false;

    private int collisionCount = 0;

    private bool isInCombo = false;
    private bool isInCenter = false;

    public bool CanStart
    {
        get
        {
            return canStart;
        }

        set
        {
            canStart = value;
        }
    }

    private bool waitForStart = false;

    public bool WaitForStart
    {
        get
        {
            return waitForStart;
        }

        set
        {
            waitForStart = value;
        }
    }

    public bool HitObstacle
    {
        get
        {
            return hitObstacle;
        }

        set
        {
            hitObstacle = value;
        }
    }

    public bool IsInCombo
    {
        get
        {
            return isInCombo;
        }

        set
        {
            isInCombo = value;
        }
    }

    public bool IsInCenter
    {
        get
        {
            return isInCenter;
        }

        set
        {
            isInCenter = value;
        }
    }

    // Use this for initialization
    void Start () {
        // Initialize and prepare the ball to be launched
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        touchOrigin = Vector3.zero;
        touchCurrent = Vector3.zero;
        playerOrigin = Vector3.zero;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        // Waiting for the manager to tell the player to wait for a user contact
        if (!CanStart && WaitForStart)
        {
            NowWaitingForStart();
        }
        // Input and movement management
		if(Input.touchCount > 0 && CanStart)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    touchOrigin = touch.position;
                    playerOrigin = this.transform.position;
                }
                touchCurrent = touch.position;
                this.transform.position = new Vector3(playerOrigin.x + ((touchCurrent.x - touchOrigin.x) * sensibility * convertPixelsToUnits), this.transform.position.y, this.transform.position.z);
                if (this.transform.position.x > 3.3f)
                {
                    this.transform.position = new Vector3(3.3f, this.transform.position.y, this.transform.position.z);
                }
                else if (this.transform.position.x < -3.3f)
                {
                    this.transform.position = new Vector3(-3.3f, this.transform.position.y, this.transform.position.z);
                }
            }
        }
        if(enteredCollision)
        {
            // Change the force applied according to state of the ball
            rb.AddForce(rb.mass * 4 * (valueOfAcceleration * Vector3.forward));
        }
        else if (rb.velocity.z > 2.0f)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z - 0.50f);
        }
        if (rb.velocity.z > 40.0f)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 40.0f);
        }
        if (rb.velocity.y > 30.0f)
        {
            rb.velocity = new Vector3(0, 30.0f, rb.velocity.z);
        }
        rb.velocity = new Vector3(0.0f, rb.velocity.y, rb.velocity.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        collisionCount++;
        // If the ball hit an obstacle
        if (collision.gameObject.name.Contains("Obstacle"))
        {
            HitObstacle = true;
        }
        // If the ball is rolling on arrows
        else if (collision.gameObject.GetComponent<Arrows>())
        {
            if(!IsInCenter)
            {
                IsInCombo = false;
            }
            if (collision.gameObject.GetComponent<Arrows>().ArrowDirection == Arrows.Direction.Forward)
            {
                valueOfAcceleration = 10.0f;
            }
            else
            {
                valueOfAcceleration = 2.5f;
            }
        }
        // If the ball is at the center of the plateform
        else if(collision.gameObject.name.Contains("Center"))
        {
            valueOfAcceleration = 7.0f;
            IsInCenter = true;
            IsInCombo = true;
        }
        else
        {
            if (!IsInCenter)
            {
                IsInCombo = false;
            }
            valueOfAcceleration = 5.0f;
        }
        enteredCollision = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        collisionCount--;
        // If the ball just left an arrow
        if (collision.gameObject.GetComponent<Arrows>())
        {
            valueOfAcceleration = 5.0f;
            return;
        }
        // If it left the center of the plateform
        else if(collision.gameObject.name.Contains("Center"))
        {
            IsInCenter = false;
            if(collisionCount > 0)
            {
                IsInCombo = false;
                valueOfAcceleration = 5.0f;
            }
        }
        else
        {
            enteredCollision = false;
        }
    }

    // Called when the ball has to wait for a user contact
    public void NowWaitingForStart()
    {
        if (Input.touchCount > 0)
        {
            CanStart = true;
            LaunchGame();
        }
    }

    // Called to launch the ball
    private void LaunchGame()
    {
        rb.AddForce(rb.mass * 5 * (initialSpeed * Vector3.forward));
        rb.useGravity = true;
    }

    // Called to stop the ball
    public void StopCurrentGame()
    {
        rb.velocity = Vector3.zero;
        CanStart = false;
        WaitForStart = false;
    }
}

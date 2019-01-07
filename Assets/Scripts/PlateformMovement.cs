using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformMovement : MonoBehaviour {

    private bool moving = false;

    public bool Moving
    {
        get
        {
            return moving;
        }

        set
        {
            moving = value;
        }
    }

    private bool moveLeft = true;

    // Use this for initialization
    void Start () {
        ResetMoving();
	}
	
	// Update is called once per frame
	void Update () {
        // If the plateform should move left-right
		if (Moving)
        {
            if (transform.position.x < -4.0f)
            {
                moveLeft = false;
            }
            else if (transform.position.x > 4.0f)
            {
                moveLeft = true;
            }
            if (moveLeft)
            {
                transform.position += new Vector3(-0.1f, 0f, 0f);
            }
            else
            {
                transform.position += new Vector3(0.1f, 0f, 0f);
            }
        }
	}

    // Function to decide if the plateform should move left-right
    public void ResetMoving()
    {
        if (transform.position.z == 0.0f)
        {
            Moving = false;
            return;
        }
        float willMove = Random.Range(0.0f, 5.0f);
        if (willMove < 2.0f)
        {
            Moving = true;
        }
    }
}

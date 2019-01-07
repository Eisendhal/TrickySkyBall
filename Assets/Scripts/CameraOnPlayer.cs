using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOnPlayer : MonoBehaviour {

    public Transform Target;
    private Vector3 awayFromPlayer;
    private Vector3 TargetLookAt;

	// Use this for initialization
	void Start () {
        awayFromPlayer = new Vector3(0, 7.0f, -11f);
        TargetLookAt = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(0, Target.transform.position.y + awayFromPlayer.y, Target.transform.position.z + awayFromPlayer.z);
        TargetLookAt = new Vector3(0, Target.transform.position.y, Target.transform.position.z);
        transform.LookAt(TargetLookAt);
	}
}

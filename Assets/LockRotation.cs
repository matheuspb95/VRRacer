using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : MonoBehaviour {
	Vector3 startRot;
	// Use this for initialization
	void Start () {
		startRot = transform.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
		transform.eulerAngles = startRot;
	}
}

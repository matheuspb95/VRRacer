using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour {
	public float ForwardForce;
	public float TurnRate;
	public float UpDownForce;

	public float AccelerationRate;
	public Transform ship;
	Rigidbody body;
	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		ship.localEulerAngles = new Vector3(Input.GetAxis("Horizontal") * -90, -90, Input.GetAxis("Vertical") * 10);
		transform.Rotate(new Vector3(UpDownForce * Input.GetAxis("Vertical"), 0, TurnRate * Input.GetAxis("Horizontal")) * Time.deltaTime);
		body.velocity = transform.forward.normalized * (ForwardForce + Input.GetAxis("Acceleration") * AccelerationRate);// + transform.right * 0.1f * TurnRate * Input.GetAxis("Horizontal");
	}
}

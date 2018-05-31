using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour {
	public Transform pivot;
	public float distanceFromPivot;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0)){
			Vector2 dir = new Vector2(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
			transform.Rotate(dir * 10);
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
		}
		
		Ray ray = new Ray(pivot.position, -transform.forward);
		Debug.DrawRay(pivot.position, -transform.forward * distanceFromPivot);
		transform.position = ray.GetPoint(distanceFromPivot);
	}
}

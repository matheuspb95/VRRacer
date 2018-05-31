using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CreateCheckPoints : MonoBehaviour {
	public BezierSpline spline;
	// Use this for initialization
	void Start () {
		spline = GetComponent<BezierSpline>();
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < transform.childCount; i++){
			Transform child = transform.GetChild(i);
			child.position = spline.GetPoint((float)i / (float)transform.childCount);
			child.up = spline.GetVelocity((float)i / (float)transform.childCount);
		}
	}
}

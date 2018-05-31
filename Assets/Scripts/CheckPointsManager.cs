using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointsManager : MonoBehaviour {
	int activeChild;
	public Material green, blue;
	// Use this for initialization
	void Start () {
		activeChild = 0;
	}

	public void ChangeCheckPoint(){
		transform.GetChild(activeChild).GetComponent<Renderer>().material = blue;
		transform.GetChild(activeChild).GetComponent<Collider>().enabled = false;
		activeChild++;
		if(activeChild >= transform.childCount) activeChild = 0;
		transform.GetChild(activeChild).GetComponent<Renderer>().material = green;
		transform.GetChild(activeChild).GetComponent<Collider>().enabled = true;
	}
}

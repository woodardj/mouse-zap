﻿using UnityEngine;
using System.Collections;

public class FloorScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void wireHasBroken () {
		// stop flashing animation
		gameObject.GetComponent<Animator> ().SetBool ("wireBreaking", false);
	}


}

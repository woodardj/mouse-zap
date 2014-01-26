using UnityEngine;
using System.Collections;

public class WireScript : MonoBehaviour {
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void wireHasBroken () {
//		Debug.Log ("Wire broken.");
		// stop wire breaking animation
		gameObject.GetComponent<Animator> ().SetBool ("wireBreaking", false);
		gameObject.renderer.enabled = false;
		// remove the wire from the game
		Destroy (this.gameObject);

	}
}

using UnityEngine;
using System.Collections;

public class KitchenBackOut : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey (KeyCode.LeftArrow) ) {
			Application.LoadLevel ("CircuitKitchenScene");
			Debug.Log ("Down key");
		}
	}
}

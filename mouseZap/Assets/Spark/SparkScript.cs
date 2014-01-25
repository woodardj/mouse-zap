using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SparkScript : MonoBehaviour {


	public GameObject test;

	public float movementSpeed = 0.3f;


	private List<GameObject> _collidedWires; // keeps a list of all wire gameObjects that are currently collided with the spark.

	// Use this for initialization
	void Start () {
		_collidedWires = new List<GameObject> ();
	}

	void OnTriggerEnter (Collider other){
		// store the collided object
		_collidedWires.Add(other.gameObject);

		// turn on light if touching object
		if (other.gameObject.CompareTag("Light") == true) {
			GameManager.instance.ActivateLight(other.gameObject);
//			Activatable objectScript other.gameObject.GetComponent<Activatable>();
		}
	}
	void OnTriggerExit (Collider other){
		// remove the collided object
		_collidedWires.Remove(other.gameObject);

		// turn off light if leaving
		if (other.gameObject.CompareTag("Light") == true) {
			GameManager.instance.DeactivateLight(other.gameObject);
		}


		// make sure the spark never leaves all wires.
		bool leftAllWires = true;
		foreach (GameObject wire in _collidedWires) {
			if (wire.transform.gameObject.CompareTag("Wire") == true) {
				leftAllWires = false;
			}
		}
		if (leftAllWires == true) {
			// we need to return to this wire
			Debug.Log ("Left all wires!!");
			if (other.gameObject.transform.localScale.x > 1) {
				// this is a left right wire, reset Z
				Vector3 newPos = this.transform.localPosition;
				newPos.z = other.gameObject.transform.position.z;
				this.transform.localPosition = newPos;
			} else if (other.gameObject.transform.localScale.z > 1) {
				// this is a up down wire, reset X
				Vector3 newPos = this.transform.localPosition;
				newPos.x = other.gameObject.transform.position.x;
				this.transform.localPosition = newPos;
			}
		}
//		Debug.Log ("trigger exit object:" + other.gameObject.ToString());
	}

	// Update is called once per frame
	void Update () {
		float h = Input.GetAxis("Horizontal") * movementSpeed;
		float v = Input.GetAxis("Vertical") * movementSpeed;
		if ((h != 0f) | (v != 0f)) {
			foreach (GameObject wire in _collidedWires) {
				if (wire.transform.gameObject.CompareTag("Wire") == true) {
					// hit a wire. // decide if we can move left or right on this wire
					if (wire.transform.localScale.x > 1) {
						// move 
						Vector3 newPosition = this.transform.position + new Vector3(h,0f,0f);
						// clamp position
						if (_collidedWires.Count == 1)
							newPosition.z = wire.transform.position.z;
						newPosition.x = Mathf.Clamp(newPosition.x,
						                            (wire.transform.localPosition.x - (wire.transform.localScale.x * 0.5f)),
						                            (wire.transform.localPosition.x + (wire.transform.localScale.x * 0.5f)));
						this.transform.position = newPosition;
					} else if (wire.transform.localScale.z > 1) {
						// move 
						Vector3 newPosition = this.transform.position + new Vector3(0f,0f,v);
						// clamp position
						if (_collidedWires.Count == 1)
							newPosition.x = wire.transform.position.x;
						newPosition.z = Mathf.Clamp(newPosition.z,
						                            (wire.transform.localPosition.z - (wire.transform.localScale.z * 0.5f)),
						                            (wire.transform.localPosition.z + (wire.transform.localScale.z * 0.5f)));
						this.transform.position = newPosition;
					}
				}
			}
		}

		if (Input.GetButtonDown ("Jump")) {
			// user pressed the activate button
			// check for something to activate


		}
	}
}

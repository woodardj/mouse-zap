using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SparkScript : MonoBehaviour {




	public float movementSpeed = 0.3f;


	private List<GameObject> _collidedWires;

	// Use this for initialization
	void Start () {
		_collidedWires = new List<GameObject> ();
	}

	void OnTriggerEnter (Collider other){
		_collidedWires.Add(other.gameObject);
//		Debug.Log ("trigger enter object:" + other.gameObject.ToString());
	}
	void OnTriggerExit (Collider other){
		_collidedWires.Remove(other.gameObject);
//		Debug.Log ("trigger exit object:" + other.gameObject.ToString());
	}

	// Update is called once per frame
	void Update () {
		float h = Input.GetAxis("Horizontal") * movementSpeed;
		float v = Input.GetAxis("Vertical") * movementSpeed;
		if ((h != 0f) | (v != 0f)) {
			// move left or right if able
			foreach (GameObject wire in _collidedWires) {

//			// ray cast directly downward and get all wires that collide 
//			Ray ray = new Ray(this.transform.position, Vector3.down );
//			RaycastHit hit; 
//			// If the ray collides with something solid in the scene, the "hit" structure will be filled with collision information
//			if( Physics.Raycast( ray, out hit ) )
//			{
				// a collision occured.
//				Debug.LogWarning("Hit this object:" + wire.transform.gameObject.ToString());
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
		
	}
}

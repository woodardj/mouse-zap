using UnityEngine;
using System.Collections;

public class CanOpenerScript : MonoBehaviour {

	public float movementSpeed = 1.0f;
	public Transform canToOpen;

	private bool _dontAllowTurning;

	private float _canTurnedDegrees;
	private float _moveOffCanOpenerAccumulator;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (_dontAllowTurning == true) {
			// no sfx 
			Camera.main.audio.Stop ();
//			audio.Stop();
			return;
		}

		// Can opener MOVEMENT
		float h = Input.GetAxis("Horizontal") * movementSpeed;
		float v = Input.GetAxis("Vertical") * movementSpeed;
		if (h != 0f) {
			_moveOffCanOpenerAccumulator = 0f; // reset the move off accumulator

			// Rotate all the cans!
			GameObject[] allCans = GameObject.FindGameObjectsWithTag("Can");
			for (int i = 0; i < allCans.Length; i++) {
				GameObject can = allCans[i];
				can.transform.Rotate(0f,h,0f);
//				if (can.transform.localRotation.eulerAngles.z > 0) 
//					newRotation.x +=h;
////				else if (can.transform.rotation.y > 0)
////					newRotation.x +=h;
//				else
//					newRotation.y += h;
//				allCans[i].transform.rotation =  Quaternion.Euler(newRotation.x, newRotation.y, newRotation.z);
			}
			_canTurnedDegrees += h;

			// play sfx while moving
			if (Camera.main.audio.isPlaying == false)
				Camera.main.audio.Play ();
//			audio.Play();

			// check if we have turned the can 360 degrees and opened it
			if ((_canTurnedDegrees > 360f) | (_canTurnedDegrees < -360f)) {
				// WINNER
				Debug.Log("Can opened! Good Job.");
				_dontAllowTurning = true;
				_canTurnedDegrees = 0f; // reset so this doesn't run again

				// load next scene
				Application.LoadLevel("LightedKitchenWin");
			}
		} else {
			// no sfx 
			Camera.main.audio.Stop ();
//			audio.Stop();
		}

		if (v != 0f) {
			// move off the can opener if pressed long enough
			_moveOffCanOpenerAccumulator += Mathf.Abs(v);

			if (_moveOffCanOpenerAccumulator > 40f) {
				_moveOffCanOpenerAccumulator = 0f;
				Debug.Log("Leaving Can opener unfinished.");

//				// set the starting position of the spark
//				Debug.Log ("transform.position:" + transform.position.ToString ());
//				GameManager.sparkPosition = transform.position;
				// load previous scene
				Application.LoadLevel("CircuitKitchenScene");
			}
		}
	}
}

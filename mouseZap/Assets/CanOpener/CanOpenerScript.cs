using UnityEngine;
using System.Collections;

public class CanOpenerScript : MonoBehaviour {

//	public Vector3 speechBubbleOffset = new Vector3(0f, 0f, 0f);
//	public float speechBubbleWidth = 120f;
//	public GUIStyle speechStyle;

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
			audio.Stop();
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
				Vector3 newRotation = allCans[i].transform.rotation.eulerAngles;
				newRotation.y += h;
				allCans[i].transform.rotation =  Quaternion.Euler(newRotation.x, newRotation.y, newRotation.z);
			}
			_canTurnedDegrees += h;

			// play sfx while moving
			audio.Play();

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
			audio.Stop();
		}

		if (v != 0f) {
			// move off the can opener if pressed long enough
			_moveOffCanOpenerAccumulator += Mathf.Abs(v);

			if (_moveOffCanOpenerAccumulator > 40f) {
				_moveOffCanOpenerAccumulator = 0f;
				Debug.Log("Leaving Can opener unfinished.");
				// load previous scene
				Application.LoadLevel("CircuitKitchenScene");
			}
		}
	}





	// SPEECH BUBBLES
//	public void ShowSpeechBubble(Transform talkingTransform, string message, float timeToDisplay) {
//		// show the bubble
//		speechBubbleText = message;
//		showSpeechBubble = true;
//		//StartCoroutine (HideSpeechBubbleAfterSeconds (timeToDisplay));
//	}
//	
//	IEnumerator HideSpeechBubbleAfterSeconds(float timeToDisplay) {
//		yield return new WaitForSeconds(timeToDisplay);
//		//		Debug.Log ("Hiding speech bubble");
//		showSpeechBubble = false;
//	}
//	// OnGUI is called once per frame
//	void OnGUI () {
//		if (showSpeechBubble == true) {
//			Vector3 point = Camera.main.WorldToScreenPoint(transform.position + speechBubbleOffset);
//			float height = speechStyle.CalcHeight( new GUIContent(speechBubbleText), speechBubbleWidth);
//			Rect rect = new Rect (0f, 0f, speechBubbleWidth, height);
//			rect.x = point.x;
//			
//			rect.y = Screen.height - point.y; // bottom left corner set to the 3D point
//			//			Debug.Log("point.y:" + point.y.ToString() + "  rect.y:" + rect.y.ToString());
//			//			GUI.Label(rect, target.name); // display its name, or other string
//			GUI.Box (rect, speechBubbleText, speechStyle);
//		}
//	}


}

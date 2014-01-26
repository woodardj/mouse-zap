using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SparkScript : MonoBehaviour {

	public float movementSpeed = 0.3f;

	public Vector3 speechBubbleOffset = new Vector3(0f, 0f, 0f);
	public float speechBubbleWidth = 120f;
	public GUIStyle speechStyle;

	private List<GameObject> _collidedWires; // keeps a list of all wire gameObjects that are currently collided with the spark.

	private bool showSpeechBubble;
	private string speechBubbleText;


	// Use this for initialization
	void Start () {
		audio.Stop();
		_collidedWires = new List<GameObject> ();
		this.renderer.enabled = false; // start hidden, animation will reveal the spark.
//		animation["SpawnSpark"].wrapMode = WrapMode.Once;
//		animation.Play("SpawnSpark");

	}

	void OnTriggerEnter (Collider other){
//		Debug.Log("OnTriggerEnter!");

		if (other.gameObject.CompareTag("Wire") == true) {
			// store the collided wire
			_collidedWires.Add(other.gameObject);
		}

		if (Time.timeSinceLevelLoad > 1.0) {  // This makes sure these don't trigger immediately on level load.
			// Triggers when touching objects.
			if (other.gameObject.CompareTag("Light") == true) {
				// turn on light if touching object
				GameManager.instance.ActivateLight(other.gameObject);
	//			Activatable objectScript other.gameObject.GetComponent<Activatable>();
			} else if (other.gameObject.CompareTag("FanOutlet") == true) {
				// set the starting position of the spark
	//			Debug.Log ("transform.position:" + transform.position.ToString ());
				GameManager.sparkPosition = transform.position;
				GameManager.instance.ActivateFan();
			} else if (other.gameObject.CompareTag("CanOpener") == true) {
				// set the starting position of the spark
				GameManager.sparkPosition = transform.position;
				GameManager.instance.ActivateCanOpener();
			} else if (other.gameObject.CompareTag("LightKitchen") == true) {
				// set the starting position of the spark
				GameManager.sparkPosition = transform.position;
				GameManager.instance.ActivateKitchenLight();
			} 	
		}

	}
//	void OnTriggerStay (Collider other) {
//
//	}

	void OnTriggerExit (Collider other){
//		Debug.Log("OnTriggerExit!");

		if (other.gameObject.CompareTag("Wire") == true) {
			// remove the collided object
			_collidedWires.Remove(other.gameObject);
		}


		// Triggers when no longer touching objects.
		if (other.gameObject.CompareTag("Light") == true) {
			// turn off light if leaving
			GameManager.instance.DeactivateLight(other.gameObject);
		} 

		// move camera to next room when passing trigger
		if (other.gameObject.CompareTag("KitchenCameraTrigger") == true) {
			if (transform.localPosition.x < other.transform.localPosition.x) {
				GameManager.instance.MoveCameraToKitchen();
			} else if (transform.localPosition.x > other.transform.localPosition.x) {
				GameManager.instance.MoveCameraToLivingRoom();
			}
		}

		// make sure the spark never leaves all wires.
		if (_collidedWires.Count == 0) {
			// we need to return to this wire
			Debug.Log ("Left all wires!!");
			if (other.gameObject.transform.localScale.x > other.gameObject.transform.localScale.z) {
				// this is a left right wire, reset Z
				Vector3 newPos = this.transform.localPosition;
				newPos.z = other.gameObject.transform.position.z;
				this.transform.localPosition = newPos;
			} else if (other.gameObject.transform.localScale.z > other.gameObject.transform.localScale.x) {
				// this is a up down wire, reset X
				Vector3 newPos = this.transform.localPosition;
				newPos.x = other.gameObject.transform.position.x;
				this.transform.localPosition = newPos;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if (GameManager.instance.freezePlayer == true) {
			// don't allow movement
			Debug.Log("don't allow movement");
			// no sfx 
			audio.Stop();
			return;
		}

		// SPARK MOVEMENT
		float h = Input.GetAxis("Horizontal") * movementSpeed;
		float v = Input.GetAxis("Vertical") * movementSpeed;
		if ((h != 0f) | (v != 0f)) {
			foreach (GameObject wire in _collidedWires) {
				if (wire.transform.gameObject.CompareTag("Wire") == true) {
					// hit a wire. // decide if we can move left or right on this wire
					if (wire.transform.localScale.x > wire.transform.localScale.z) {
						// move 
						Vector3 newPosition = this.transform.position + new Vector3(h,0f,0f);
						// clamp position
						if (_collidedWires.Count == 1)
							newPosition.z = wire.transform.position.z;
						newPosition.x = Mathf.Clamp(newPosition.x,
						                            (wire.transform.localPosition.x - (wire.transform.localScale.x * 0.5f)),
						                            (wire.transform.localPosition.x + (wire.transform.localScale.x * 0.5f)));
						this.transform.position = newPosition;
					} else if (wire.transform.localScale.z > wire.transform.localScale.x) {
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
			// play sfx while moving
			if (audio.isPlaying == false)
				audio.Play();
		} else {
			// no sfx 
			audio.Stop();
		}
		
		if (Input.GetButtonDown ("Jump")) {
			// user pressed the activate button
			// check for something to activate


		}
	}


	void sparkHasSpawned () {
		// stop the spawning animation
		gameObject.GetComponent<Animator> ().SetBool ("doneSpawning", true);
		gameObject.renderer.enabled = true;
	}


	// SPEECH BUBBLES
	public void ShowSpeechBubble(Transform talkingTransform, string message, float timeToDisplay) {
		// show the bubble
		speechBubbleText = message;
		showSpeechBubble = true;
		StartCoroutine (HideSpeechBubbleAfterSeconds (timeToDisplay));
	}

	IEnumerator HideSpeechBubbleAfterSeconds(float timeToDisplay) {
		yield return new WaitForSeconds(timeToDisplay);
//		Debug.Log ("Hiding speech bubble");
		//showSpeechBubble = false;
	}
	// OnGUI is called once per frame
	void OnGUI () {
		if (showSpeechBubble == true) {
			Vector3 point = Camera.main.WorldToScreenPoint(transform.position + speechBubbleOffset);
			float height = speechStyle.CalcHeight( new GUIContent(speechBubbleText), speechBubbleWidth);
			Rect rect = new Rect (0, 0f, speechBubbleWidth, height);
			rect.x = point.x;

			rect.y = Screen.height - point.y; // bottom left corner set to the 3D point
//			Debug.Log("point.y:" + point.y.ToString() + "  rect.y:" + rect.y.ToString());
//			GUI.Label(rect, target.name); // display its name, or other string
			GUI.Box (rect, speechBubbleText, speechStyle);
		}
	}


	
}

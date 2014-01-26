using UnityEngine;
using System.Collections;

public class TalkingCanOpenerScript : MonoBehaviour {

	public Vector3 speechBubbleOffset = new Vector3(0f, 0f, 0f);
	public float speechBubbleWidth = 120f;
	public GUIStyle speechStyle;

	private bool showSpeechBubble;
	private string speechBubbleText;

	// Use this for initialization
	void Start () {
		ShowSpeechBubble ("Mwah hah hah hah!", 5.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	
	// SPEECH BUBBLES
	public void ShowSpeechBubble(string message, float timeToDisplay) {
		// show the bubble
		speechBubbleText = message;
		showSpeechBubble = true;
		StartCoroutine (HideSpeechBubbleAfterSeconds (timeToDisplay));
	}
	
	IEnumerator HideSpeechBubbleAfterSeconds(float timeToDisplay) {
		yield return new WaitForSeconds(timeToDisplay);
		//		Debug.Log ("Hiding speech bubble");
		showSpeechBubble = false;
		Application.LoadLevel ("MainMenu");
	}
	// OnGUI is called once per frame
	void OnGUI () {
		if (showSpeechBubble == true) {
			Vector3 point = Camera.main.WorldToScreenPoint(transform.position + speechBubbleOffset);
			float height = speechStyle.CalcHeight( new GUIContent(speechBubbleText), speechBubbleWidth);
			Rect rect = new Rect (0f, 0f, speechBubbleWidth, height);
			rect.x = point.x;
			
			rect.y = Screen.height - point.y; // bottom left corner set to the 3D point
			//			Debug.Log("point.y:" + point.y.ToString() + "  rect.y:" + rect.y.ToString());
			//			GUI.Label(rect, target.name); // display its name, or other string
			GUI.Box (rect, speechBubbleText, speechStyle);
		}
	}


}

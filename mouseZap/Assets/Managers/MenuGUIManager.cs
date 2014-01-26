using UnityEngine;
using System.Collections;

public class MenuGUIManager : MonoBehaviour {

	public Texture2D startButtonTexture;

	// Use this for initialization
	void Start () {
	
	}
	
	// OnGUI is called once per frame
	void OnGUI () {
		if (GUI.Button (new Rect (Screen.width * 0.5f, Screen.height * 0.5f, 80, 40), "Start")) {
//			Debug.Log("Button pressed.");
			// Fade to White then load the GameScene
			CameraFade.StartAlphaFade( Color.white, false, 1.5f, 0f, () => { Application.LoadLevel("LightedLiving");} );
		}
	}


}

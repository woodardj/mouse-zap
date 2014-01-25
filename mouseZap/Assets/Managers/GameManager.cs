using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject floor1;




	// SINGLETON CODE
	// s_Instance is used to cache the instance found in the scene so we don't have to look it up every time.
	private static GameManager s_Instance = null;
	
	// This defines a static instance property that attempts to find the manager object in the scene and
	// returns it to the caller.
	public static GameManager instance {
		get {
			if (s_Instance == null) {
				// This is where the magic happens.
				//  FindObjectOfType(...) returns the first BeingsData object in the scene.
				s_Instance =  FindObjectOfType(typeof (GameManager)) as GameManager;
			}
			
			// If it is still null, create a new instance
			if (s_Instance == null) {
				GameObject obj = new GameObject("GameManager");
				s_Instance = obj.AddComponent(typeof (GameManager)) as GameManager;
				Debug.Log ("Could not locate a BeingsData object. BeingsData was Generated Automaticaly.");
			}
			
			return s_Instance;
		}
	}
	
	// Ensure that the instance is destroyed when the game is stopped in the editor.
	void OnApplicationQuit() {
		s_Instance = null;
	}
	// END OF SINGLETON CODE


	// Use this for initialization
	void Start () {
		// fade the floor
		floor1.renderer.material.color = new Color (0f, 0f, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void ActivateLight(GameObject light) {
		// called by the spark when it collides with a light
		// turning on the light should show the background texture
//		Debug.Log ("Activating light!");
		floor1.transform.renderer.material.color = new Color (1.0f, 1.0f, 1.0f, 1.0f);

	}

	public void DeactivateLight(GameObject light) {
		// called by the spark when it collides with a light
		// turning on the light should show the background texture
//		Debug.Log ("Deactivating light!");
		floor1.transform.renderer.material.color = new Color (0f, 0f, 0f, 0f);
	}

}

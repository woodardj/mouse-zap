using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject floor1;
	public GameObject brokenWire1;
	public GameObject spark;

	public bool freezePlayer;


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
		// fade the floor, so it can be revealed later.
		floor1.renderer.material.color = new Color (0f, 0f, 0f, 0f);

		StartRoom1 ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void StartRoom1 () {
		// animate stuff before the player has control
		// move the mouse
//		Debug.Log ("StartingRoom1");
		brokenWire1.GetComponent<Animator>().SetBool ("wireBreaking", true);

		spark.GetComponent<Animator> ().SetBool ("spawning", true);

//		animation.Play("StartRoom1", PlayMode.StopAll);
		// flash and remove part of a wire

		// lights go out

		// reveal spark

		// player has control
		freezePlayer = false;
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





//	// ========== Here is code to fade and load Game Scenes
//
//	/*
//    Usage:
// 
//    // Load my level    
//    LevelLoadFade.FadeAndLoadLevel("mylevel", Color.white, 0.5);
// 
//    // Reset the current level
//    LevelLoadFade.FadeAndLoadLevel(Application.loadedLevel, Color.white, 0.5);
//*/
//	
//	static void FadeAndLoadLevel (Texture2D level, Texture2D fadeTexture, float fadeLength)
//	{
//		if (fadeTexture == null)
//			FadeAndLoadLevel(level, Color.white, fadeLength);
//		
//		GameObject fade = new GameObject ("Fade");
//		fade.AddComponent(LevelLoadFade);
//		fade.AddComponent(GUITexture);
//		fade.transform.position = Vector3 (0.5, 0.5, 1000);
//		fade.guiTexture.texture = fadeTexture;
//		fade.GetComponent(LevelLoadFade).DoFade(level, fadeLength, false);
//	}
//	
//	static void FadeAndLoadLevel (Color level, Color color, float fadeLength)
//	{
//		var fadeTexture = new Texture2D (1, 1);
//		fadeTexture.SetPixel(0, 0, color);
//		fadeTexture.Apply();
//		
//		var fade = new GameObject ("Fade");
//		fade.AddComponent(LevelLoadFade);
//		fade.AddComponent(GUITexture);
//		fade.transform.position = Vector3 (0.5, 0.5, 1000);
//		fade.guiTexture.texture = fadeTexture;
//		
//		DontDestroyOnLoad(fadeTexture);
//		fade.GetComponent(LevelLoadFade).DoFade(level, fadeLength, true);
//	}
//	
//	void DoFade (string level, float fadeLength, bool destroyTexture)
//	{
//		// Dont destroy the fade game object during level load
//		DontDestroyOnLoad(gameObject);
//		
//		// Fadeout to start with
//		guiTexture.color.a = 0;
//		
//		// Fade texture in
//		var time = 0.0;
//		while (time < fadeLength)
//		{
//			time += Time.deltaTime;
//			guiTexture.color.a = Mathf.InverseLerp(0.0, fadeLength, time);
//			yield;
//		}
//		guiTexture.color.a = 1;
//		yield;
//		
//		// Complete the fade out (Load a level or reset player position)
//		Application.LoadLevel(level);
//		
//		// Fade texture out
//		time = 0.0;
//		while (time < fadeLength)
//		{
//			time += Time.deltaTime;
//			guiTexture.color.a = Mathf.InverseLerp(fadeLength, 0.0, time);
//			yield;
//		}
//		guiTexture.color.a = 0;
//		yield;
//		
//		Destroy (gameObject);
//		
//		// If we created the texture from code we used DontDestroyOnLoad,
//		// which means we have to clean it up manually to avoid leaks
//		if (destroyTexture)
//			Destroy (guiTexture.texture);
//	}
//
//	// ====== END OF CODE TO fade game scenes



}

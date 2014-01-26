﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject floorLivingRoom;
	public GameObject floorKitchen;
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
		floorLivingRoom.renderer.material.color = new Color (0f, 0f, 0f, 0f);
		floorKitchen.renderer.material.color = new Color (0f, 0f, 0f, 0f);

		StartRoom1 ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void StartRoom1 () {
		// animate stuff before the player has control
		// move the mouse


//		Debug.Log ("StartingRoom1");


		// flash and remove part of a wire
		brokenWire1.GetComponent<Animator>().SetBool ("wireBreaking", true);
//		floor1.GetComponent<Animator> ().SetBool ("wireBreaking", true);
		spark.GetComponent<Animator> ().SetBool ("spawning", true);
		
		// lights go out
		floorLivingRoom.renderer.material.color = new Color (0f, 0f, 0f, 1f);

		// reveal spark

		// player has control
		freezePlayer = false;
	}

	public void ActivateLight(GameObject light) {
		// called by the spark when it collides with a light
		// turning on the light should show the background texture
//		Debug.Log ("Activating light!");
		floorLivingRoom.transform.renderer.material.color = new Color (1.0f, 1.0f, 1.0f, 1.0f);

	}

	public void DeactivateLight(GameObject light) {
		// called by the spark when it collides with a light
		// turning on the light should show the background texture
//		Debug.Log ("Deactivating light!");
		floorLivingRoom.transform.renderer.material.color = new Color (0f, 0f, 0f, 0f);
	}

	public void ActivateFan() {
		// turn on all wind particle renderers
		Application.LoadLevel("FanScene");
	}

	public void DeactivateFan() {
		// turn off all wind particle renderers
	}


	public void MoveCameraToKitchen () {
		Vector3 newCameraPosition = floorKitchen.transform.position + (Vector3.up * 20f);
//		Debug.Log ("MoveCameraToKitchen. new pos:" + newCameraPosition.ToString ());
		StartCoroutine(MoveObject(Camera.main.transform, Camera.main.transform.position, newCameraPosition, 1.8f));
	}

	public void MoveCameraToLivingRoom () {
		Vector3 newCameraPosition = floorLivingRoom.transform.position + (Vector3.up * 20f);
//		Debug.Log ("MoveCameraToLivingRoom. new pos:" + newCameraPosition.ToString ());
		StartCoroutine(MoveObject(Camera.main.transform, Camera.main.transform.position, newCameraPosition, 1.8f));
	}

	// Call the MoveObject coroutine like this:
	//  StartCoroutine(MoveObject(Camera.main.transform, pointA, pointB, 3.0f));
	IEnumerator MoveObject (Transform thisTransform, Vector3 startPos, Vector3 endPos, float time) {
		float i = 0.0f;
		float rate = 1.0f / time;
		while (i < 1.0f) {
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp(startPos, endPos, i);
			yield return null; 
		}
	}



}

using UnityEngine;
using System.Collections;

public class WiresKitchenManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log ("KitchenWires Start");
		GameManager.instance.StartKitchenWires ();

		// move camera to next room when passing trigger
		GameObject trigger = GameObject.FindGameObjectWithTag("KitchenCameraTrigger");
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (player.transform.localPosition.x < trigger.transform.localPosition.x) {
			GameManager.instance.PlaceCameraInKitchen();
		} else if (player.transform.localPosition.x > trigger.transform.localPosition.x) {
			GameManager.instance.PlaceCameraInLivingRoom();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

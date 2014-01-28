using UnityEngine;
using System.Collections;

public class MouseAnimationManager : MonoBehaviour {
	public AudioClip Squeek;
	public AudioClip mouseChewing;
	public AudioClip Fan;

	void NextScene(){
		Application.LoadLevel ("CircuitLivingScene");
	}

	void PlayChewingSoundEffect () {
		audio.Stop (); // stop the scurry sound effect
		audio.PlayOneShot(mouseChewing); // play the chewing sound
	}


}

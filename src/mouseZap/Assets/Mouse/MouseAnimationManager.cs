using UnityEngine;
using System.Collections;

public class MouseAnimationManager : MonoBehaviour {
	public AudioClip Squeek;
	public AudioClip Fan;

	void NextScene(){
		Application.LoadLevel ("CircuitLivingScene");
	}


}

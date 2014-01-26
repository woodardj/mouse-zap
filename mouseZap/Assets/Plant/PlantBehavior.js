#pragma strict

var hitPoints = 100;
var damageThreshold = 30; //Determined empirically.

private var alive;
private var firstBlood;
//private var time_since_birth;

function Start () {
	alive = true;
	firstBlood = true;
	//time_since_birth = 0.0;
	StartCoroutine("Hint1");
}

function Update () {
 	//Debug.Log("updating");
 	//if(Input.GetKey("space")){
 	//}
 	//time_since_birth += Time.deltaTime;
 	
 	//if(time_since_birth > 30.0 && firstBlood = false){
 	//	(GameObject.Find("Blades").GetComponent("FanControl") as FanControl).TalkEncourage();
 	//}
}

function Hint1(){
	Debug.Log("hint1 yielding");
	yield WaitForSeconds(30);
	if(firstBlood){
		(GameObject.Find("Blades").GetComponent("FanControl") as FanControl).TalkHint1();
	}
	StartCoroutine("Hint2");
}

function Hint2(){
	Debug.Log("hint2 yielding");
	yield WaitForSeconds(20);
	if(firstBlood){
		(GameObject.Find("Blades").GetComponent("FanControl") as FanControl).TalkHint2();
	}
	//StartCoroutine("Hint2");
}

function OnCollisionEnter(collision: Collision){
	if(collision.relativeVelocity.magnitude > damageThreshold){
		hitPoints -= (collision.relativeVelocity.magnitude / 2);
		transform.Rotate(Vector3(0, -5, 0));

		//Debug.Log('hit!');
		Debug.Log(hitPoints);
		if( firstBlood ){
			firstBlood = false;
			//Debug.Log(GameObject.Find("Blades").GetComponent("FanControl"));
			//fanScript.Talk1();
			(GameObject.Find("Blades").GetComponent("FanControl") as FanControl).TalkEncourage();
		}
	} else {
		transform.Rotate(Random.insideUnitSphere * .5); // Twitch so they know what's happening.
	}
	
	
	
	if(hitPoints <= 0){
		Debug.Log("WINNER");
		death();
	}
}

function death(){
	if(alive){
		alive = false;
		//rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		//gameObject.collider.active = false;
		animation.wrapMode = WrapMode.Once;
		animation.Play("Death");
	}
}

function scareMouse(){
	(GameObject.Find("Blades").GetComponent("FanControl") as FanControl).TalkSuccess();
	GameObject.Find("Mouse").animation.Play("MouseEscape");
}

function nextLevel(){
	Application.LoadLevel(4);
}
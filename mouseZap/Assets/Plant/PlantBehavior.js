#pragma strict

var hitPoints = 100;
var damageThreshold = 30; //Determined empirically.

private var alive;

function Start () {
	alive = true;
}

function Update () {
 	//Debug.Log("updating");
 	//if(Input.GetKey("space")){
 	//}
}

function OnCollisionEnter(collision: Collision){
	if(collision.relativeVelocity.magnitude > damageThreshold){
		hitPoints -= (collision.relativeVelocity.magnitude / 2);
		transform.Rotate(Vector3(0, -5, 0));

		Debug.Log('hit!');
		Debug.Log(hitPoints);
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

function nextLevel(){
	Application.LoadLevel("CircuitKitchenScene");
}
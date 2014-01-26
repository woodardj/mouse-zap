#pragma strict

var hitPoints = 100;

function Start () {

}

function Update () {
 	Debug.Log("updating");
}

function onCollisionEnter(collision: Collision){
	Debug.Log(collision.relativeVelocity.magnitude);
}
#pragma strict

var strength = 0;

function Start () {
	Debug.Log("hi");
}

function Update () {
	if (Input.GetKey("up")) {
		strength = Mathf.Clamp( strength + 1 , 0, 100 );
	}else if(Input.GetKey("down")) {
		strength = Mathf.Clamp( strength - 1 , 0, 100 );
	}
	
	if (Input.GetKey("left")) {
		//this.hingeJoint.angle = this.hingeJoint.angle;
	}
	Debug.Log(strength);
}
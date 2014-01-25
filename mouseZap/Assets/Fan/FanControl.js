
var rotate_speed = 10;
var strength = 5;

function Start () {
	Debug.Log("hi");
}

function Update () {
	if (Input.GetKey("up")) {
		strength = Mathf.Clamp( strength + 1 , 0, 100 );
	}else if(Input.GetKey("down")) {
		strength = Mathf.Clamp( strength - 1 , 0, 100 );
	}
	
	if (Input.GetKey("left") || Input.GetKey("right")) {
		if (Input.GetKey("left")) var dir = -1; else dir = 1;
		transform.Rotate( dir * rotate_speed * Vector3.up * Time.deltaTime);
	}
}

public function getAngle(){
	//this will change axes if we use an upright cube instead of a cylinder.
	return transform.eulerAngles.y;
}

public function getDirection(){
	return transform.forward;
}

public function makeWind(){
	return transform.forward * strength;
}
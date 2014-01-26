
var rotate_speed = 10;
var strength = 5;
var airparticle : GameObject;
var spawn_delay = .333;

private var time_since_spawn;

function Start () {
	Debug.Log("hi");
	time_since_spawn = 0;
}

function Update () {
Debug.Log('updating');
	if (Input.GetKey("up")) {
		strength = Mathf.Clamp( strength + 1 , 0, 100 );
	}else if(Input.GetKey("down")) {
		strength = Mathf.Clamp( strength - 1 , 0, 100 );
	}
	
	if (Input.GetKey("left") || Input.GetKey("right")) {
		if (Input.GetKey("left")) var dir = -1; else dir = 1;
		transform.Rotate( dir * rotate_speed * Vector3.up * Time.deltaTime);
	}
	
	time_since_spawn += Time.deltaTime;
	
	if((time_since_spawn > spawn_delay) && (strength > 0)){
		//Every spawn_delay seconds, generate a new air particle to make up for the loss from the fireplace.
		pos = transform.position + Random.insideUnitSphere;
		pos.y = 1;
		Instantiate(airparticle, pos, Quaternion.identity);
		time_since_spawn = 0;
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
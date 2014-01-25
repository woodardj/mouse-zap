
function Start(){
	Debug.Log("ahoy!");
}

function blowinInTheWind (other : Collider) {
	//Don't hang on.
	if (other.CompareTag("Fan") == true){
		var blower = other.gameObject.GetComponent("FanControl").makeWind();
		rigidbody.AddForce(blower);
	}	
};

//OnTriggerEnter = blowinInTheWind;
//OnTriggerStay = blowinInTheWind;

function OnTriggerEnter(other : Collider) {
	blowinInTheWind(other);
//	if (other.CompareTag("Fan") == true){
//		var blower = other.gameObject.GetComponent("FanControl").makeWind();
//		rigidbody.AddForce(blower);
//	}
}

function OnTriggerStay(other : Collider) {
	blowinInTheWind(other);
}

function Start(){
	//Debug.Log("ahoy!");
}

function blowinInTheWind (other : Collider) {
	//Don't hang on.
	if (other.CompareTag("Fan") == true){
		var blower = other.gameObject.GetComponent("FanControl").makeWind();
		rigidbody.AddForce(blower);
	}else if (other.CompareTag("Drafty") == true) {
		if(other.gameObject.GetComponent("DraftBehavior").particleEscapes()){
			UnityEngine.Object.Destroy(gameObject);
		}
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
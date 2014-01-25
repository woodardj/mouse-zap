var airparticle : GameObject; // A little self-referential nonsense for bootstrapping

function Start(){
	bootstrapAir();	
}

function OnTriggerEnter(other : Collider) {
	Debug.Log(other.gameObject.CompareTag("Fan"));
	if (other.CompareTag("Fan") == true){
		var blower = other.gameObject.GetComponent("FanControl").makeWind();
		rigidbody.AddForce(blower);
	}
}

function bootstrapAir(){
	for(var i = 0 ; i < 5 ; i++){
		var pos = Vector3(i, 0, i);
		Instantiate(airparticle, pos, Quaternion.identity);
	}
}
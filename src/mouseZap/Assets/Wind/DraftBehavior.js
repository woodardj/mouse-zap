#pragma strict

var airparticle : GameObject;
var outflow_rate : float = 0.5;
var inflow_rate : float = 0.0;

function Start () {

}

function Update () {
	if(airparticle.name != null && inflow_rate > Random.Range(0.0,1.0)){
		//TODO move this into the room, instead of dead-center.
		var pos = transform.position;
		Instantiate(airparticle, pos, Quaternion.identity);
	}
}

function particleEscapes(){
	var rand = Random.Range(0.0,1.0);
	return (outflow_rate > rand);
}
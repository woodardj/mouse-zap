#pragma strict

var airparticle : GameObject;
var roomXSize : float;
var roomZSize : float;
var density : float = 1.0;

function Start(){
	Debug.Log('start!');
	bootstrapAir();	
}

function Update () {

}

function bootstrapAir(){
	Debug.Log('boot strizzap');
	var pos = Vector3(0, 1, 0);
	for(var x = -1 * roomXSize; x <= roomXSize ; x += density){
		for(var z  =  -1 * roomZSize; z <= roomZSize ; z += density){
			pos = Vector3(x, 1, z);
			Instantiate(airparticle, pos, Quaternion.identity);
		}
	}
}
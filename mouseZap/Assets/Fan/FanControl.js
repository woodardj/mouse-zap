﻿
var rotate_speed = 20;
var strength = 5;
var airparticle : GameObject;
var spawn_delay = .333;

var speechBubbleOffset : Vector3 = Vector3(0f, 0f, 0f);
var speechBubbleWidth : float = 120f ;
var speechStyle : GUIStyle;

private var speechBubbleText;

private var time_since_spawn;
private var fansprite;
private var rotation_bounds = [-0.75, 0.17];

private var showSpeechBubble;

function Start () {
	//Debug.Log("hi");
	time_since_spawn = 0;
	fansprite = transform.Find("FanSprite").GetComponent("SpriteAnimation");//GetComponentsInChildren.GetComponent("FanControl")
	//Debug.Log(fansprite.fps);
	showSpeechBubble = true;
}

function Update () {
	if (Input.GetKey("up")) {
		strength = Mathf.Clamp( strength + 1 , 0, 100 );
	} else if(Input.GetKey("down")) {
		strength = Mathf.Clamp( strength - 1 , 0, 100 );
	}
	
	if (strength == 0){
		fansprite.fps = 0;
	} else {
		fansprite.fps = Mathf.CeilToInt(strength / 10) + 5;
	}
	
	if (Input.GetKey("left") || Input.GetKey("right")) {
		if (Input.GetKey("left")) var dir = -1; else dir = 1;
		if( //This got ugly. Sorry for partying. --jw
			(transform.rotation.y > rotation_bounds[0] || dir == 1) && //inside left bound or turning right
			(transform.rotation.y < rotation_bounds[1] || dir == -1))  //inside right bound or turning left
		{
			transform.Rotate( dir * rotate_speed * Vector3.up * Time.deltaTime);
		}
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


function OnGUI () {
	if (showSpeechBubble == true) {
	speechBubbleText = "Hi there";
		var point = Camera.main.WorldToScreenPoint(transform.position + speechBubbleOffset);
		var height = 50; //speechStyle.CalcHeight( GUIContent(speechBubbleText), speechBubbleWidth);
		var rect = Rect (0f, 0f, speechBubbleWidth, height);
		rect.x = point.x;

		rect.y = Screen.height - point.y; // bottom left corner set to the 3D point
//			Debug.Log("point.y:" + point.y.ToString() + "  rect.y:" + rect.y.ToString());
//			GUI.Label(rect, target.name); // display its name, or other string
		GUI.Box (rect, speechBubbleText, speechStyle);
	}
}


//Getters, mostly deprecated
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
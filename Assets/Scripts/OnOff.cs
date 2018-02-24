using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class OnOff : MonoBehaviour {

	protected bool turnOn;
	protected bool playerCol;
	protected SpriteRenderer aSwitch;
	protected Color color1;
	protected Color color2;

	// Use this for initialization
	void Start () {
		turnOn = false;
		playerCol = false;
		aSwitch = GetComponent<SpriteRenderer>();
		color1 = aSwitch.material.GetColor("_EmissionColor");
		color2 = Color.blue;
		begin ();
	}

	protected void OnTriggerStay2D (Collider2D col){
		if (col.gameObject.tag == "Player") {
			playerCol = true;
		}
	}
		
	protected void OnTriggerExit2D (Collider2D col){
		if (col.gameObject.tag == "Player") {
			playerCol = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(playerCol && Input.GetKeyDown("m")) {
			turnOn = !turnOn;
			changeVisual (turnOn);
			action (turnOn);
		}
	}
		
	protected void changeVisual(bool turnOn) {
		if(turnOn) {
			aSwitch.material.SetColor ("_EmissionColor", color2);
		} else {
			aSwitch.material.SetColor ("_EmissionColor", color1);
		}
	}

	protected abstract void action (bool turnOn);
	protected abstract void begin ();
}

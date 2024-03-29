﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntegratedOnOff : OnOff {

	// same determines if the two switches should always have the same or opposite states.
	public OnOff other;
	public bool same;

	protected override void action (bool turnOn) {
		if (same) {
			other.setOnBool (turnOn);
		} else {
			other.setOnBool (!turnOn);
		}
	}

	protected override void begin () { }

	void Update () {
		// If the player is on the object and toggles it, turn the object On/Off.
		if((cCol && (Input.GetKeyDown("j") || Input.GetMouseButtonDown(0))) || (rCol && (Input.GetKeyDown("l") || Input.GetMouseButtonDown(1)))) {
			setOnBool(!turnOn);
		}
		if (same) {
			if(other.getOnBool() != this.turnOn) {
				this.setOnBool(!turnOn);
			}
		} else {
			if(other.getOnBool() == this.turnOn) {
				this.setOnBool(!turnOn);
			}
		}
	}
}

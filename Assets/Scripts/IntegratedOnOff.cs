using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntegratedOnOff : OnOff {

	public OnOff other;

	protected override void action (bool turnOn) {
		other.setOnBool (turnOn);
	}

	protected override void begin () { }

	void Update () {
		// If the player is on the object and toggles it, turn the object On/Off.
		if((cCol && Input.GetKeyDown("j")) || (rCol && Input.GetKeyDown("l"))) {
			setOnBool(!turnOn);
		}
		if(other.getOnBool() != this.turnOn) {
			this.setOnBool(!turnOn);
		}
	}
}

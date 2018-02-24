using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSwitch : OnOff {
	
	public GameObject platform;

	protected override void begin () {
		platform.SetActive (false);
	}

	protected override void action(bool turnOn) {
		if(turnOn) {
			platform.SetActive (true);
		} else {
			platform.SetActive (false);
		}
	}
}

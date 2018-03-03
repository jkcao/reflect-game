using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformSwitch : OnOff {

	public GameObject platform;
	public bool isActiveOnStart;
	private MovingPlatform movingPlatform;

	protected override void begin () {
		movingPlatform = platform.GetComponent<MovingPlatform> ();
		movingPlatform.isActive = isActiveOnStart;
	}

	protected override void action(bool turnOn) {
		if(turnOn) {
			movingPlatform.isActive = isActiveOnStart;
		} else {
			movingPlatform.isActive = !isActiveOnStart;
		}
	}
}

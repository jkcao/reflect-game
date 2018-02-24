using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSwitch : OnOff {
	
	private GameObject[] platforms;
	public GameObject platform1;
	public GameObject platform2;
	public GameObject platform3;

	// Deactivates platforms until switched on.
	protected override void begin () {
		platforms = new GameObject[]{platform1, platform2, platform3};
		for(int i = 0; i < 3; i++) {
			if (platforms [i] != null) platforms [i].SetActive (false);
		}
	}

	// Toggles platforms.
	protected override void action(bool turnOn) {
		if(turnOn) {
			for(int i = 0; i < 3; i++) {
				if (platforms[i] != null) platforms[i].SetActive (true);
			}
		} else {
			for(int i = 0; i < 3; i++) {
				if (platforms[i] != null) platforms[i].SetActive (false);
			}
		}
	}
}

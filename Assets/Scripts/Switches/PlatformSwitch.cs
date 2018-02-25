using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSwitch : OnOff {
	
	private GameObject[] platforms;
	public GameObject platform1;
	public GameObject platform2;
	public GameObject platform3;
	public GameObject platform4;
	// The number of platforms that will be toggled together, with the rest in a different set.
	public int toggleSet;

	// Deactivates platforms until switched on.
	protected override void begin () {
		platforms = new GameObject[]{platform1, platform2, platform3, platform4};
		for(int i = 0; i < 4; i++) {
			if (platforms [i] != null) {
				if (i < toggleSet) {
					platforms [i].SetActive (false);
				} else {
					platforms [i].SetActive (true);
				}
			}
		}
	}

	// Toggles platforms.
	protected override void action(bool turnOn) {


		if(turnOn) {
			for(int i = 0; i < 4; i++) {
				if (platforms [i] != null) {
					if (i < toggleSet) {
						platforms [i].SetActive (true);
					} else {
						platforms [i].SetActive (false);
					}
				}
			}
		} else {
			for(int i = 0; i < 4; i++) {
				if (platforms [i] != null) {
					if (i < toggleSet) {
						platforms [i].SetActive (false);
					} else {
						platforms [i].SetActive (true);
					}
				}
			}
		}
	}
}

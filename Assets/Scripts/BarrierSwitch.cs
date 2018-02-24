using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierSwitch : OnOff {

	public GameObject barrier;

	protected override void begin () {}

	protected override void action(bool turnOn) {
		if(turnOn) {
			barrier.SetActive (false);
		} else {
			barrier.SetActive (true);
		}
	}
}

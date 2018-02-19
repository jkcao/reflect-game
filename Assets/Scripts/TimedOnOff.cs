using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedOnOff : MonoBehaviour {

	// Generalize to if we want a timer for how long something is on or how long it's off.
	public bool timeOn;
	public int timer;

	IEnumerator Time() {
		yield return new WaitForSeconds(timer);
		this.GetComponent<OnOff> ().setOnBool (!timeOn);
	}

	// Update is called once per frame
	void Update () {
		if (this.GetComponent<OnOff> ().getOnBool () == timeOn) {
			StartCoroutine (Time());
		}

	}
}

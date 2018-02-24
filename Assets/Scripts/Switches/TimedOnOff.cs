using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedOnOff : MonoBehaviour {

	// Generalize to if we want a timer for how long something is on or how long it's off.
	public bool timeOn;
	public float timer;

	// Run a timer, which resets if the button gets turned off before its through.
	IEnumerator Time() {
		float timeLeft = timer;
		while (timeLeft > 0) {
			if (this.GetComponent<OnOff> ().getOnBool () != timeOn)
				break;
			yield return new WaitForSeconds (1);
			timeLeft--;
		}
		if (timeLeft == 0) this.GetComponent<OnOff> ().setOnBool (!timeOn);
		StopAllCoroutines ();
	}

	// Update is called once per frame
	void Update () {
		if (this.GetComponent<OnOff> ().getOnBool () == timeOn) {
			StartCoroutine (Time());
		}
	}
}

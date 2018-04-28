using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour {

	IEnumerator toMenu() {
		float fadeTime = this.GetComponent<FadeIn> ().BeginFade (1);
		yield return new WaitForSeconds(fadeTime);
		SceneManager.LoadScene ("MainMenu");
	}

	// Update is called once per frame
	void Update () {
		if(Input.anyKeyDown) StartCoroutine (toMenu());
	}
}

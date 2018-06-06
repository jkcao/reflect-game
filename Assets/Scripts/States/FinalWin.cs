using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalWin : MonoBehaviour {

	IEnumerator Finish() {
		float fadeTime = this.GetComponent<FadeIn> ().BeginFade (1);
		yield return new WaitForSeconds(fadeTime);
		SceneManager.LoadScene ("Finish");
	}
	
	private void OnTriggerEnter2D (Collider2D col){
		if (col.gameObject.tag == "Character" || col.gameObject.tag == "Reflection") {
			this.GetComponent<AudioSource> ().Play ();
			StartCoroutine (Finish());
		}
	}
}

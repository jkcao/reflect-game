using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour {

	public string nextScene;

	private bool playingSound;

	IEnumerator Next() {
		yield return new WaitForSeconds(0.5f);
		playingSound = false;
		SceneManager.LoadScene (nextScene);
	}

	private void OnTriggerEnter2D (Collider2D col){
		if (col.gameObject.tag == "Character" || col.gameObject.tag == "Reflection") {
			if (!playingSound) {
				playingSound = true;
				this.GetComponent<AudioSource> ().Play ();
			}
			StartCoroutine (Next());
		}
	}
}

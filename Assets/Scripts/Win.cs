using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour {

	public string nextScene;

	IEnumerator Next() {
		yield return new WaitForSeconds(0.5f);
		SceneManager.LoadScene (nextScene);
	}

	private void OnTriggerEnter2D (Collider2D col){
		if (col.gameObject.tag == "Character" || col.gameObject.tag == "Reflection") {
			this.GetComponent<AudioSource> ().Play ();
			StartCoroutine (Next());
		}
	}
}

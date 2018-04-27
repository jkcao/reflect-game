using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour {

  private bool touchingChar;
  private bool audioPlaying = true;
	private Player reflection;

  IEnumerator Destroy() {
	yield return new WaitForSeconds(0.5f);
		Destroy(this.GetComponent<BoxCollider2D>());
		Destroy(this.GetComponent<SpriteRenderer>());
  }

	IEnumerator AudioTimer() {
		yield return new WaitForSeconds(0.4f);
		audioPlaying = false;
		reflection.setSpecAbil (false);
		reflection.setCanMove (true);
		reflection.mirror.setCanMove (true);
	}

  private void Update()
  {
	if (Input.GetKeyDown("l") || Input.GetMouseButtonDown(1))
    {
		if (touchingChar)
		{
			this.GetComponent<AudioSource> ().Play ();
				if (reflection != null) {
					reflection.setSpecAbil (true);
					reflection.setCanMove (false);
					reflection.mirror.setCanMove (false);
				}
				StartCoroutine (Destroy ());
				StartCoroutine (AudioTimer ());
		}
    }
		if (!audioPlaying) {
			Destroy(gameObject);
		}
  }



  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.tag == "Reflection")
    {
      touchingChar = true;
	  reflection = other.gameObject.GetComponent<Player> ();
    }
  }

  private void OnCollisionExit2D(Collision2D other)
  {
    if (other.gameObject.tag == "Reflection")
    {
      touchingChar = false;
    }
  }
}
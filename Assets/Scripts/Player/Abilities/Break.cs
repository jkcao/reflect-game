using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour {

  private bool touchingChar;
  private bool audioPlaying = true;

  IEnumerator Destroy() {
	yield return new WaitForSeconds(0.15f);
		Destroy(this.GetComponent<BoxCollider2D>());
		Destroy(this.GetComponent<SpriteRenderer>());
  }

	IEnumerator AudioTimer() {
		yield return new WaitForSeconds(0.4f);
		audioPlaying = false;
	}

  private void Update()
  {
	if (Input.GetKeyDown("l") || Input.GetMouseButtonDown(1))
    {
		if (touchingChar)
		{
			this.GetComponent<AudioSource> ().Play ();
				StartCoroutine (Destroy ());
				StartCoroutine (AudioTimer ());
		}
    }
		if (!audioPlaying) {
			Destroy(gameObject);
		}
  }



  private void OnCollisionStay2D(Collision2D other)
  {
    if (other.gameObject.tag == "Reflection")
    {
      touchingChar = true;
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
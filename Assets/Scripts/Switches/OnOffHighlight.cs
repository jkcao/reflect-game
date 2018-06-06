using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffHighlight : MonoBehaviour {

	private OnOff switchObj;

	void Start () {
		switchObj = this.transform.parent.gameObject.GetComponent<OnOff> ();
	}

	protected void OnTriggerStay2D(Collider2D col) {
		if (!switchObj.getOnBool ()) {
			switchObj.switchVisual.sprite = switchObj.onHighlightSprite;
		} else {
			switchObj.switchVisual.sprite = switchObj.offHighlightSprite;
		}
	}
	
	protected void OnTriggerExit2D(Collider2D col) {
		if (!switchObj.getOnBool()) {
			switchObj.switchVisual.sprite = switchObj.onSprite;
		} else {
			switchObj.switchVisual.sprite = switchObj.offSprite;
		}
	}
}
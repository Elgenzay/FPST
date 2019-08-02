using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

	private float reactivate_timeStamp;
	private int rotateProgress;
	private bool isGreen;

	public void Hit(){
		if (!isGreen && rotateProgress == 0) {
			rotateProgress = 9;
			reactivate_timeStamp = Time.time + 3f;
			isGreen = true;
		}
	}

	// Use this for initialization
	void Start () {
		reactivate_timeStamp = 0f;
		rotateProgress = 0;
		isGreen = false;
	}

	void FixedUpdate () {
		if (rotateProgress != 0) {
			rotateProgress -= 1;
			if (isGreen) {
				this.transform.Rotate (20f, 0f, 0f);
			} else {
				this.transform.Rotate (-5f, 0f, 0f);
			}
		} else if (isGreen && reactivate_timeStamp < Time.time) {
			isGreen = false;
			rotateProgress = 36;
		}
	}
}

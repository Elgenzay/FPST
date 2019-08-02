using UnityEngine;
using System.Collections;

public class BoxTarget : MonoBehaviour {

	public GameObject targetHealthControl;

	private float reactivate_timeStamp;
	private float regen_timeStamp;
	private int rotateProgress;
	private short state; //1:downback 2:downfront 3:upfront 4:upback
	private int maxHealth;
	private int currentHealth;

	private TargetHealthControl thc;

	public void Hit(Vector3 firePosition){
		if (maxHealth != thc.targetHealth) {
			maxHealth = thc.targetHealth;
			currentHealth = thc.targetHealth;
		}
		if ((state == 3 || state == 4) && rotateProgress == 0 && maxHealth != 0) {
			regen_timeStamp = Time.time + 3f;
			currentHealth--;
			if (currentHealth < 1) {
				rotateProgress = 6;
				reactivate_timeStamp = Time.time + 3f;
				if (this.transform.InverseTransformPoint (firePosition).z < 0f) {
					state = 1;
				} else {
					state = 2;
				}
			}
		}
	}

	// Use this for initialization
	void Start () {
		thc = targetHealthControl.GetComponent<TargetHealthControl> ();
		regen_timeStamp = 0f;
		maxHealth = thc.targetHealth;
		currentHealth = thc.targetHealth;
		reactivate_timeStamp = 0f;
		rotateProgress = 0;
		state = 1;
	}

	void FixedUpdate () {
		if ((state == 3 || state == 4) && regen_timeStamp < Time.time && currentHealth < maxHealth) {
			currentHealth++;
			regen_timeStamp = Time.time + 3f;
		}
		if (rotateProgress != 0) {
			rotateProgress -= 1;
			if (state == 1) {
				this.transform.Rotate (15f, 0f, 0f);
			} else if (state == 2) {
				this.transform.Rotate (-15f, 0f, 0f);
			} else if (state == 3) {
				this.transform.Rotate (2.5f, 0f, 0f);
			} else if (state == 4) {
				this.transform.Rotate (-2.5f, 0f, 0f);
			}
		}
		if ((state == 1 || state == 2) && reactivate_timeStamp <= Time.time) {
			if (state == 1) {
				state = 4;
			} else if (state == 2) {
				state = 3;
			}
			rotateProgress = 36;
			currentHealth = thc.targetHealth;
		}
	}
}

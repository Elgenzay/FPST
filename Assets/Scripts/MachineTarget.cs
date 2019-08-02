using UnityEngine;
using System.Collections;

public class MachineTarget : MonoBehaviour {

	public GameObject targetMachine;

	private int rotateProgress;
	private bool isGreen;
	public float deathTime;

	public void Spawn(GameObject tm){
		targetMachine = tm;
		deathTime = Time.time + targetMachine.GetComponent<TargetMachine> ().trueDuration;
		this.transform.localPosition = new Vector3( tm.transform.position.x + Random.Range (0f, targetMachine.GetComponent<TargetMachine> ().screenSize), tm.transform.position.y + Random.Range (-targetMachine.GetComponent<TargetMachine> ().trueHeight, targetMachine.GetComponent<TargetMachine> ().trueHeight), tm.transform.position.z + Random.Range(-4f,4f));
		this.transform.localScale = new Vector3 (targetMachine.GetComponent<TargetMachine> ().trueSize, 0.001f, targetMachine.GetComponent<TargetMachine> ().trueSize);
	}

	public void Hit(){
		if (!isGreen && rotateProgress == 0) {
			rotateProgress = 9;
			isGreen = true;
			targetMachine.GetComponent<TargetMachine> ().IncrementScore ();
			this.transform.localPosition = new Vector3 (this.transform.localPosition.x, this.transform.localPosition.y, targetMachine.transform.localPosition.z - 5f);
		}
	}

	// Use this for initialization
	void Start () {
		rotateProgress = 0;
		isGreen = false;
	}

	void FixedUpdate () {
		if (rotateProgress != 0) {
			rotateProgress -= 1;
			if (isGreen) {
				this.transform.Rotate (20f,0f,0f);
			} else {
				this.transform.Rotate (-5f,0f,0f);
			}
		}
		if (deathTime <= Time.time) {
			if (!isGreen) {
				targetMachine.GetComponent<TargetMachine> ().ResetScore ();
			}
			Destroy (this.gameObject);
		}
	}
}

using UnityEngine;
using System.Collections;

public class THCButton : MonoBehaviour {

	public GameObject healthMachine;
	public Texture unpressed;
	public Texture pressed;
	public short buttonType;

	private TargetHealthControl thcScript;
	private Renderer thisRenderer;
	private float pressedAnimationTimer;

	void Start(){
		thcScript = healthMachine.GetComponent<TargetHealthControl> ();
		thisRenderer = this.GetComponent<Renderer> ();
		pressedAnimationTimer = 0f;
	}

	public void Hit(){
		Press ();
	}

	public void Interact(){
		Press ();
	}

	private void Press(){
		if (buttonType == 1) {
			thcScript.IncrementHealth ();
		} else {
			thcScript.DecrementHealth ();
		}
		pressedAnimationTimer = 10f;
		thisRenderer.material.mainTexture = pressed;
	}

	void FixedUpdate(){
		if (pressedAnimationTimer > 0f) {
			pressedAnimationTimer--;
			if (pressedAnimationTimer <= 0f) {
				thisRenderer.material.mainTexture = unpressed;
			}
		}
	}
}

using UnityEngine;
using System.Collections;

public class TargetMachineButton : MonoBehaviour {

	public GameObject targetMachine;
	public Texture unpressed;
	public Texture pressed;
	public short buttonType;

	private TargetMachine tmScript;
	private Renderer thisRenderer;
	private float pressedAnimationTimer;

	void Start(){
		tmScript = targetMachine.GetComponent<TargetMachine> ();
		pressedAnimationTimer = 0f;
		thisRenderer = this.GetComponent<Renderer> ();
	}

	public void Hit(){
		Press ();
	}

	public void Interact(){
		Press ();
	}

	private void Press(){
		tmScript.ModifyValue (buttonType);
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

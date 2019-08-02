using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {
	
	public void Interact(){
		if (this.gameObject.GetComponent<TargetMachineButton>() != null){
			this.gameObject.GetComponent<TargetMachineButton> ().Interact ();
		}
		else if (this.gameObject.GetComponent<THCButton>() != null){
			this.gameObject.GetComponent<THCButton>().Hit();
		}
	}

}

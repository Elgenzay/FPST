using UnityEngine;
using System.Collections;

public class Carriable : MonoBehaviour {
	
	[HideInInspector]
	public float initialAngularDrag;
	private Rigidbody rb;

	void Start(){
		rb = this.GetComponent<Rigidbody> ();
		initialAngularDrag = rb.angularDrag;
	}

	void Reset(){
		rb.angularDrag = initialAngularDrag;
	}
}

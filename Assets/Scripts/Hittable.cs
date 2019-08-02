using UnityEngine;
using System.Collections;

public class Hittable : MonoBehaviour {

	public void Hit(Vector3 firePosition){
		if (this.gameObject.GetComponent<Target>() != null){
			this.gameObject.GetComponent<Target>().Hit();
		}
		else if (this.gameObject.GetComponent<BoxTarget>() != null){
			this.gameObject.GetComponent<BoxTarget>().Hit(firePosition);
		}
		else if (this.gameObject.GetComponent<MachineTarget>() != null){
			this.gameObject.GetComponent<MachineTarget>().Hit();
		}
		else if (this.gameObject.GetComponent<TargetMachineButton>() != null){
			this.gameObject.GetComponent<TargetMachineButton>().Hit();
		}
		else if (this.gameObject.GetComponent<THCButton>() != null){
			this.gameObject.GetComponent<THCButton>().Hit();
		}
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MouseSensitivity : MonoBehaviour {
	
	private static float onePx = 0.05f;

	public Toggle xyToggle;

	public Slider xSensitivitySlider;
	public InputField xDotsPerFov;
	public InputField xDegsPerDot;
	public InputField xDotsPerDeg;
	public InputField xDotsPer360;
	public InputField xMultiplier;

	public Slider ySensitivitySlider;
	public InputField yDotsPerFov;
	public InputField yDegsPerDot;
	public InputField yDotsPerDeg;
	public InputField yDotsPer360;
	public InputField yMultiplier;

	private FPS_Controller fpsC;
	private bool isXYtoggled;

	void Awake(){
		fpsC = this.GetComponent<FPS_Controller> ();
	}

	void Start(){
		if (xyToggle.isOn) {
			xyToggled (true);
		} else {
			xyToggled (false);
		}
	}

	public void ApplySugSens(){
		xyToggle.isOn = true;
		xUpdateSensitivity(float.Parse (fpsC.sugHor.text));
		yUpdateSensitivity(float.Parse (fpsC.sugVer.text));
	}

	public void xyToggled(bool newval){
		isXYtoggled = newval;
		if (newval) {
			ySensitivitySlider.interactable = true;
			yDotsPerFov.interactable = true;
			yDegsPerDot.interactable = true;
			yDotsPerDeg.interactable = true;
			yDotsPer360.interactable = true;
			yMultiplier.interactable = true;
		} else {
			ySensitivitySlider.interactable = false;
			yDotsPerFov.interactable = false;
			yDegsPerDot.interactable = false;
			yDotsPerDeg.interactable = false;
			yDotsPer360.interactable = false;
			yMultiplier.interactable = false;
			yUpdateSensitivity (fpsC.xSensitivity);
		}
	}


	public void xUpdateSensitivity(float newval){
		fpsC.xSensitivity = newval;
		if (!isXYtoggled) {
			yUpdateSensitivity (newval);
		}
		if (!xDotsPerFov.isFocused) {
			xDotsPerFov.text = (fpsC.hFov / (onePx * newval)).ToString ();
		}
		if (!xDegsPerDot.isFocused) {
			xDegsPerDot.text = (newval * onePx).ToString();
		}
		if (!xDotsPerDeg.isFocused) {
			xDotsPerDeg.text = (1 / (newval * onePx)).ToString();
		}
		if (!xDotsPer360.isFocused) {
			xDotsPer360.text = ((1 / (newval * onePx)) * 360).ToString();
		}
		if (!xMultiplier.isFocused) {
			xMultiplier.text = newval.ToString();
		}
		xSensitivitySlider.value = newval;
	}

	public void yUpdateSensitivity(float newval){
		fpsC.ySensitivity = newval;
		if (!yDotsPerFov.isFocused) {
			yDotsPerFov.text = (fpsC.vFov / (onePx * newval)).ToString ();
		}
		if (!yDegsPerDot.isFocused) {
			yDegsPerDot.text = (newval * onePx).ToString();
		}
		if (!yDotsPerDeg.isFocused) {
			yDotsPerDeg.text = (1 / (newval * onePx)).ToString();
		}
		if (!yDotsPer360.isFocused) {
			yDotsPer360.text = ((1 / (newval * onePx)) * 360).ToString();
		}
		if (!yMultiplier.isFocused) {
			yMultiplier.text = newval.ToString();
		}
		ySensitivitySlider.value = newval;
	}

	public void xDotsPerFovInputField(string newval){
		if (newval != "" && newval != "-" && newval != "." && xDotsPerFov.isFocused) {
			if (float.Parse (newval) > 0f) {
				xUpdateSensitivity (fpsC.hFov/(float.Parse(newval)*onePx));
			}
		}
	}

	public void xDegreesPerDotInputField(string newval){
		if (newval != "" && newval != "-" && newval != "." && xDegsPerDot.isFocused) {
			if (float.Parse (newval) > 0f) {
				xUpdateSensitivity (float.Parse (newval) / onePx);
			}
		}
	}

	public void xDotsPerDegreeInputField(string newval){
		if (newval != "" && newval != "-" && newval != "." && xDotsPerDeg.isFocused) {
			if (float.Parse (newval) > 0f) {
				xUpdateSensitivity (1f / ( float.Parse (newval) * onePx));
			}
		}
	}

	public void xDotsPer360InputField(string newval){
		if (newval != "" && newval != "-" && newval != "." && xDotsPer360.isFocused) {
			if (float.Parse (newval) > 0f) {
				xUpdateSensitivity ((1 / (float.Parse (newval) * onePx)) * 360);
			}
		}
	}

	public void xMultiplierInputField(string newval){
		if (newval != "" && newval != "-" && newval != "." && xMultiplier.isFocused) {
			if (float.Parse (newval) > 0f) {
				xUpdateSensitivity (float.Parse (newval));
			}
		}
	}

	public void yDegsPerFovInputField(string newval){
		if (newval != "" && newval != "-" && newval != "." && yDotsPerFov.isFocused) {
			if (float.Parse (newval) > 0f) {
				yUpdateSensitivity (fpsC.vFov/(float.Parse(newval)*onePx));
			}
		}
	}

	public void yDegreesPerDotInputField(string newval){
		if (newval != "" && newval != "-" && newval != "." && yDegsPerDot.isFocused) {
			if (float.Parse (newval) > 0f) {
				yUpdateSensitivity (float.Parse (newval) / onePx);
			}
		}
	}

	public void yDotsPerDegreeInputField(string newval){
		if (newval != "" && newval != "-" && newval != "." && yDotsPerDeg.isFocused) {
			if (float.Parse (newval) > 0f) {
				yUpdateSensitivity (1f / ( float.Parse (newval) * onePx));
			}
		}
	}

	public void yDotsPer360InputField(string newval){
		if (newval != "" && newval != "-" && newval != "." && yDotsPer360.isFocused) {
			if (float.Parse (newval) > 0f) {
				yUpdateSensitivity ((1 / (float.Parse (newval) * onePx)) * 360);
			}
		}
	}

	public void yMultiplierInputField(string newval){
		if (newval != "" && newval != "-" && newval != "." && yMultiplier.isFocused) {
			if (float.Parse (newval) > 0f) {
				yUpdateSensitivity (float.Parse (newval));
			}
		}
	}

}

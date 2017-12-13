using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScriptTimer : MonoBehaviour {

	//Create a UI Text, then create an Buttom.UI and put the text here. In button select gameobject and script timer click
	public Button phaseButton;
	public Text Timer;
	private float time = 4f;
	bool ActivateButton;

	// Use this for initialization
	void Start () {
		
		Timer.text = " Start ";
		ActivateButton = false;
	}
	
	// Update is called once per frame
	void Update () {

		//When the action button is true, it starts the timer counting down

		if (ControlState.CurrentPhase == Phase.Action) {

			time -= Time.deltaTime;
			Timer.text = "  " + time.ToString (" 0 ");

			//When the timer is 0, the action button is false 

			if (time <= 0) {

				Timer.text = " Start ";
				time = 4f;
				ActivateButton = false;

				ControlState.ChangePhases (Phase.Resolution);

			}

			if (ControlState.CurrentPhase == Phase.Resolution) {
				phaseButton.gameObject.SetActive (false);
				Timer.text = "";
			
			}
			if(ControlState.CurrentPhase == Phase.Planning) {
				Timer.text = " Start ";
			}


		}
}

	public void Click() {
		
		ControlState.ChangePhases (Phase.Action);


	}
}
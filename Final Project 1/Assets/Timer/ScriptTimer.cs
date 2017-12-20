using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScriptTimer : MonoBehaviour {

	//Create a UI Text, then create an Buttom.UI and put the text here. In button select gameobject and script timer click
	//public static ScriptTimer instance; 
	public Button phaseButton;
	public Text Timer;
	private float time = 4f;
	public GameObject gameMusicAction; 
	public GameObject gameMusic; 

	//bool ActivateButton;

	// Use this for initialization
	void Start () {
		Timer.text = "Start";
		//ActivateButton = false;

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
				//ActivateButton = false;

				ControlState.ChangePhases (Phase.Resolution);

			}

			if (ControlState.CurrentPhase == Phase.Resolution) {
				phaseButton.gameObject.SetActive (false);
				Timer.text = "";
			
			}
		
		}
		if(ControlState.CurrentPhase == Phase.Planning) {
			phaseButton.gameObject.SetActive (true);
			Timer.text = " Start ";
		}
		if (ControlState.CurrentPhase == Phase.End) { 
			//Instantiate (gameMusic, new Vector3 (0,0,0), Quaternion.identity);
			phaseButton.gameObject.SetActive (true);
			Timer.text = " Game Over! To Play Again Click Here :)";
		}
}

	public void Click() {
		if (ControlState.CurrentPhase == Phase.Planning) {
			//Destroy (NewBehaviourScript.instance.gameMusic);
			//Instantiate (gameMusicAction, new Vector3 (0,0,0), Quaternion.identity);
			ControlState.ChangePhases (Phase.Action);
		}
		if (ControlState.CurrentPhase == Phase.End) { 
			NewBehaviourScript.newScore = 0; 
			NewBehaviourScript.score = 0; 
			NewBehaviourScript.turn = 0;
			//Destroy (NewBehaviourScript.instance.gameMusic);
			//Instantiate (gameMusic, new Vector3 (0,0,0), Quaternion.identity);
			ControlState.ChangePhases (Phase.Planning);
		}

	}
}
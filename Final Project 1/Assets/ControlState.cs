using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Phase {


	Planning,
	Action,
	Resolution, 
	End

}

public class ControlState : MonoBehaviour {
	

	public static Phase CurrentPhase = Phase.Planning;

	public static void ChangePhases(Phase NewPhase){

	CurrentPhase = NewPhase;
		if (ControlState.CurrentPhase == Phase.Resolution) {
			NewBehaviourScript.instance.pushBlocks ();
		}

	}


}

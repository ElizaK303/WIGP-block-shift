using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeScript : MonoBehaviour {
	public int x, y;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnMouseDown() {
		if (ControlState.CurrentPhase == Phase.Action /*NewBehaviourScript.grid[x*/) {
			NewBehaviourScript.ProcessClick (gameObject);
		}
	}
}

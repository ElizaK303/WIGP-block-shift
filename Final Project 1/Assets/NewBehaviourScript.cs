 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour {

	public static NewBehaviourScript instance; 
	public int actionPhaseTimeLeft,actionPhaseSubtract; 
	public GameObject cube;
	Vector3 cubePosition; 
	public static int starterCube1X, starterCube1Y, starterCube2X, starterCube2Y,gridX,gridY;
	public static int moveStarterCubesX, moveStarterCubesY;
	static GameObject[,] grid;
	Color[,] colors;
	Color cube1Color,cube2Color;
	public Text text1, text2;
	Color cubeColor,leftCubeColor,rightCubeColor,upperCubeColor,lowerCubeColor,gridColor;
	List<Color> loot;
	// Use this for initialization
	void Start () {
		instance = this;
		loot = new List<Color>();
		gridX = 9; 
		gridY = 6;
		grid = new GameObject[gridX,gridY];
		starterCube1X = 5;
		starterCube1Y = 5;
		starterCube2X = 8; 
		starterCube2Y = 3;
		moveStarterCubesX = 0;
		moveStarterCubesY = 0;

		Color[] colors = {Color.black, Color.blue, Color.green, Color.red, Color.yellow, Color.white};
		for (int x = 0; x < 9; x++) {
			for (int y = 0; y < 6; y++)  {
				cubePosition = new Vector3 (x*1.75f-6f,y*1.75f-5f,0); 
				grid[x,y] = Instantiate (cube, cubePosition, Quaternion.identity);
				//grid[x,y].GetComponent<cubeScript>().x = x; 
				//grid[x,y].GetComponent<cubeScript>().y = y;
				grid[x,y].GetComponent<Renderer>().material.color = colors[Random.Range(0,6)];
				if (x == 8 || y == 5){
					grid [x, y].SetActive (false);
				}
			}
		}
		for (int x = 0; x < 9; x++) {
			for (int y = 0; y < 6; y++) {
				cubeColor = grid [x, y].GetComponent<Renderer> ().material.color; 
				if (x != 0 && x != 8 && y != 0 && y != 5) {
					leftCubeColor = grid [x - 1, y].GetComponent<Renderer> ().material.color; 
					rightCubeColor = grid [x + 1, y].GetComponent<Renderer> ().material.color; 
					upperCubeColor = grid [x, y + 1].GetComponent<Renderer> ().material.color; 
					lowerCubeColor = grid [x, y - 1].GetComponent<Renderer> ().material.color; 
					while (cubeColor == leftCubeColor && cubeColor == rightCubeColor)  {
						grid[x,y].GetComponent<Renderer>().material.color = colors[Random.Range(0,6)];
						cubeColor = grid [x, y].GetComponent<Renderer> ().material.color; 
					}
					while (cubeColor == upperCubeColor && cubeColor == lowerCubeColor) {
						grid[x,y].GetComponent<Renderer>().material.color = colors[Random.Range(0,6)];
						cubeColor = grid [x, y].GetComponent<Renderer> ().material.color; 
					}
				}
			}
		}	
	}
	
	// Update is called once per frame
	void Update () {
		detectKeyboardInput ();
		determineMovement ();
		if (ControlState.CurrentPhase == Phase.Planning) {
			newBlocks ();
		}
	} 

	void newBlocks() {
		grid [starterCube1X, starterCube1Y].SetActive (true);
		grid [starterCube2X, starterCube2Y].SetActive (true); 
		grid [starterCube1X, starterCube1Y].GetComponent<Renderer> ().material.color = cube1Color;
		grid [starterCube2X, starterCube2Y].GetComponent<Renderer> ().material.color = cube2Color;
	}

	void detectKeyboardInput() {

		if(ControlState.CurrentPhase == Phase.Planning){

		if (Input.GetKeyDown(KeyCode.DownArrow)) {
			moveStarterCubesY = -1; 
			moveStarterCubesX = 0;
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			moveStarterCubesY = 0; 
			moveStarterCubesX = -1;
		}
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			moveStarterCubesY = 0; 
			moveStarterCubesX = 1;
		}
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			moveStarterCubesY = 1; 
			moveStarterCubesX = 0;
		}
		
	}	

}
	void moveCubes(bool isDirectionY) {
		grid [starterCube1X, starterCube1Y].SetActive (false); 
		grid [starterCube2X, starterCube2Y].SetActive (false); 
		if (isDirectionY) {
			starterCube2Y += moveStarterCubesY;
		} else {
			starterCube1X += moveStarterCubesX;
		}
		moveStarterCubesY = 0;
		moveStarterCubesX = 0;
		grid [starterCube1X, starterCube1Y].SetActive (true);
		grid [starterCube2X, starterCube2Y].SetActive (true);
		grid [starterCube1X, starterCube1Y].GetComponent<Renderer> ().material.color = cube1Color;
		grid [starterCube2X, starterCube2Y].GetComponent<Renderer> ().material.color = cube2Color;
	}
	void determineMovement() {
		if ((moveStarterCubesY == -1 && starterCube2Y > 0 )|| (moveStarterCubesY == 1 && starterCube2Y < 4 )) {
			moveCubes (true); 
		}
		if ((moveStarterCubesX == -1 && starterCube1X > 0)|| (moveStarterCubesX == 1 && starterCube1X < 7)) {
			moveCubes(false);
		}

	}
	public static void ProcessClick(GameObject clickedCube) {
		if (clickedCube.activeSelf) { 
			Destroy (clickedCube);
		}
	}
	public void pushBlocks() {
		Color oldCubeColor; 
		oldCubeColor = Color.blue;
		bool end = false;

		for (int y = gridY-1; y >= 0 && !end; y--) {
			if (y == gridY - 1) {
				gridColor = grid [starterCube1X, y].GetComponent<Renderer> ().material.color;
				grid [starterCube1X, y].SetActive (false);
			} else {
				gridColor = oldCubeColor;
			}
			if (y == 0) { 
				loot.Add (gridColor);
			} else if (grid [starterCube1X, y - 1] == null) {
				cubePosition = new Vector3 (starterCube1X * 1.75f - 6f, (y - 1) * 1.75f - 5f, 0);
				grid [starterCube1X, y - 1] = Instantiate (cube, cubePosition, Quaternion.identity);
				//gridColor = grid [starterCube1X, y].GetComponent<Renderer> ().material.color;
				grid [starterCube1X, y - 1].GetComponent<Renderer> ().material.color = gridColor;
				if (y == gridY - 1) {
					grid [starterCube1X, y].SetActive (false);
				} else {
					Destroy (grid [starterCube1X, y]);
				}
				Debug.Log ("break");
				end = true;
			} else {
				oldCubeColor = grid [starterCube1X, y - 1].GetComponent < Renderer> ().material.color;
				grid [starterCube1X, y - 1].GetComponent<Renderer> ().material.color = gridColor;
			}
			Debug.Log ("continue for loop");

		}
		if (starterCube2X == 8) {
			

		}
	} 
}



	/*void fillGrid() {
		if (ControlState.CurrentPhase == Phase.Resolution) {
			for (int x = 0; x < 9; x++) {
				for (int y = 0; y < 6; y++) {
					if (x =){
					}
				}
			}

		}
	} */
	  

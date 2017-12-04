using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour {
	public bool actionPhase, planningPhase, endingPhase; 
	public int actionPhaseTimeLeft,actionPhaseSubtract; 
	public GameObject cube;
	Vector3 cubePosition; 
	public static int starterCube1X, starterCube1Y, starterCube2X, starterCube2Y,gridX,gridY;
	public static int moveStarterCubesX, moveStarterCubesY;
	static GameObject[,] grid;
	Color[,] colors;
	Color cube1Color,cube2Color;

 
	public Text text1, text2;
	Color cubeColor,leftCubeColor,rightCubeColor,upperCubeColor,lowerCubeColor;

	// Use this for initialization
	void Start () {
		
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
		newBlocks ();
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
		newBlocks ();
		detectKeyboardInput ();
		determineMovement ();
	} 

	void newBlocks() {
		grid [starterCube1X, starterCube1Y].SetActive (true);
		grid [starterCube2X, starterCube2Y].SetActive (true); 
		grid [starterCube1X, starterCube1Y].GetComponent<Renderer> ().material.color = cube1Color;
		grid [starterCube2X, starterCube2Y].GetComponent<Renderer> ().material.color = cube2Color;
	}
	void detectKeyboardInput() {
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
	void moveCubes(int cubeLoc1, int cubeLoc2 ) {
		grid [starterCube1X, starterCube1Y].SetActive (false); 
		grid [starterCube2X, starterCube2Y].SetActive (false); 
		cubeLoc1 += cubeLoc2;
		moveStarterCubesY = 0;
		moveStarterCubesX = 0;
		grid [starterCube1X, starterCube1Y].SetActive (true);
		grid [starterCube2X, starterCube2Y].SetActive (true);
		grid [starterCube1X, starterCube1Y].GetComponent<Renderer> ().material.color = cube1Color;
		grid [starterCube2X, starterCube2Y].GetComponent<Renderer> ().material.color = cube2Color;
	}
	void determineMovement() {
		if (moveStarterCubesY == -1 && starterCube2Y > 0) {
			moveCubes (starterCube2Y,moveStarterCubesY);
		
		}
		if (moveStarterCubesX == -1 && starterCube1X > 0) {
			moveCubes(starterCube1X,moveStarterCubesX);
			 
		}
		if (moveStarterCubesY == 1 && starterCube2Y < 4 ) {
			moveCubes (starterCube2Y,moveStarterCubesY);
			 
		}
		if (moveStarterCubesX == 1 && starterCube1X < 7) {
			moveCubes (starterCube1X,moveStarterCubesX);
			 
		}

	}

	/*void determineMovement() {
		if (moveStarterCubesY == -1 && starterCube2Y > 0) {
			grid [starterCube1X, starterCube1Y].SetActive (false); 
			grid [starterCube2X, starterCube2Y].SetActive (false); 
			starterCube2Y += moveStarterCubesY;
			moveStarterCubesY = 0;
			moveStarterCubesX = 0;
			grid [starterCube1X, starterCube1Y].SetActive (true);
			grid [starterCube2X, starterCube2Y].SetActive (true);
			grid [starterCube1X, starterCube1Y].GetComponent<Renderer> ().material.color = cube1Color;
			grid [starterCube2X, starterCube2Y].GetComponent<Renderer> ().material.color = cube2Color;
		}
		if (moveStarterCubesX == -1 && starterCube1X > 0) {
			grid [starterCube1X, starterCube1Y].SetActive (false); 
			grid [starterCube2X, starterCube2Y].SetActive (false); 
			starterCube1X += moveStarterCubesX;
			moveStarterCubesX = 0;
			moveStarterCubesY = 0;
			grid [starterCube1X, starterCube1Y].SetActive (true);
			grid [starterCube2X, starterCube2Y].SetActive (true);
			grid [starterCube1X, starterCube1Y].GetComponent<Renderer> ().material.color = cube1Color;
			grid [starterCube2X, starterCube2Y].GetComponent<Renderer> ().material.color = cube2Color;
		}
		if (moveStarterCubesY == 1 && starterCube2Y < 4 ) {
			grid [starterCube1X, starterCube1Y].SetActive (false); 
			grid [starterCube2X, starterCube2Y].SetActive (false); 
			starterCube2Y += moveStarterCubesY;
			moveStarterCubesX = 0;
			moveStarterCubesY = 0;
			grid [starterCube1X, starterCube1Y].SetActive (true);
			grid [starterCube2X, starterCube2Y].SetActive (true);
			grid [starterCube1X, starterCube1Y].GetComponent<Renderer> ().material.color = cube1Color;
			grid [starterCube2X, starterCube2Y].GetComponent<Renderer> ().material.color = cube2Color;
		}
		if (moveStarterCubesX == 1 && starterCube1X < 7) {
			grid [starterCube1X, starterCube1Y].SetActive (false); 
			grid [starterCube2X, starterCube2Y].SetActive (false); 
			starterCube1X += moveStarterCubesX;
			moveStarterCubesX = 0;
			moveStarterCubesY = 0;
			grid [starterCube1X, starterCube1Y].SetActive (true);
			grid [starterCube2X, starterCube2Y].SetActive (true);
			grid [starterCube1X, starterCube1Y].GetComponent<Renderer> ().material.color = cube1Color;
			grid [starterCube2X, starterCube2Y].GetComponent<Renderer> ().material.color = cube2Color;

		}

	}*/
	  
}
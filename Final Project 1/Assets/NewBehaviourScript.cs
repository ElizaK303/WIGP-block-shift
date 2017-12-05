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
	public static int pusher1index,pusher1Index; 

 
	public Text text1, text2;
	Color cubeColor,leftCubeColor,rightCubeColor,upperCubeColor,lowerCubeColor;
	int[] validX, validY;

	// Use this for initialization
	void Start () {
		
		gridX = 10; 
		gridY = 7;
		grid = new GameObject[gridX,gridY];

		/*
		starterCube1X = 5;
		starterCube1Y = 5;
		starterCube2X = 8; 
		starterCube2Y = 3;
		moveStarterCubesX = 0;
		moveStarterCubesY = 0;
		*/

		validX = new int[gridX * 2 - 4 + gridY * 2 - 4];
		validY = new int[gridX * 2 - 4 + gridY * 2 - 4];

		Color[] colors = {Color.black, Color.blue, Color.green, Color.red, Color.yellow, Color.white};

		///this shoul be gridx and gridy 
		for (int x = 0; x < gridX; x++) {
			
			for (int y = 0; y < gridY; y++)  {
				
				cubePosition = new Vector3 (x*1.75f-6f,y*1.75f-5f,0); 
				grid[x,y] = Instantiate (cube, cubePosition, Quaternion.identity);
				//grid[x,y].GetComponent<cubeScript>().x = x; 
				//grid[x,y].GetComponent<cubeScript>().y = y;
				grid[x,y].GetComponent<Renderer>().material.color = colors[Random.Range(0,6)];

				if (x == gridX - 1 || y == gridY - 1 || x == 0 || y == 0){
					
					grid [x, y].SetActive (false);

				}
			}
		}
		newBlocks ();
		for (int x = 0; x < gridX; x++) {
			for (int y = 0; y < gridY; y++) {
				cubeColor = grid [x, y].GetComponent<Renderer> ().material.color; 
				if (x != 0 && x != gridX - 1 && y != 0 && y != gridY - 1) {
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
		//determineMovement ();
	} 

	void newBlocks() {
		grid [starterCube1X, starterCube1Y].SetActive (true);
		grid [starterCube2X, starterCube2Y].SetActive (true); 
		grid [starterCube1X, starterCube1Y].GetComponent<Renderer> ().material.color = cube1Color;
		grid [starterCube2X, starterCube2Y].GetComponent<Renderer> ().material.color = cube2Color;
	}

	void detectKeyboardInput() {
		if (Input.GetKeyDown(KeyCode.DownArrow)) {
			movePusher1 (true);
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
		
		}
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
		
		}
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			movePusher1 (false);
		}
	}


	void movePusher1(bool clockwise) {
		grid [ validX[pusher1index], validY[pusher1index] ].SetActive (false);

		if (clockwise) {
			pusher1index++;
		} else {
			pusher1index--;
		}

		grid [ validX[pusher1Index], validY[pusher1index] ].SetActive (true);
		grid [ validX[pusher1Index], validY[pusher1index] ].GetComponent<Renderer> ().material.color = cube1Color;
	}

	/*
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
	*/

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
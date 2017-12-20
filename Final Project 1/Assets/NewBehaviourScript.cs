 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour {
	
	public static NewBehaviourScript instance; 
	public int actionPhaseTimeLeft,actionPhaseSubtract; 
	public GameObject cube;
	Vector3 cubePosition; 
	public static int starterCube1X, starterCube1Y, starterCube2X, starterCube2Y,gridX,gridY,turnMax,turn;
	public static int moveStarterCubesX, moveStarterCubesY;
	static GameObject[,] grid;
	Color[,] colors;
	Color cube1Color,cube2Color;
	public Text text1, text2;
	Color cubeColor,leftCubeColor,rightCubeColor,upperCubeColor,lowerCubeColor,gridColor;
	public static List<Color> loot;
	public static int score,newScore,lootScore;
	public bool over,newBlocksSpawned; 
	public GameObject gameMusic; 
	// Use this for initialization
	void Start () {
		over = false; 
		newBlocksSpawned = false;
		turn = 0; 
		turnMax = 12;
		score = 0;
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
		//Instantiate (gameMusic, new Vector3 (0,0,0), Quaternion.identity);
		//Sets 8*5 grid plus pusher cubes. 
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
		if (ControlState.CurrentPhase == Phase.Resolution){ 
			getScore (); 
			print(score); 
			turn += 1;
			newBlocksSpawned = false;
			fillNullSpots ();
			if (turn < turnMax) {
				ControlState.ChangePhases (Phase.Planning);
				over = false; 
			}
			if (turn == turnMax) {
				ControlState.ChangePhases (Phase.End);
				over = false;
			}
	}		

	}

	void newBlocks() {
		//sets the color of the two pusher cubes
		while (!over) {
			Color[] colors = {Color.black, Color.blue, Color.green, Color.red, Color.yellow, Color.white};
			cube1Color = colors[Random.Range(0,6)];
			cube2Color = colors[Random.Range(0,6)];
			over = true;
		}
		//instantiates pusher cubes
		grid [starterCube1X, starterCube1Y].SetActive (true);
		grid [starterCube2X, starterCube2Y].SetActive (true); 
		grid [starterCube1X, starterCube1Y].GetComponent<Renderer> ().material.color = cube1Color;
		grid [starterCube2X, starterCube2Y].GetComponent<Renderer> ().material.color = cube2Color;
	}

	void detectKeyboardInput() {
		//Gets keyboard input during planning phase. 

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
		//sets the original cubes inactive if a key was pressed. 
		grid [starterCube1X, starterCube1Y].SetActive (false); 
		grid [starterCube2X, starterCube2Y].SetActive (false); 
		//Determines which grid location (gridx location or gridy location) the number should be added to. 
		if (isDirectionY) {
			starterCube2Y += moveStarterCubesY;
		} else {
			starterCube1X += moveStarterCubesX;
		}
		//resets keyboard input.
		moveStarterCubesY = 0;
		moveStarterCubesX = 0;
		//instantiates new cubes. 
		grid [starterCube1X, starterCube1Y].SetActive (true);
		grid [starterCube2X, starterCube2Y].SetActive (true);
		grid [starterCube1X, starterCube1Y].GetComponent<Renderer> ().material.color = cube1Color;
		grid [starterCube2X, starterCube2Y].GetComponent<Renderer> ().material.color = cube2Color;
	}
	void determineMovement() {
		//adds keyboard input int to gridx or grid y location 
		if ((moveStarterCubesY == -1 && starterCube2Y > 0 )|| (moveStarterCubesY == 1 && starterCube2Y < 4 )) {
			moveCubes (true); 
		}
		if ((moveStarterCubesX == -1 && starterCube1X > 0)|| (moveStarterCubesX == 1 && starterCube1X < 7)) {
			moveCubes(false);
		}

	}
	public static void ProcessClick(GameObject clickedCube) {
		//deletes cube if clicked 
		if (clickedCube.activeSelf) { 
			Destroy (clickedCube);
		}
	}
	public void pushBlocks() {
		Color oldCubeColor; 
		oldCubeColor = Color.blue;
		bool end = false;
		//moves pusher bocks.  
		for (int y = gridY-1; y >= 0 && !end; y--) {
			if (y == gridY - 1) {
				gridColor = grid [starterCube1X, y].GetComponent<Renderer> ().material.color;
				grid [starterCube1X, y].SetActive (false);
			} else {
				gridColor = oldCubeColor;
			}
			if (y == 0) { 
				//appends to grid array of loot so that the length can be counted. 	
				loot.Add (gridColor);
			} else if (grid [starterCube1X, y - 1] == null) {
				while (grid [starterCube1X, y - 1] == null) {
					cubePosition = new Vector3 (starterCube1X * 1.75f - 6f, (y - 1) * 1.75f - 5f, 0);
					grid [starterCube1X, y - 1] = Instantiate (cube, cubePosition, Quaternion.identity);
					//gridColor = grid [starterCube1X, y].GetComponent<Renderer> ().material.color;
					grid [starterCube1X, y - 1].GetComponent<Renderer> ().material.color = gridColor;
					if (y == gridY - 1) {
						grid [starterCube1X, y].SetActive (false);
					} else {
						Destroy (grid [starterCube1X, y]);
					}
				}
			
			} else {
				oldCubeColor = grid [starterCube1X, y - 1].GetComponent < Renderer> ().material.color;
				grid [starterCube1X, y - 1].GetComponent<Renderer> ().material.color = gridColor;
			}

		}
		if (starterCube2X == 8) {
			for (int x = gridX-1; x >= 0 && !end; x--) {
				if (x == gridX - 1) {
					gridColor = grid [x, starterCube2Y].GetComponent<Renderer> ().material.color;
					grid [x, starterCube2Y].SetActive (false);
				} else {
					gridColor = oldCubeColor;
				}
				if (x == 0) { 
					loot.Add (gridColor);
				} else if (grid [x, starterCube2Y]== null) {
					while (grid [x, starterCube2Y] == null) {
						cubePosition = new Vector3 (x * 1.75f - 6f, (starterCube2Y - 1) * 1.75f - 5f, 0);
						grid [x, starterCube2Y] = Instantiate (cube, cubePosition, Quaternion.identity);
						//gridColor = grid [starterCube1X, y].GetComponent<Renderer> ().material.color;
						grid [x, starterCube2Y].GetComponent<Renderer> ().material.color = gridColor;
						if (x == gridY - 1) {
							grid [x, starterCube2Y].SetActive (false);
						} else {
							Destroy (grid [x, starterCube2Y]);
						}
					}

					//Debug.Log ("break");
					//end = true;
				} else {
					oldCubeColor = grid [x, starterCube2Y].GetComponent < Renderer> ().material.color;
					grid [x, starterCube2Y].GetComponent<Renderer> ().material.color = gridColor;
				}


			}


		}
	} 

	public void getScore() {
		//sets the score for the round to newscore
		newScore = 0;
		//adds loot to score. 
		lootScore = loot.Count;
		score += lootScore;
		loot.Clear();
		for (int x = 0; x< gridX-2; x++) {
			for (int y = 0; y< gridY-2; y++) { 
				if (grid[x,y].GetComponent<Renderer>().material.color ==
					grid[x+1, y].GetComponent<Renderer>().material.color)
				{
					if (grid[x, y].GetComponent<Renderer>().material.color ==
						grid[x+2, y].GetComponent<Renderer>().material.color)
					{
						newScore += 10;
					}
			}
		}
			
	}
		if (newScore >= 20) {
			//combo bonus. Because the new score is always a multiple of ten there will be a combo bonus if it is above 19. 
			newScore *= (newScore / 10);
		}
		//adds newscore to the score.
		score += newScore; 
}
	public void fillNullSpots() {
		//fills null spots.
		while (!newBlocksSpawned) {
			for (int x = 0; x < 9; x++) {
				for (int y = 0; y < 6; y++)  {
					if (grid [x, y] == null) {
						cubePosition = new Vector3 (x*1.75f-6f,y*1.75f-5f,0); 
						grid [x, y] = Instantiate (cube, cubePosition, Quaternion.identity);
					}
				
				}
			}
			//Destroy (ScriptTimer.instance.gameMusic); 
			//Instantiate (gameMusic, new Vector3 (0,0,0), Quaternion.identity);
			newBlocksSpawned = true; 
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
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour {
	public GameObject cube;
	Vector3 cubePosition; 
	public static int starterCube1X, starterCube1Y, starterCube2X, starterCube2Y,gridX,gridY;
	public static int moveStarterCubesX, moveStarterCubesY;
	static GameObject[,] grid;
	Color[,] colors;
	Color cube1Color,cube2Color;
	public Text text1; 
	public Text text2;

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
		for (int x = 0; x < 8; x++) {
			for (int y = 0; y < 5; y++)  {
				cubePosition = new Vector3 (x*1.75f-6f,y*1.75f-5f,0); 
				grid[x,y] = Instantiate (cube, cubePosition, Quaternion.identity);
				//grid[x,y].GetComponent<cubeScript>().x = x; 
				//grid[x,y].GetComponent<cubeScript>().y = y;
				grid[x,y].GetComponent<Renderer>().material.color = colors[Random.Range(0,6)];
			}
		}
		cube1Color = colors [Random.Range (0, 6)];
		cube2Color = colors [Random.Range (0, 6)];
		newBlocks ();
		
	}
	
	// Update is called once per frame
	void Update () {
		newBlocks ();
		detectKeyboardInput ();
		moveCubes ();
	} 

	void newBlocks() {
		grid[starterCube1X,starterCube1Y] = Instantiate (cube, new Vector3 (starterCube1X*1.75f-6f,starterCube1X*1.75f-5f,0), Quaternion.identity);
		grid[starterCube2X,starterCube2Y] = Instantiate (cube, new Vector3 (starterCube2X*1.75f-6f,starterCube2Y*1.75f-5f,0) , Quaternion.identity);
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
	void moveCubes() {
		if (moveStarterCubesY == -1 && starterCube2Y > 0) {
			grid [starterCube1X, starterCube1Y].SetActive (false); 
			grid [starterCube2X, starterCube2Y].SetActive (false); 
			starterCube2Y += moveStarterCubesY;
			moveStarterCubesY = 0;
			moveStarterCubesX = 0;
			grid [starterCube1X,starterCube1Y] = Instantiate (cube, new Vector3 (starterCube1X*1.75f-6f,starterCube1Y*1.75f-5f,0), Quaternion.identity);
			grid [starterCube2X,starterCube2Y] = Instantiate (cube, new Vector3 (starterCube2X*1.75f-6f,starterCube2Y*1.75f-5f,0) , Quaternion.identity);
			grid [starterCube1X, starterCube1Y].GetComponent<Renderer> ().material.color = cube1Color;
			grid [starterCube2X, starterCube2Y].GetComponent<Renderer> ().material.color = cube2Color;
		}
		if (moveStarterCubesX == -1 && starterCube1X > 0) {
			grid [starterCube1X, starterCube1Y].SetActive (false); 
			grid [starterCube2X, starterCube2Y].SetActive (false); 
			starterCube1X += moveStarterCubesX;
			moveStarterCubesX = 0;
			moveStarterCubesY = 0;
			grid [starterCube1X,starterCube1Y] = Instantiate (cube, new Vector3 (starterCube1X*1.75f-6f,starterCube1Y*1.75f-5f,0), Quaternion.identity);
			grid [starterCube2X,starterCube2Y] = Instantiate (cube, new Vector3 (starterCube2X*1.75f-6f,starterCube2Y*1.75f-5f,0) , Quaternion.identity);
			grid [starterCube1X, starterCube1Y].GetComponent<Renderer> ().material.color = cube1Color;
			grid [starterCube2X, starterCube2Y].GetComponent<Renderer> ().material.color = cube2Color;
		}
		if (moveStarterCubesY == 1 && starterCube2Y < 4 ) {
			grid [starterCube1X, starterCube1Y].SetActive (false); 
			grid [starterCube2X, starterCube2Y].SetActive (false); 
			starterCube2Y += moveStarterCubesY;
			moveStarterCubesX = 0;
			grid [starterCube1X,starterCube1Y] = Instantiate (cube, new Vector3 (starterCube1X*1.75f-6f,starterCube1Y*1.75f-5f,0), Quaternion.identity);
			grid [starterCube2X,starterCube2Y] = Instantiate (cube, new Vector3 (starterCube2X*1.75f-6f,starterCube2Y*1.75f-5f,0) , Quaternion.identity);
			grid [starterCube1X, starterCube1Y].GetComponent<Renderer> ().material.color = cube1Color;
			grid [starterCube2X, starterCube2Y].GetComponent<Renderer> ().material.color = cube2Color;
		}
		if (moveStarterCubesX == 1 && starterCube1X < 7) {
			grid [starterCube1X, starterCube1Y].SetActive (false); 
			grid [starterCube2X, starterCube2Y].SetActive (false); 
			starterCube1X += moveStarterCubesX;
			moveStarterCubesX = 0;
			moveStarterCubesY = 0;
			grid [starterCube1X,starterCube1Y] = Instantiate (cube, new Vector3 (starterCube1X*1.75f-6f,starterCube1Y*1.75f-5f,0), Quaternion.identity);
			grid [starterCube2X,starterCube2Y] = Instantiate (cube, new Vector3 (starterCube2X*1.75f-6f,starterCube2Y*1.75f-5f,0) , Quaternion.identity);
			grid [starterCube1X, starterCube1Y].GetComponent<Renderer> ().material.color = cube1Color;
			grid [starterCube2X, starterCube2Y].GetComponent<Renderer> ().material.color = cube2Color;

		}

	}
	  
}
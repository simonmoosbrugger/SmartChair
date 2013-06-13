using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using SmartChair.model;


public class GameController : MonoBehaviour
{
	public enum GameState
	{
		playing,
		won,
		lost
	};
	
	public static GameController SP;
	private int totalGems;
	private int foundGems;
	private GameState gameState;
	private TcpClient _clientSocket;
	private float time =2.0f;
	private bool showBox = false;
	
	void Awake ()
	{
		SP = this; 
		gameState = GameState.playing;
		totalGems = GameObject.FindGameObjectsWithTag ("Pickup").Length;
		foundGems = 0;
		_clientSocket = new TcpClient ();
		_clientSocket.Connect ("127.0.0.1", 9900);
		Thread ctThread = new Thread (getMessage);
		ctThread.Start ();
		Time.timeScale = 1.0f;	
	}

	void OnGUI ()
	{
		if(!showBox) {
			GUI.Box(new Rect(0,0,200,100),"");
			GUI.Label(new Rect(8,10,100, 100),"Game started");
			GUI.Label(new Rect(5,30,200,100), " Found gems: " + foundGems + "/" + totalGems);
			if(GUI.Button(new Rect(40, 60, 100, 30), "Next Level")) {
				if(Application.loadedLevel == 0) {
					Application.LoadLevel (1);
				}else {
					Application.LoadLevel (0);
				}
			}
		}
		//GUILayout.Label (" Found gems: " + foundGems + "/" + totalGems);
		
		
		if(gameState == GameState.lost) {
			showBox = true;
			GUI.Box(new Rect(Screen.width/2-100,Screen.height/2-100,200,100),"");
			GUI.Label(new Rect(Screen.width/2-40,Screen.height/2-60,100, 100),"Game lost!");
			if(GUI.Button(new Rect(Screen.width/2-30,Screen.height/2-35,80,30), "Try again!")) {
				if(Application.loadedLevel == 0) {
					Application.LoadLevel (0);
				}else if(Application.loadedLevel == 1){
					Application.LoadLevel (1);
				}
			}
			/*while((time -=Time.deltaTime) > 0) {
				Application.LoadLevel (0);
			}*/
		}else if(gameState == GameState.won) {
			showBox = true;
			GUI.Box(new Rect(Screen.width/2-100,Screen.height/2-100,200,100),"");
			GUI.Label(new Rect(Screen.width/2-40,Screen.height/2-60,100, 100),"Game won!");
			if(GUI.Button(new Rect(Screen.width/2-30,Screen.height/2-35,80,30), "Try again!")) {
				if(Application.loadedLevel == 0) {
					Application.LoadLevel (1);
				}else if(Application.loadedLevel == 1){
					Application.LoadLevel (0);
				}
			}
			/*while((time -=Time.deltaTime) > 0) {
				Application.LoadLevel (1);
			}*/
		}
		

	}
	
	public void FoundGem ()
	{
		foundGems++;
		
		if (foundGems >= totalGems) {
			WonGame ();
		}
	}
	
	public SensorData _data;
	
	private  void getMessage ()
	{
		while (true) {
			NetworkStream stream = _clientSocket.GetStream ();
			BinaryFormatter formatter = new BinaryFormatter ();
			object obj = formatter.Deserialize (stream);
			SensorData data = (SensorData)obj;
			//Console.WriteLine (data.BottomLeft + " - " + data.BottomoRight + " - " + data.TopLeft + " - " + data.TopRight);
			_data = (SensorData)obj;
		}
	}

	public void WonGame ()
	{
		Time.timeScale = 0.0f;
		gameState = GameState.won;
	}

	public void SetGameOver ()
	{
		Time.timeScale = 0.0f;
		gameState = GameState.lost;
	}
}

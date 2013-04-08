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
		GUILayout.Label ("Game started");
		GUILayout.Label (" Found gems: " + foundGems + "/" + totalGems);

		if (gameState == GameState.lost) {
			GUILayout.Label ("You Lost!");
			if (GUILayout.Button ("Try again")) {
				Application.LoadLevel (Application.loadedLevel);
			}
		} else if (gameState == GameState.won) {
			GUILayout.Label ("You won!");
			if (GUILayout.Button ("Play again")) {
				Application.LoadLevel (Application.loadedLevel);
			}
		}
		

	}
	
	public void FoundGem ()
	{
		foundGems++;
		if (foundGems >= totalGems) {
			WonGame ();
		}
	}

	private  void getMessage ()
	{
		while (true) {
			NetworkStream stream = _clientSocket.GetStream ();
			BinaryFormatter formatter = new BinaryFormatter ();
			object obj = formatter.Deserialize (stream);
			SensorData data = (SensorData)obj;
			Console.WriteLine (data.BottomLeft + " - " + data.BottomoRight + " - " + data.TopLeft + " - " + data.TopRight);
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

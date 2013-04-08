using UnityEngine;
using System.Collections;

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
	
	void Awake ()
	{
		SP = this; 
		gameState = GameState.playing;
		totalGems = GameObject.FindGameObjectsWithTag ("Pickup").Length;
		foundGems = 0;
		Time.timeScale = 1.0f;	
	}

	void OnGUI ()
	{
		GUILayout.Label ("Game started");
		GUILayout.Label(" Found gems: "+foundGems+"/"+totalGems );

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
	
	public void FoundGem()
    {
        foundGems++;
        if (foundGems >= totalGems)
        {
            WonGame();
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

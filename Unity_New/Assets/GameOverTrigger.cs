using UnityEngine;
using System.Collections;

public class GameOverTrigger : MonoBehaviour {

	 void OnTriggerEnter()
    {
        GameController.SP.SetGameOver();
    }
}

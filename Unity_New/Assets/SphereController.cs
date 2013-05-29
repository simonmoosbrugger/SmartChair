using UnityEngine;
using System.Collections;
using SmartChair.model;
using System;

public class SphereController : MonoBehaviour
{
	   public float movementSpeed = 6.0f;
	private int factor = 2;
	// Update is called once per frame
	void Update ()
	{	
		SensorData data = GameController.SP._data;
		if(data != null){
			try{
				float x = 0;
				float y = 0;
				if(data.Cog.X >= 5 || data.Cog.X <= -5){
					x = data.Cog.X * factor;
				}
				
				if(data.Cog.Y >= 1 || data.Cog.Y <= -5){
					y = data.Cog.Y * factor;
				}
				
				rigidbody.AddForce(x,0, -y, ForceMode.Acceleration);
			} catch (Exception) {}
		}
	}
	
	void OnTriggerEnter  (Collider other  ) {
        if (other.tag == "Pickup")
        {
            GameController.SP.FoundGem();
            Destroy(other.gameObject);
        }
        else
        {
            //Other collider.. See other.tag and other.name
        }        
    }
}

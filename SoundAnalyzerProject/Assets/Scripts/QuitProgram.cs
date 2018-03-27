using UnityEngine;
using System.Collections;

public class QuitProgram : MonoBehaviour 
{

	float timeHeld = 2f;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKey(KeyCode.Escape))
		{
			timeHeld -= Time.deltaTime;
			Debug.Log(timeHeld);
		}

		if(Input.GetKeyUp(KeyCode.Escape))
		{
			timeHeld = 2f;
		}


		if(timeHeld < 0)
		{
			Application.Quit();

		}
	}
}

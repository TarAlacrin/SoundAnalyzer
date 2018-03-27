using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnOnOffCubes: ButtonPress 
{
	public PopulateCubes populai;//used
	// Use this for initialization
	public override void Start () 
	{
		base.Start();
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();


		if(this.isDepressed)
		{
			populai.queue = 1;
		}
		else
		{
			populai.queue = 0;
		}
	}
	
	
	
	
	public override void OnPress()
	{
		base.OnPress();

		this.isDepressed = !this.isDepressed;

	}
	
	public override void OnLeave()
	{
		base.OnLeave();

		//this.isDepressed = false;
	}
	
	
}

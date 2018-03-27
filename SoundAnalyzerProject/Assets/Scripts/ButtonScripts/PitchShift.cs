using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PitchShift : ButtonPress 
{
	public AudioSampler sampler;//used
	public bool resetPitch;//should this reset the pitch to 1 when pressed
	public float pitchShift;//amount to shift the pitch of the song when pressed
	AudioSource soundplayer;
	// Use this for initialization
	public override void Start () 
	{
		base.Start();
		soundplayer = sampler.gameObject.GetComponent<AudioSource>();
		soundplayer.Play();
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
		
	}
	
	
	
	
	public override void OnPress()
	{
		base.OnPress();
		if(!this.isDepressed)
		{
			this.isDepressed = true;
			if(resetPitch)
			{
				soundplayer.pitch = 1;
			}
			else
			{
				soundplayer.pitch += pitchShift;
			}
		}
	}
	
	public override void OnLeave()
	{
		base.OnLeave();
		this.isDepressed = false;
	}
	
	
}

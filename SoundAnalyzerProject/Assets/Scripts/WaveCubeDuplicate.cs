using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveCubeDuplicate : MonoBehaviour 
{
	public int dupeNumber;//the number of duplicates that this. UNUSED
	public int sampleNumber; // the sample number that this cube is attatched too. UNUSED
	public string baseString = "waveCube ";

	public WaveHistoryHandler handler;

	public Transform previous;
	Vector3 delayedScale = Vector3.zero;
	 
	int updateframe = 0;//this is the number of frames that have past after the last frame where the scale was updated

	Vector3[] scaleCollection;//for interpolation (saves all the values for the frames between updates) 

	// Use this for initialization
	void Start () 
	{
		//sets up the 
		if(dupeNumber == 0)
		{
			scaleCollection = new Vector3[handler.frameDelay];
			for(int i =0; i < scaleCollection.Length; i++)
			{
				scaleCollection[i] = Vector3.zero;
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{

		//updates the scale every so many frames
		if(updateframe > handler.frameDelay)
		{
			updateframe = 0;
			if(dupeNumber == 0 && handler.interpolateInterem)
			{
				Vector3 interpScale = delayedScale;
				for(int i =0; i < scaleCollection.Length; i++)
				{
					interpScale += scaleCollection[i];
				}
				float mult = 1/(scaleCollection.Length + 1);
				interpScale = Vector3.Scale(interpScale, new Vector3(mult, mult, mult));
				this.transform.localScale = interpScale;
			}
			else
			{
				this.transform.localScale = delayedScale;
			}
		}

		if(updateframe == handler.frameDelay)
		{
			delayedScale = previous.localScale;
			if(dupeNumber == 0)
			{
				delayedScale = Vector3.Scale(delayedScale, handler.waveDuplicateSizeMod);
			}
		}
		else if(dupeNumber == 0 && updateframe < handler.frameDelay)
		{
			scaleCollection[updateframe] = delayedScale;
		}
		
		updateframe++;


	}
}

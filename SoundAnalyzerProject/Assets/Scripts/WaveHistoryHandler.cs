using UnityEngine;
using System.Collections;

public class WaveHistoryHandler : MonoBehaviour 
{
	public AudioSampler sampler;
	public bool enableColliders = false;
	public Vector3 initialOffset = new Vector3(0,0,0);
	public Vector3 waveDuplicateOffset = new Vector3(0,0,0);
	public Vector3 waveDuplicateSizeMod = new Vector3(1,1,1);

	public int frameDelay = 1;//number of frames that the history will hold its current size before switching.
	public int historyLength = 4;//number of rowes of history to initialize
	public string baseString = "waveCube ";

	public bool interpolateInterem = false;//should the history cubes compute the average of the different transforms during the delay


	int numberOfSamples;
	bool hasInitiated = false;


	// Use this for initialization
	void Start () 
	{
		if(frameDelay < 0)
			frameDelay = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!hasInitiated && Time.time > 0.1f)
		{
			hasInitiated = true;
			for(int i = 0; i < sampler.xSamples; i++)//number of samples
			{
				for(int j = 0; j < this.historyLength; j++)//number of histories to create per sample
				{
					GameObject newObj = GameObject.CreatePrimitive(UnityEngine.PrimitiveType.Cube);
					GameObject waveObj;

					if(j == 0)
					{
						waveObj = GameObject.Find(baseString + i);
						newObj.transform.position = waveObj.transform.position + initialOffset;
						newObj.transform.localScale = Vector3.Scale(waveObj.transform.localScale, waveDuplicateSizeMod);
					}
					else
					{
						waveObj = GameObject.Find(baseString + i + "_" + (j-1));
						newObj.transform.position = waveObj.transform.position + waveDuplicateOffset;
						newObj.transform.localScale = Vector3.Scale(waveObj.transform.localScale,new Vector3(1,0,1));
					}

					//Destroy( newObj.GetComponent<BoxCollider>() );	
					newObj.GetComponent<BoxCollider>().enabled = this.enableColliders;
					newObj.name = "waveCube " + i +"_"+j; 

					WaveCubeDuplicate dupe = newObj.AddComponent<WaveCubeDuplicate>();
					dupe.baseString = this.baseString;
					dupe.dupeNumber = j;
					dupe.handler = this;
					dupe.previous = waveObj.transform;
					dupe.sampleNumber = i;

				}
			}

		}
	}


	void makeHistory(int numberOfSamples)
	{

	}
}

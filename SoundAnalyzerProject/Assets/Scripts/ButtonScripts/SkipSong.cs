using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

[ExecuteInEditMode]
public class SkipSong : ButtonPress 
{
	public AudioSampler sampler;//used
	public List<AudioClip> possibleTracks;
	public int curTrack = 0;
	AudioSource soundplayer;

	public Text uiSongDisplay;

	// Use this for initialization
	public override void Start () 
	{
		base.Start();


		Reload ();

		soundplayer = sampler.gameObject.GetComponent<AudioSource>();
		soundplayer.clip = possibleTracks[curTrack];

		if(!Application.isEditor)
			soundplayer.Play();
	}


	public void Reload()
	{
		if (Application.isEditor) 
		{
			curTrack = 0;

			for(int i = possibleTracks.Count-1; i >= 0; i--)
			{
				if(possibleTracks[i]== null)
				{
					possibleTracks.RemoveAt(i);
				}
			}

			List<string> filesinfolder = new List<string> (Directory.GetFiles (Application.dataPath+"/Songs/Resources"));

			for (int j = 0; j < filesinfolder.Count; j++) {

				string[] splitfiles = filesinfolder [j].Split (".".ToCharArray (), System.StringSplitOptions.None);

				string filetype = splitfiles [splitfiles.Length - 1].ToLower();
				if (filetype == "wav" || filetype == "ogg" || filetype == "mp3") 
				{
					string filenamesplit = filesinfolder [j].Split ("\\/".ToCharArray ()).Last();
					filenamesplit = filenamesplit.Remove (filenamesplit.Length - 4);
					AudioClip aclip = Resources.Load<AudioClip> (filenamesplit);


					if (aclip != null) {
						if(!possibleTracks.Contains(aclip))
							possibleTracks.Add(aclip);
					}
				}


			}

		}

	}


	// Update is called once per frame
	public override void Update ()
	{
		base.Update();
		
		if(Input.GetKeyDown(KeyCode.R))
		{
			Reload ();
			soundplayer.Play();
		}
	}




	public override void OnPress()
	{
		base.OnPress();
		if(!this.isDepressed)
		{
			this.isDepressed = true;

			curTrack ++;
			if(curTrack >=possibleTracks.Count)
			{
				curTrack =0;
			}

			if(curTrack == 2 || curTrack == 3)
				sampler.levelsPowerMult = new Vector2(1f, 0.4f);
			else
				sampler.levelsPowerMult = new Vector2(1.3f, .8f);
						
						
			soundplayer.clip = possibleTracks[curTrack];
			uiSongDisplay.text = "CurrentSong: " + possibleTracks[curTrack].name;
			soundplayer.Play();
		}
	}
	
	public override void OnLeave()
	{
		base.OnLeave();
		this.isDepressed = false;
	}


}

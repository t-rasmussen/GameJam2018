using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySample : MonoBehaviour {
    public AudioSource sampleD;
    public AudioSource sampleV;
    public AudioSource sampleF;
    public AudioSource sampleH;

    public int butScene;
	private bool isPlaying = false;

    private void OnMouseDown()
    {
        if (PlayerPrefs.GetInt("SceneCur") == butScene)
        {

			if (!isPlaying)
			{
				isPlaying = true;
				if (PlayerPrefs.GetInt("instruType") == 1)
				{
					sampleD.Play();
				}

				if (PlayerPrefs.GetInt("instruType") == 2)
				{
					sampleV.Play();
				}

				if (PlayerPrefs.GetInt("instruType") == 3)
				{
					sampleF.Play();
				}

				if (PlayerPrefs.GetInt("instruType") == 4)
				{
					sampleH.Play();
				}
				PlayerPrefs.SetInt("sampleplay", 1);
			}
			else
			{
				isPlaying = false;
				sampleD.Stop();
				sampleV.Stop();
				sampleF.Stop();
				sampleH.Stop();
			}
        }
    }



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //stops sample if on different scene
       if ( PlayerPrefs.GetInt("SceneCur") != butScene) {
            sampleD.Stop();
            sampleV.Stop();
            sampleF.Stop();
            sampleH.Stop();
            PlayerPrefs.SetInt("sampleplay", 0);
        }

        if (PlayerPrefs.GetInt("submitting") == 1)
        {
            sampleD.Stop();
            sampleV.Stop();
            sampleF.Stop();
            sampleH.Stop();
            PlayerPrefs.SetInt("sampleplay", 0);
        }

        //if none of the samples are playing then sampleplay goes to 0
        if ( !sampleD.isPlaying && !sampleV.isPlaying 
            && !sampleF.isPlaying && !sampleH.isPlaying)
        {
            PlayerPrefs.SetInt("sampleplay", 0);
        }
    }
}

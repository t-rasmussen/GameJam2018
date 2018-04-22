using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTheme : MonoBehaviour {
    public AudioSource thememusic;
	public int butScene;
    private bool canplay=true;


    // Use this for initialization
    void Start () {
    }



    // Update is called once per frame
    //plays the theme song on title and instrument select page
    void Update () {

		if (PlayerPrefs.GetInt("SceneCur") == butScene)
		{
			thememusic.mute = true;
			canplay = false;
		}
		else if ( PlayerPrefs.GetInt("sampleplay") == 1 || PlayerPrefs.GetInt("submitting") == 1)
        {
            thememusic.mute = true;
            canplay = false;
        }
        else
        {
            thememusic.mute = false;
            canplay = true;
        }

       /* if (Input.GetKeyDown(KeyCode.M) && canplay)
        {
            if (thememusic.mute) thememusic.mute = false;
            else thememusic.mute=true;
        }*/
    }

}

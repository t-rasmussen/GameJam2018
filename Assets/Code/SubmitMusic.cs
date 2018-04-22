using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitMusic : MonoBehaviour {
    public int butScene;

    private void OnMouseDown()
    {
        if (PlayerPrefs.GetInt("SceneCur") == butScene)
        {
			//submit recording
			AudioSource source = null;
			string sourceName = "";
			if (PlayerPrefs.GetInt("instruType") == 1)
			{
				source = GlobalState.drumSource;
				sourceName = "drum";
			}

			if (PlayerPrefs.GetInt("instruType") == 2)
			{
				source = GlobalState.violinSource;
				sourceName = "violin";
			}

			if (PlayerPrefs.GetInt("instruType") == 3)
			{
				source = GlobalState.fluteSource;
				sourceName = "flute";
			}

			if (PlayerPrefs.GetInt("instruType") == 4)
			{
				source = GlobalState.hornSource;
				sourceName = "horn";
			}
			byte[] soundbytes = SaveWavFile(source.clip, sourceName);
			UploadSoundFile.UploadFile(soundbytes, sourceName);

			if (PlayerPrefs.GetInt("instruType") == 1 && PlayerPrefs.GetInt("drumDone") == 0)
            {
                PlayerPrefs.SetInt("drumDone", 1);
                PlayerPrefs.SetInt("instruDone", PlayerPrefs.GetInt("instruDone") + 1 );
                //print(PlayerPrefs.GetInt("drumDone"));
            }

            if (PlayerPrefs.GetInt("instruType") == 2 && PlayerPrefs.GetInt("violinDone") == 0 )
            {
                PlayerPrefs.SetInt("violinDone", 1);
                PlayerPrefs.SetInt("instruDone", PlayerPrefs.GetInt("instruDone") + 1);
                //print(PlayerPrefs.GetInt("violinDone"));
            }

            if (PlayerPrefs.GetInt("instruType") == 3 && PlayerPrefs.GetInt("fluteDone") == 0)
            {
                PlayerPrefs.SetInt("fluteDone", 1);
                PlayerPrefs.SetInt("instruDone", PlayerPrefs.GetInt("instruDone") + 1);
                //print(PlayerPrefs.GetInt("fluteDone"));
            }

            if (PlayerPrefs.GetInt("instruType") == 4 && PlayerPrefs.GetInt("hornDone") == 0)
            {
                PlayerPrefs.SetInt("hornDone", 1);
                PlayerPrefs.SetInt("instruDone", PlayerPrefs.GetInt("instruDone") + 1);
                //print(PlayerPrefs.GetInt("hornDone"));
            }
            //print(PlayerPrefs.GetInt("instruDone"));
        }
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

	public byte[] SaveWavFile(AudioClip audioClip, string sourceName)
	{
		string filepath;
		byte[] bytes = WavUtility.FromAudioClip(audioClip, out filepath, true, sourceName);
		///var t = new Tuple<string, byte[]>(filepath, bytes);
		return bytes;
	}
}

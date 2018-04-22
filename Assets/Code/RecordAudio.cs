using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class RecordAudio : MonoBehaviour
{

	bool isRecording = false;
	private AudioSource audioSource;
	private AudioSource currentSource;
	private AudioSource[] audioSources;
	//duration of recording
	private int duration = 1;
	private int clipCount = 0;
	private string fileNames;

	//list of recorded clips...
	List<float[]> recordedClips = new List<float[]>();

	void Start()
	{
		audioSources = GetComponents<AudioSource>(); //GetComponent<AudioSource>();
		audioSource = audioSources[0];
		//var enume = UploadSoundFile.GetText();
		//StartCoroutine(GetFiles());

		WWW www = GetFiles();
		Debug.Log(www.text);
		FileData fileData = JsonUtility.FromJson<FileData>(www.text);
		List<string> files = fileData.files;

		foreach (string file in files)
		{
			WWW filewww = GetFile(file);
			Debug.Log(filewww.text);
			setDownloadedAudioSource(filewww.text);
		} 
		
	}

	private void OnMouseDown()
	{
		if (!isRecording)
		{
			Debug.Log("microphone pos : " + Microphone.GetPosition(null));
			isRecording = true;
			Debug.Log("Recording");
			//audioSource.Stop();
			//Microphone.End(null);
			audioSource.clip = Microphone.Start(null, false, duration, 44100);
			audioSource.loop = false;
			Debug.Log("in while loop");

			while (Microphone.GetPosition(null) < duration * 44100)
			{
				//Debug.Log("in while loop");
			}

			int length = 44100 * duration;
			float[] fullClip = new float[length];
			audioSource.clip.GetData(fullClip, 0);
			recordedClips.Add(fullClip);

			AudioSource source = gameObject.AddComponent<AudioSource>();
			source.clip = AudioClip.Create("recorded clip " + clipCount, length, 1, 44100, false);
			source.clip.SetData(fullClip, 0);
			source.Play();

			if (PlayerPrefs.GetInt("instruType") == 1)
			{
				GlobalState.drumSource = source;
			}

			if (PlayerPrefs.GetInt("instruType") == 2)
			{
				GlobalState.violinSource = source;
			}

			if (PlayerPrefs.GetInt("instruType") == 3)
			{
				GlobalState.fluteSource = source;
			}

			if (PlayerPrefs.GetInt("instruType") == 4)
			{
				GlobalState.hornSource = source;
			}
		/*	byte[] soundbytes = SaveWavFile(source.clip);
			Debug.Log("sound bytes length " + soundbytes.Length);
			Debug.Log("sound float length " + fullClip.Length);
			UploadSoundFile.UploadFile(soundbytes);*/

			//audioSource.Stop();
			//Microphone.End(null);

			Debug.Log("Finished recording");
			isRecording = false;
			clipCount++;
		}
	}

	public static WWW GetFiles(string url = "http://localhost:3000/download", string sessionid = "")
	{
		if (sessionid.Equals(""))
		{
			sessionid = "" + GlobalState.sessionid;
		}

		WWW www = new WWW(url+"?sessionid="+sessionid);

		WaitForSeconds w;
		while (!www.isDone)
			w = new WaitForSeconds(0.1f);

		
		return www;
	}


	public static WWW GetFile(string filename)
	{
		WWW www = new WWW("http://localhost:3000/downloadfile?filename=" + filename + "&sessionid=" + GlobalState.sessionid);

		WaitForSeconds w;
		while (!www.isDone)
			w = new WaitForSeconds(0.1f);


		return www;
	}

	//after getting sound file, set it as audio source on gameobject
	private void setDownloadedAudioSource(string dataFromServer)
	{
		// retrieve data form server as binary data
		//byte[] results = www.downloadHandler.data;
		dataFromServer = dataFromServer.Substring(33);
		dataFromServer = dataFromServer.Substring(0, dataFromServer.Length - 3);
		//Debug.Log("file " + file);
		string[] bytesAsStringArr = dataFromServer.Split(',');
		byte[] bytes = bytesAsStringArr.Select(s => Convert.ToByte(s, 10)).ToArray();

		//convert bytes to audioclip
		AudioSource source = gameObject.AddComponent<AudioSource>();
		source.clip = WavUtility.ToAudioClip(bytes); //AudioClip.Create("downloaded clip", floatArray2.Length, 1, 44100, false);
													 //source.clip.SetData(floatArray2, 0);
		source.Play();
	}



	private void Update()
	{
		/*if (Input.GetKeyDown("space"))
		{
			if (!isRecording)
			{
				Debug.Log("microphone pos : " + Microphone.GetPosition(null));
				isRecording = true;
				Debug.Log("Recording");
				//audioSource.Stop();
				//Microphone.End(null);
				audioSource.clip = Microphone.Start(null, false, duration, 44100);
				audioSource.loop = false;
				Debug.Log("in while loop");
	
				while (Microphone.GetPosition(null) < duration * 44100)
				{
					//Debug.Log("in while loop");
				}

				int length = 44100 * duration;
				float[] fullClip = new float[length];
				audioSource.clip.GetData(fullClip, 0);
				recordedClips.Add(fullClip);

				AudioSource source = gameObject.AddComponent<AudioSource>();
				source.clip = AudioClip.Create("recorded clip " + clipCount, length, 1, 44100, false);
				source.clip.SetData(fullClip, 0);
				source.Play();
				byte[] soundbytes = SaveWavFile(source.clip);
				Debug.Log("sound bytes length " + soundbytes.Length);
				Debug.Log("sound float length " + fullClip.Length);
				UploadSoundFile.UploadFile(soundbytes);

				//audioSource.Stop();
				//Microphone.End(null);

				Debug.Log("Finished recording");
				isRecording = false;
				clipCount ++;
			}
		}

		//use number keys to switch between recorded clips, start from 1!!
		for (int i = 0; i < 10; i++)
		{
			if (Input.GetKeyDown("" + i))
			{
				Debug.Log("Pressed " + i);
				SwitchClips(i - 1);
			}
		}*/
	}


	//chooose which clip to play based on number key..
	void SwitchClips(int index)
	{
		if (index < recordedClips.Count)
		{
			audioSource.Stop();
			int length = recordedClips[index].Length;
			audioSource.clip = AudioClip.Create("recorded sample " + index, length, 1, 44100, false);
			audioSource.clip.SetData(recordedClips[index], 0);
			audioSource.Play();
		}
		//mix recording by clicking numkey key equal to number of clips
		if(index == recordedClips.Count && recordedClips.Count > 1)
		{
			audioSource.Stop();
			AudioSource[] audioSources = GetComponents<AudioSource>();
			for(int i = 1; i < audioSources.Length; i++)
			{
				AudioSource s = audioSources[i];
				s.Play();
			}
		}
	}

	public byte[] SaveWavFile(AudioClip audioClip)
	{
		string filepath;
		byte[] bytes = WavUtility.FromAudioClip(audioClip, out filepath, true);
		///var t = new Tuple<string, byte[]>(filepath, bytes);
		return bytes;
	}


}

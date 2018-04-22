using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UploadSoundFile {


	public static void UploadFile(byte[] bytes, string sourceName)
	{
		WWWForm form = new WWWForm();
		form.AddField("sessionid", GlobalState.sessionid);
		form.AddBinaryData("sound", bytes, sourceName+".wav");

		UnityWebRequest www = UnityWebRequest.Post("http://localhost:3000/upload", form);
		www.SendWebRequest();

		WaitForSeconds w;
		while (!www.isDone)
		{
			w = new WaitForSeconds(0.1f);
		}

		if (www.isNetworkError || www.isHttpError)
		{
			Debug.Log(www.error);
		}
		else
		{
			Debug.Log("Form upload complete!");
		}
	}


	

}

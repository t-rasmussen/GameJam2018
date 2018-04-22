using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVar : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        ResetAll();
    }

    public void ResetAll()
    {
        PlayerPrefs.SetInt("instruType", 0);    //the instrument 1-drums 2-violin 3-flute 4-horn
        PlayerPrefs.SetInt("SceneCur", 0);     //the current Scene
        PlayerPrefs.SetInt("instruDone", 0);     //number instruments submitted 4 max 

        //0-instrument not submitted 1-instrument submitted
        PlayerPrefs.SetInt("drumDone", 0);
        PlayerPrefs.SetInt("violinDone", 0);
        PlayerPrefs.SetInt("fluteDone", 0);     
        PlayerPrefs.SetInt("hornDone", 0);

        PlayerPrefs.SetInt("sampleplay", 0);       //0-user not playing sample 1-user playing sample
        PlayerPrefs.SetInt("submitting", 0);     //0-user not submitting 1-user submitting
    }
}

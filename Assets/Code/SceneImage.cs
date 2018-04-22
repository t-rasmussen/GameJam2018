using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneImage : MonoBehaviour {

    public Sprite newSceneI; // Drag your first sprite here
    public Sprite newScene2;
    public SpriteRenderer viewScene;
    public int butScene;
    public int newScene;
    public int instrChosen;

    private void OnMouseDown()
    {
        if (PlayerPrefs.GetInt("SceneCur") == butScene)
        {
            viewScene.sprite = newSceneI; 
            PlayerPrefs.SetInt("SceneCur", newScene);

            if(butScene == 1)
            {
                PlayerPrefs.SetInt("instruType", instrChosen);
               //print(PlayerPrefs.GetInt("instruType"));
            }

            if ( butScene==2 && PlayerPrefs.GetInt("instruDone") == 4)
            //if (butScene == 2 && )
            {
                viewScene.sprite = newScene2;
                PlayerPrefs.SetInt("SceneCur", 3);
            }

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

}

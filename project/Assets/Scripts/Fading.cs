using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fading : MonoBehaviour {

    public Texture2D fadeOutTexture; //texture to overlay the screen
    public float fadeSpeed = 0.8f;

    private int drawDepth = -1000; //textures order in hierarchy - on top
    private float alpha = 1.0f; //alpha value
    private int fadeDir = -1; //fade direction of scene: in:-1, out:1

    //unuty's functionfor gui
    private void OnGUI()
    {
        //fade in/out
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        //force(clamp) number 0..1 because GUI.color uses 0..1
        alpha = Mathf.Clamp01(alpha);

        //set color of GUI, leave colors and change alpha
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;   //render black texture on top
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture); //draw texture to fit entire screen
    }

    //set fadeDir to the direction parameter
    public float BeginFade (int direction)
    {
        fadeDir = direction;
        return 1.0f / fadeSpeed; //return the fadeDuration, calculated from the fadeSpeed variable so its easy to time the SceneManager.ChangeScene();
    }

    private void OnLevelFinishedLoading()
    {
        //alpha = 1
        BeginFade(-1);
    }
}

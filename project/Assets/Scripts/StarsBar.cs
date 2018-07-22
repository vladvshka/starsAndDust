using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StarsBar : MonoBehaviour {

    private CharacterCTR character;
    private Text starsNum;
    private int goal;


    private void Awake()
    {
        character = FindObjectOfType<CharacterCTR>();
        starsNum = GetComponentInChildren<Text>();
        goal = GameObject.FindGameObjectsWithTag("star").Length; //count number of stars
        Refresh();
    }

    public void Refresh()
    {
        starsNum.text = (character.Stars).ToString();

        if (character.Stars == goal)
        {
            StartCoroutine(ChangeLevel());
        }
        if (starsNum.text == "0")
            starsNum.color = Color.red;
        else starsNum.color = new Color32(0x36, 0xFF, 0x07, 0xFF); // RGBA
    }

    IEnumerator ChangeLevel()
    {
        float fadeTime = GameObject.FindGameObjectWithTag("Finish").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        if ((SceneManager.GetActiveScene().buildIndex + 1) == SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene("MainMenuScene");
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

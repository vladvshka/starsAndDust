using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interface : MonoBehaviour
{
    public Transform canvas;
    public Transform player;

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) Pause();
    }

    public void Pause()
    {
        if (!canvas.gameObject.activeInHierarchy)
        {
            canvas.gameObject.SetActive(true);
            Time.timeScale = 0;
            player.gameObject.SetActive(false);
        }

        else
        {
            canvas.gameObject.SetActive(false);
            Time.timeScale = 1;
            player.gameObject.SetActive(true);
        }
    }
}

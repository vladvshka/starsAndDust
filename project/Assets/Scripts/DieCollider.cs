using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieCollider : MonoBehaviour {

    public AudioClip gameOverSound;

    void OnTriggerEnter2D(Collider2D collider)
    {
        CharacterCTR character = collider.GetComponent<CharacterCTR>();
        if (character)
        {
            SoundManager.instance.PlaySingle(gameOverSound);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }            
    }
}

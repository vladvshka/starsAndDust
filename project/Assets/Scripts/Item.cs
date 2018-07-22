using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public AudioClip eatSound1;
    public AudioClip eatSound2;
    public AudioClip drinkSound1;
    public AudioClip drinkSound2;

    protected virtual void Delete()
    {
        SoundManager.instance.RandomizeSfx(eatSound1, eatSound2, drinkSound1, drinkSound2);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel1 : MonoBehaviour {

    private Animator anim;

    void Awake()
    {
        //Get a component reference to the SpriteRenderer.
        anim = GetComponent<Animator>();
    }

    public void Explode()
    {
        StartCoroutine(Example());
    }

    IEnumerator Example()
    {
        anim.Play("Explode");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}


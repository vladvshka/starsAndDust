﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesBar : MonoBehaviour {

    private Transform[] hearts = new Transform[5];

    private CharacterCTR character;

    private void Awake()
    {
        character = FindObjectOfType<CharacterCTR>();
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i] = transform.GetChild(i);
        }
    }

    public void Refresh()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < character.Lives) hearts[i].gameObject.SetActive(true);
            else hearts[i].gameObject.SetActive(false);
        }
    }
}

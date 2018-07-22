using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  

public class AmmoBar : MonoBehaviour {

    private CharacterCTR character;
    private Text ammoNum;

    private void Awake()
    {
        character = FindObjectOfType<CharacterCTR>();
        ammoNum = GetComponentInChildren<Text>();
        Refresh();
    }

    public void Refresh()
    {
        ammoNum.text = (character.Ammo).ToString();
        if (ammoNum.text == "0")
            ammoNum.color = Color.red;
        else ammoNum.color = new Color32(0x36, 0xFF, 0x07, 0xFF); // RGBA
    }
}

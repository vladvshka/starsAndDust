using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammunation : Item {

    void OnTriggerEnter2D(Collider2D collider)
    {
        CharacterCTR character = collider.GetComponent<CharacterCTR>();
        if (character)
        {
            character.Ammo += 3;
            base.Delete();
        }
    }
}

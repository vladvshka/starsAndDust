using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Item {

    void OnTriggerEnter2D(Collider2D collider)
    {
        CharacterCTR character = collider.GetComponent<CharacterCTR>();
        if (character)
        {
            character.Lives++;
            base.Delete();
        }
    }
}

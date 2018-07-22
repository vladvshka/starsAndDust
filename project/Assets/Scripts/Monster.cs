using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Unit
{
    public AudioClip enemyAttack2;
    [SerializeField]
    private int lives = 3;
    //public int Lives { set { lives = value; } }

    public override void ReceiveDamage()
    {
        lives--;
        SoundManager.instance.RandomizeSfx(enemyAttack2);
        if (lives == 0) base.ReceiveDamage(); 
    }

    //чекает попадание пули 
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        CharacterCTR character = collider.GetComponent<CharacterCTR>();
        if (character)
        {
            character.ReceiveDamage();
        }
    }

    // Use this for initialization
    protected virtual void Start () {}
    // Update is called once per frame
    protected virtual void Update () {} 
    protected virtual void Awake() { }
}

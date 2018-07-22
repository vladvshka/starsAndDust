using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MovableMonster : Monster {

    [SerializeField]
    private float speed = 2.0F;
    [SerializeField]
    private Vector3 direction;

    private SpriteRenderer sprite;
    //вынести в отд класс ссылки на префабы, чтобы они загружались 1 раз и обращ-ся к ним как к статич полям!
    
    //при создании
    protected override void Awake()
    {
        //base.Lives = 4;
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Move()
    {
        //проверяем ближайшие коллайдеры на опред расс-нии
        //OverlapCircleAll вернет массив всех коллайдеров  (вынести что то в коэф-ты!)
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.5F + transform.right * direction.x * 0.4F, 0.3F);
        //если коллайдер не персонаж 
        if (colliders.Length > 1 && colliders.All(x => !x.GetComponent<CharacterCTR>()))
            direction *= -1;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

        sprite.flipX = direction.x < 0.0F;
    }

    // по умолчанию
    protected override void Start()
    {
        direction = transform.right;
    }

    // Update is called once per frame
    protected override void Update()
    {
        Move();
    }

    protected override void OnTriggerEnter2D(Collider2D collider)   
    {      
        CharacterCTR character = collider.GetComponent<CharacterCTR>();
        if (character)  //если прыгает на него перс, сразу уничт-е
        {
            if(Mathf.Abs(character.transform.position.x - transform.position.x) < 0.5F) //расст-е меньше, только если игрок сверху
            {
                Destroy(gameObject);
            }
            else
            {
                character.ReceiveDamage();
            }               
        }
        if (collider.tag == "Obstacle" && (Mathf.Abs(collider.transform.position.x - transform.position.x) < 0.5F))
        {
            Destroy(gameObject);
        }
    }   
}

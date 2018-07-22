using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableMonster : Monster
{
    [SerializeField]
    private float shootRate = 2.0F;
    [SerializeField]
    private Color bulletColor = Color.white;
    [SerializeField]
    private Vector3 direction;    

    private Bullet bullet;
    private SpriteRenderer sprite;
    private Transform player;

    protected override void Awake()
    {
        //загружаем префаб
        bullet = Resources.Load<Bullet>("Bullet");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    // Use this for initialization
    protected override void Start()
    {
        direction = transform.right;
        //повторяющийся вызов стрельбы
        InvokeRepeating("Shoot", shootRate, shootRate);
    }

    // Update is called once per frame
    protected override void Update()
    {
        CheckPosition();
    }

    private void CheckPosition()
    {
        float dir = transform.position.x - player.position.x;
        if (dir > 0)
        {
            direction = transform.right * -1.0F;           
        }
        else if (dir < 0)
        {
            direction = transform.right;
        }
        sprite.flipX = direction.x < 0.0F;
    }

    private void Shoot()
    {
        //пуля созд-ся в позиции
        Vector3 position = transform.position;
        //сдвинуть с позиции отсчета персонажа(под ногами)
        position.y += 0.8F;
        //position.x += 1.0F;
        //создаем пулю клонир-ем префаба, заданием позиции и ротации (по умолч)
        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation);
        //задаем напр-е пули через публичное св-во
        newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -1.0F : 1.0F);  //direction!
        newBullet.Parent = gameObject;
        newBullet.Color = bulletColor;
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        CharacterCTR character = collider.GetComponent<CharacterCTR>();
        if (character)  //если прыгает на него перс, сразу уничт-е
        {
            if (Mathf.Abs(character.transform.position.x - transform.position.x) < 0.3F) //расст-е меньше, только если игрок сверху
            {
                Destroy(gameObject);
            }
            else
            {
                character.ReceiveDamage();
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterCTR : Unit
{
    public AudioClip moveSound1;
    public AudioClip moveSound2;
    public AudioClip gameOverSound;
    public AudioClip enemyAttack1;
    public AudioClip shoot1;
    public AudioClip shoot2;

    [SerializeField]
    private int lives = 5;
    [SerializeField]
    private int ammo = 5;
    [SerializeField]
    private float speed = 3.0F;
    [SerializeField]
    private float jumpForce = 15.0F;

    private int stars = 0;
    private bool isGrounded = false; //приземленность объекта. для прыжка
    //ссылка на префаб пули
    private Bullet bullet;

    private CharacterState State  //св-во, связ-щее Animator State с enum CharState
    {
        get { return (CharacterState)animator.GetInteger("State"); } //получить знач-е параметра State аниматора
        set { animator.SetInteger("State", (int)value); }
    }

    public int Lives {
        get { return lives; }
        set
        {
            if (value < 6) lives = value;
            lb.Refresh();
        }
    }

    public int Ammo
    {
        get { return ammo; }
        set
        {
            if (value < 21) ammo = value;
            ab.Refresh();
        }
    }

    public int Stars
    {
        get { return stars; }
        set
        {
            stars = value;
            sb.Refresh();
        }
    }

    //ссылки на компоненты
    private StarsBar sb;
    private AmmoBar ab;
    private LivesBar lb;
    new private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer sprite; //для доступа к св-ву flip: инверсия спрайтов

    // Use this for initialization
    private void Awake()
    {
        //получим ссылки на компоненты
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        bullet = Resources.Load<Bullet>("Bullet"); //from dir Resources load type Bullet and same name
        lb = FindObjectOfType<LivesBar>();
        ab = FindObjectOfType<AmmoBar>();
        sb = FindObjectOfType<StarsBar>();
    }

    //метод для физики, вызываемый через фиксир. фремя
    private void FixedUpdate()
    {
        CheckGround();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isGrounded) State = CharacterState.Idle;
        if (Input.GetButton("Horizontal")) Run();
        if (isGrounded && Input.GetButtonDown("Jump")) Jump(); //space in InputManager
        if (Input.GetKeyDown(KeyCode.LeftControl)) Shoot();    

        if (State == CharacterState.Run)
        {
            SoundManager.instance.walkSource.mute = false;
        }
        else SoundManager.instance.walkSource.mute = true;
    }

    private void Run()
    {
        //X-axis movement direction
        //GetAxis returnes 1/-1 when we push arrow buttons right/left (d/a)
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        //X-axis movement
        //MoveTowards(from where, where to, distance in 1 frame)
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

        //flip sprite depending on direction (flipX : true(left), false(right))
        sprite.flipX = direction.x < 0.0F;

        if (isGrounded) State = CharacterState.Run;
    }

    private void Jump()
    {
        //AddForce(длина вектора силы, тип силы)
        rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        SoundManager.instance.efxSource.Stop();
        SoundManager.instance.RandomizeSfx(enemyAttack1);
    }

    private void CheckGround()
    {
        //OverlapCircleAll проверяет есть ли в опред радиусе вокруг точки отсчета 
        //коллайдеры и возвращает их OverlapCircleAll()
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3F);
        isGrounded = colliders.Length > 1;  //в массив всегда будет попадать собствен. коллайдер объекта

        if (!isGrounded) State = CharacterState.Jump;
    }

    private void Shoot()
    {
        if (Ammo > 0)
        {
            //задать перезаррядку
            //пуля созд-ся в позиции
            Vector3 position = transform.position;
            //сдвинуть с позиции отсчета персонажа(под ногами)
            position.y += 0.8F;
            //position.x += 1.0F;
            //создаем пулю клонир-ем префаба, заданием позиции и ротации (по умолч)
            Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation);
            //задаем напр-е пули через публичное св-во
            newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -1.0F : 1.0F);
            newBullet.Parent = gameObject;
            Ammo--;
            SoundManager.instance.RandomizeSfx(shoot1, shoot2);

        }
    }

    public override void ReceiveDamage()
    {
        Lives--;
        SoundManager.instance.RandomizeSfx(enemyAttack1);

        if (Lives == 0)
        {
            SoundManager.instance.PlaySingle(gameOverSound);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);    
        }

        //обнуляем ускорение тела (тк иначе вектор силы тяжести > силы, ооталкивающей перса при коллизии)
        rigidbody.velocity = Vector3.zero;
        //отталкиваем персонажа от текущей позиции применяя импус
        rigidbody.AddForce(transform.up * 8.0F, ForceMode2D.Impulse);
    }
}

//перечисление с возможными состояниями персонажа
public enum CharacterState
{
    Idle, Run, Jump
}



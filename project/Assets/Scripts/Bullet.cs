using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private float speed = 10.0F;
    //bullet creater
    private GameObject parent;
    public GameObject Parent { set { parent = value; } get { return parent; } }

    public Color Color { set { sprite.color = value; } }
    private Vector3 direction;
    public Vector3 Direction { set { direction = value; } }

    private SpriteRenderer sprite; //для доступа к св-ву color

    // Use this for initialization
    private void Awake ()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
	
	// Update is called once per frame
	private void Update ()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        //flip sprite depending on direction
        sprite.flipX = direction.x < 0.0F;
    }

    private void Start()
    {
        Destroy(gameObject, 1.5F);
    }

    //чекает попадание пули и удаляет ее  - метод рефлексия
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>(); //проверяем с юнитом ли коллизия

        if (unit && unit.gameObject != parent)
        {
            Debug.Log(unit.name);
            Destroy(gameObject);
            unit.ReceiveDamage();
        }

        Barrel1 bar = collider.GetComponent<Barrel1>();
        if (bar)
        {
            bar.Explode();
        }
    }
}


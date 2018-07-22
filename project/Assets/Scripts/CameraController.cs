using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField]
    private float speed = 3.0F;
    [SerializeField]
    private Transform target; //целевой объект камеры

    private void Awake()
    {
        if (!target) target = FindObjectOfType<CharacterCTR>().transform;
    }

    // Update is called once per frame, moves camera
    private void Update ()
    {
        //камера по Oz должна быть на расст. от остальной сцены, иначе сольется с ней
        Vector3 targPos = target.position;
        targPos.z = -10.0F;
        //сглаживает движ-е камеры, на расст-е = скорость*время между кадрами
        transform.position = Vector3.Lerp(transform.position, targPos, speed * Time.deltaTime);
	}
}

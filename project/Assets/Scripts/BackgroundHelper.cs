using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundHelper : MonoBehaviour {

    [SerializeField]
    private float speed = 0.0F; //для настройки скорости в инспекторе
    private float pos = 0.0F; //переменная для позиции картинки
    private RawImage image; //создаем объект нашей картинки

    // Use this for initialization
    void Start () {
        image = GetComponent<RawImage>();
	}
	
	// Update is called once per frame
	void Update () {
        //в апдейте прописываем как, с какой скоростью и куда мы будем двигать нашу картинку
        pos += speed;
        if (pos > 1.0F)
            pos -= 1.0F;
        image.uvRect = new Rect(pos, 0, 1, 1);
    }
}

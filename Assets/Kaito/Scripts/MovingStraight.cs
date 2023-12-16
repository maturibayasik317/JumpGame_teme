using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----真っ直ぐ飛んでくるオブジェクトのスクリプト----
public class MovingStraight : MonoBehaviour
{
    [SerializeField] float speed; // 移動速度
    [SerializeField] float rotationSpeed; // 回転速度

    SpriteRenderer sr = null;
    bool isView = false;

    void Start()
    {
        sr = GetComponent <SpriteRenderer>();
    }

    void Update()
    {
        if (sr.isVisible) // 画面内なら
        {
            isView = true;
            if (isView)
            {
                transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
                transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            isView = false;
        }
    }
}

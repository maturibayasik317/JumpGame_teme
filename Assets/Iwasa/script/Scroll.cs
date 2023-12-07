using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    //背景とかの動かしたいものに入れてください
    [SerializeField] private float speed;

    void Update()
    {
        //背景もしくはグラウンドにコンポーネントしといて、Serializeで設定したからスピードの設定も忘れずに
        transform.position -= new Vector3(Time.deltaTime * speed,0);
        if(transform.position.x <= -8.9f)
        {
            transform.position = new Vector3(8.9f,0);
        }
    }
}

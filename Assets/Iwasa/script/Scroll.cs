using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    //背景とかの動かしたいものに入れてください
    [SerializeField] private float speed;

    Vector3  Startpos;
    Jump_Dash playerScript;

    private void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<Jump_Dash>();
        Startpos = transform.position;
    }


    void Update()
    {
        if (playerScript.GetIsClear || playerScript.GetIsDead) return;

        //背景もしくはグラウンドにコンポーネントしといて、Serializeで設定したからスピードの設定も忘れずに
        transform.position -= new Vector3(Time.deltaTime * speed, Startpos.y);
        if(transform.position.x <= -8.9f);

    }
}

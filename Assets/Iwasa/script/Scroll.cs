using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    //背景とかの動かしたいものに入れてください
    [SerializeField] private float speed;

    Vector3  Startpos;
    Jump_Dash playerScript;
    float elapsedTime = 0;

    private void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<Jump_Dash>();
        Startpos = transform.position;
    }


    void Update()
    {
        const float time = 2.0f;
        if (playerScript.GetIsDead || elapsedTime > time) return;

        if (playerScript.GetIsClear)
        {
            elapsedTime += Time.deltaTime;
        }

        //背景もしくはグラウンドにコンポーネントしといて、Serializeで設定したからスピードの設定も忘れずに
        transform.position -= new Vector3(Time.deltaTime * speed, Startpos.y);
        if(transform.position.x <= -8.9f);

    }
}

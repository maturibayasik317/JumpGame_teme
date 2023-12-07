using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jump_Dash : MonoBehaviour
{   //プレイヤーにコンポーネントしてください
    private Vector2 velocity;
    [SerializeField] float Jump_Power;//ジャンプ力
    private int Jump_Count =1;
    private int JumpCount = 0;
    private bool jump = false;
    Rigidbody2D rigid2D;
    [SerializeField] ContactFilter2D filter2D;

    bool dash = false;


    public GameObject text;

    void MoveDash()
    {
        //ここをelseにしてif(ジャンプ中にスペースを押すとって感じに書き直す)
        if(Input.GetKey(KeyCode.Space)) 
        { 
            transform.position+=  new Vector3(0.1f,0,0);
        }
        dash = false;
    }

    void MoveNormal()
    {
        if( Input.GetKeyDown(KeyCode.Space))
            dash = true;
    }

    private void Update()
    {
        //スペースでジャンプ
        if ( JumpCount < Jump_Count && Input.GetKeyDown (KeyCode.Space))
        {
            jump = true;
        }
        if (jump)
        { 
            this.rigid2D = GetComponent<Rigidbody2D>();
            rigid2D.velocity = Vector2.zero;

            //ジャンプさせる
            this.rigid2D.AddForce(transform.up * Jump_Power);
            JumpCount++;

            jump = false;
        }
        if (dash)//ダッシュ移動
        {
            MoveDash();
        }
        else//通常状態
        {
            MoveNormal();
        }
        if (GetComponent<Rigidbody2D>().IsTouching(filter2D))
        {
            //地面についたら終わったらもう一回できるようにする
            JumpCount = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "HitBox")
        {
            //プレイヤーを消去
            Destroy(this.gameObject);
            //ゲームオーバーテキストの表示
            //IwasaのところにPrefabとしてゲームオーバーテキストお用意したのでInspecterに入れてください
            text.SetActive(true);
        }
    }
}

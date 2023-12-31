using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----ギミックのテスト用----
public class PlayerTest : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;

    bool dash = false;

    // 読み取り専用プロパティ
    public bool GetDash => dash;

    void Start()
    {
        
    }

    void Update()
    {
        //transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;

        

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
            dash = true;
        }
        else
        {
            dash = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("jump");
            rb.AddForce(Vector2.up * jumpForce);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HitBox"))
        {
            Destroy(gameObject);
            Debug.Log("ゲームオーバー");
        }
    }
}

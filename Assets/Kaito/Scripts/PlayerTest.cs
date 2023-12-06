using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----ギミックのテスト用----
public class PlayerTest : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(new Vector2(speed, 0) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(new Vector2(-speed, 0) * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("jump");
            rb.AddForce(Vector2.up * jumpForce);
        }
    }
}

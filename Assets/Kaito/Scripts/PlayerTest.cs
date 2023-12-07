using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----�M�~�b�N�̃e�X�g�p----
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
        transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("jump");
            rb.AddForce(Vector2.up * jumpForce);
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HitBox"))
        {
            Destroy(gameObject);
            Debug.Log("�Q�[���I�[�o�[");
        }
    }*/
}

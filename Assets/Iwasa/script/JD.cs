using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JD : MonoBehaviour
{
    private int Jump_Count = 1;
    private int JumpCount = 0;
    private bool jump = false;
    private bool dash = false;
    private Rigidbody2D rigid2D;

    [SerializeField] float Jump_Power;
    [SerializeField] ContactFilter2D filter2D;

    public GameObject text;

    void MoveDash()
    {
        // �X�y�[�X�������Ă��W�����v���_�b�V��������������
        transform.position += new Vector3(5f, 0, 0);
        dash = false;
    }

    void MoveNormal()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dash = true;
        }
    }

    private void Update()
    {
        // �X�y�[�X�ŃW�����v
        if (JumpCount < Jump_Count && Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }

        if (jump)
        {
            this.rigid2D = GetComponent<Rigidbody2D>();
            rigid2D.velocity = Vector2.zero;

            // �W�����v������
            this.rigid2D.AddForce(transform.up * Jump_Power);
            JumpCount++;

            // jump �t���O�� true �ɐݒ肷��^�C�~���O��ύX
            // �W�����v���ɃX�y�[�X�������ƃ_�b�V��������������
            jump = Input.GetKey(KeyCode.Space);

            // �X�y�[�X�������Ă��W�����v���_�b�V��������������
            if (dash)
            {
                MoveDash();
            }
        }
        else // �ʏ���
        {
            MoveNormal();
        }

        if (GetComponent<Rigidbody2D>().IsTouching(filter2D))
        {
            // �n�ʂɂ�����I�������������ł���悤�ɂ���
            JumpCount = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "HitBox")
        {
            // �v���C���[������
            Destroy(this.gameObject);
            // �Q�[���I�[�o�[�e�L�X�g�̕\��
            text.SetActive(true);
        }
    }
}

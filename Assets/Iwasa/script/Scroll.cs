using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    //�w�i�Ƃ��̓������������̂ɓ���Ă�������
    [SerializeField] private float speed;

    void Update()
    {
        //�w�i�������̓O���E���h�ɃR���|�[�l���g���Ƃ��āASerialize�Őݒ肵������X�s�[�h�̐ݒ���Y�ꂸ��
        transform.position -= new Vector3(Time.deltaTime * speed,0);
        if(transform.position.x <= -8.9f)
        {
            transform.position = new Vector3(8.9f,0);
        }
    }
}

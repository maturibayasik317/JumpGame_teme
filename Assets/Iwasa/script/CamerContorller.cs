using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerContorller : MonoBehaviour
{
    //�J������add���Ă�������
    //add������v���C���[�̉��ɓ���Ă�������
    float y = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.y = y;
        transform.position = pos; 

    }
}

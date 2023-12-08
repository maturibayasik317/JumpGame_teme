using UnityEngine;
public class RotateSample : MonoBehaviour
{
    [SerializeField ]
    private float _rotateSpeed = 100f;
    private float angle;
    private void Update()
    {
        angle += _rotateSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
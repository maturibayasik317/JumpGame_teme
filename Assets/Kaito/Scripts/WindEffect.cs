using UnityEngine;

//----プレイヤーが存在する間常に吹いている風のエフェクト----
/// <summary>
/// 風エフェクトを発生させるクラス
/// </summary>
public class WindEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;
    [SerializeField] GameObject player;

    void Start()
    {
        particle.Play();
    }

    void Update()
    {
        if (!player)
        {
            particle.Stop();
        }
    }
}

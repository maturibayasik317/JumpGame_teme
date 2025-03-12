using UnityEngine;

/// <summary>
/// 動く床クラス
/// </summary>
public class MovingFloor : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;

    // 動く軸を設定（インスペクター上でチェックを入れる）
    [SerializeField] bool xAxis;
    [SerializeField] bool yAxis;

    [SerializeField] float moveTime; // 一方向への移動時間
    [SerializeField] float speed; // 移動速度

    Vector2 oldPos = Vector2.zero; // 直前の位置を取得
    Vector2 velocity = Vector2.zero;
    Vector2 addPlayerVelocity = Vector2.zero; // プレイヤーに加算する速度
    float elapsedTime = 0; // 時間計測用

    /// <summary>
    /// プレイヤーに加算する速度を返す関数
    /// </summary>
    /// <returns></returns>
    public Vector2 GetVelocity() { return addPlayerVelocity; }

    void Start()
    {
        oldPos = transform.position;
    }

    void FixedUpdate()
    {
        if (sr.isVisible) // 画面内なら
        {
            Move();
        }
    }

    /// <summary>
    /// 床を動かす関数
    /// </summary>
    void Move()
    {
        elapsedTime += Time.deltaTime; // 経過時間

        // x軸方向に移動
        if (xAxis)
        {
            // 速度
            velocity.x = speed * Time.deltaTime;
            transform.position += (Vector3)velocity;

            if (elapsedTime >= moveTime)
            {
                // 逆方向に移動
                speed *= -1;
                elapsedTime = 0;
            }

            // 進んだ距離を出し、時間で割る
            addPlayerVelocity.x = (transform.position.x - oldPos.x) / Time.deltaTime;
            oldPos.x = transform.position.x; // 直前の床の位置を保存
        }

        // y軸方向に移動
        if (yAxis)
        {
            // 速度
            velocity.y = speed * Time.deltaTime;
            transform.position += (Vector3)velocity;
            
            if (elapsedTime >= moveTime)
            {
                // 逆方向に移動
                speed *= -1;
                elapsedTime = 0;
            }

            addPlayerVelocity.y = (transform.position.y - oldPos.y) / Time.deltaTime;
            oldPos.y = transform.position.y;
        }
    }
}

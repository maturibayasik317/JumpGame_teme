using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFollowPlayer: MonoBehaviour
{
	[SerializeField]
	[Tooltip("発生させるエフェクト(パーティクル)")]
	private ParticleSystem particle;

	/// <summary>
	/// 衝突した時
	/// </summary>
	/// <param name="collision"></param>
	private void OnCollisionEnter(Collision collision)
	{
		// 当たった相手が"Player"タグを持っていたら
		if (collision.gameObject.tag == "Player")
		{
			// パーティクルシステムのインスタンスを生成する。
			ParticleSystem newParticle = Instantiate(particle);
			// 生成したパーティクルのpositionをプレイヤーと同じにする。
			newParticle.transform.position = collision.gameObject.transform.position;
			// 生成したパーティクルをプレイヤーの子オブジェクトとする。
			newParticle.transform.parent = collision.gameObject.transform;
			// パーティクルを発生させる。
			newParticle.Play();
			// インスタンス化したパーティクルシステムのGameObjectを削除する。(任意)
			// ※第一引数をnewParticleだけにするとコンポーネントしか削除されない。
			Destroy(newParticle.gameObject, 5.0f);
		}
	}
}


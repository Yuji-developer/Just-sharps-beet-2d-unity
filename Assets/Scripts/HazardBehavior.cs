using UnityEngine;
using System.Collections; // コルーチン用

public class HazardBehavior : MonoBehaviour
{
    public float warningTime = 6f; // 警告時間（実体化するまでの時間）
    public float lifeTime = 2f;

    private Collider2D col;
    private SpriteRenderer sr;

    void Start()
    {
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();

        // 1. 最初は当たり判定を消す
        col.enabled = false;

        // 2. 色を半透明にする（警告色）
        Color c = sr.color;
        c.a = 0.3f; // 透明度30%
        sr.color = c;

        // 3. 警告アニメーションを開始
        StartCoroutine(ActivateHazard());

        // 寿命の設定
        Destroy(gameObject, lifeTime + warningTime);
    }

    IEnumerator ActivateHazard()
    {
        // 警告時間だけ待つ
        yield return new WaitForSeconds(warningTime);

        // === 実体化 ===
        col.enabled = true; // 当たり判定ON

        // 色を元に戻す（不透明）
        Color c = sr.color;
        c.a = 1.0f;
        sr.color = c;

        // ここで「ドーン！」という効果音を鳴らすと最高です
    }
}

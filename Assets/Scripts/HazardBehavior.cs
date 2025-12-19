using UnityEngine;
using System.Collections; // コルーチン用

public class HazardBehavior : MonoBehaviour
{
    public float warningTime = 6f; // 警告時間（実体化するまでの時間）
    public float lifeTime = 2f;

    private Collider2D col;
    private SpriteRenderer sr;


    public IEnumerator ActivateHazard()
    {
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();

        // 1. 最初は当たり判定を消す
        col.enabled = false;
        this.gameObject.SetActive(false);

        // 2. 色を半透明にする（警告色）
        Color c = sr.color;
        c.a = 0.3f; // 透明度30%
        sr.color = c;

        // 警告時間だけ待つ
        yield return new WaitForSeconds(warningTime);

        // === 実体化 ===
        col.enabled = true; // 当たり判定ON

        this.gameObject.SetActive(true);
        // 色を元に戻す（不透明）
        c = sr.color;
        c.a = 1.0f;
        sr.color = c;

        // ここで「ドーン！」という効果音を鳴らすと最高です

        yield return new WaitForSeconds(lifeTime);
        this.gameObject.SetActive(false);

        yield break;
    }
}

using UnityEngine;
using System.Collections; // コルーチン用



public class HazardBehavior : MonoBehaviour, IEnemy
{
    public float warningTime = 6f; // 警告時間（実体化するまでの時間）
    public float lifeTime = 2f;
    public float activeateTime = 1f;

    private Collider2D col;
    private SpriteRenderer sr;

    private EnemyStatus _enemyStatus;

    public EnemyStatus GetEnemyStatus() => _enemyStatus;


    public IEnumerator ActivateHazard()
    {
        _enemyStatus = EnemyStatus.Inactive;
        this.gameObject.SetActive(false);
        yield return new WaitForSeconds(activeateTime);

        this.gameObject.SetActive(true);
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        col.enabled = false;
      

        
        // 2. 色を半透明にする（警告色）
        Color c = sr.color;
        c.a = 0.3f; // 透明度30%
        sr.color = c;


        
        // activeteTime
        yield return new WaitForSeconds(warningTime);

        _enemyStatus = EnemyStatus.Active;
        col.enabled = true;

        
        // 色を元に戻す（不透明）
        c = sr.color;
        c.a = 1.0f;
        sr.color = c;

        // ここで「ドーン！」という効果音を鳴らすと最高です

        yield return new WaitForSeconds(lifeTime);
        _enemyStatus = EnemyStatus.Inactive;
        col.enabled = false;
        this.gameObject.SetActive(false);

        yield break;
    }
}

using UnityEngine;
using DG.Tweening;

public class SphereEnemyBehavior : MonoBehaviour, IEnemy
{
    [Header("�����̐ݒ�")]
    public Vector2 moveDirection = Vector2.left;
    public float moveSpeed = 8f;
    public float lifeTime = 6f;
    public float appearDuration = 0.5f;

    private Rigidbody2D rb;
    private Collider2D col;
    private MeshRenderer mr;

    private EnemyStatus _enemyStatus;

    public EnemyStatus GetEnemyStatus() => _enemyStatus;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        mr = GetComponent<MeshRenderer>();
    }

    void Start()
    {

        if (col != null) col.enabled = false;
        if (mr != null) mr.enabled = false;
        _enemyStatus = EnemyStatus.Inactive;

        transform.localScale = Vector3.zero;


        StartCoroutine(AppearSequence());
    }

    System.Collections.IEnumerator AppearSequence()
    {

        yield return new WaitForSeconds(1.0f);


        if (mr != null) mr.enabled = true;
        _enemyStatus = EnemyStatus.Active;


        transform.DOScale(Vector3.one * 0.5f, appearDuration)
                   .SetEase(Ease.OutBack)
                   .OnComplete(() => {
                       if (col != null) col.enabled = true;
                       _enemyStatus = EnemyStatus.Active;


                       if (rb != null)
                       {
                           rb.linearVelocity = moveDirection.normalized * moveSpeed;
                       }


                       Destroy(gameObject, lifeTime);
                   });
    }
}
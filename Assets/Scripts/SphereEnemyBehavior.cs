using UnityEngine;
using DG.Tweening;

public class SphereEnemyBehavior : MonoBehaviour
{
    [Header("�����̐ݒ�")]
    public Vector2 moveDirection = Vector2.left; // ���ł��������i�����l�͍��j
    public float moveSpeed = 8f; // �����i�l�p��菭���x���Ă�OK�j
    public float lifeTime = 6f;   // ���b��ɏ����邩�i�l�p��菭�����߂ł�OK�j
    public float appearDuration = 0.5f; // �o�����̃A�j���[�V��������

    private Rigidbody2D rb;
    private Collider2D col; // �����蔻��p
    private MeshRenderer mr; // �F�ⓧ���x��ς��邽�߁i3D�I�u�W�F�N�g�Ȃ̂�MeshRenderer�j

    void Awake() // Start��菭�������Ă΂��
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        mr = GetComponent<MeshRenderer>();
    }

    void Start()
    {
       
        if (col != null) col.enabled = false;
        if (mr != null) mr.enabled = false;

        transform.localScale = Vector3.zero; 

     
        StartCoroutine(AppearSequence());
    }

    System.Collections.IEnumerator AppearSequence()
    {
       
        yield return new WaitForSeconds(1.0f); 

        
        if (mr != null) mr.enabled = true; 

        
        transform.DOScale(Vector3.one * 0.5f, appearDuration)
                   .SetEase(Ease.OutBack) 
                   .OnComplete(() => {
                       if (col != null) col.enabled = true; 

                      
                       if (rb != null)
                       {
                           rb.linearVelocity = moveDirection.normalized * moveSpeed;
                       }

                     
                       Destroy(gameObject, lifeTime);
                   });
    }
}

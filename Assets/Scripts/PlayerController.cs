using UnityEngine;
using System.Collections; // Coroutine (IEnumerator) を使うために必要
using DG.Tweening;
using System.Runtime.InteropServices.WindowsRuntime;

public class PlayerController : MonoBehaviour
{
    // === 設定パラメータ (Inspectorで調整可能) ===
    [Header("Movement Settings")]
    public float moveSpeed = 5f;        // 通常時の移動速度
    public float dashSpeed = 20f;       // ダッシュ時の移動速度
    
    [Header("Dash Settings")]
    public float dashDuration = 0.15f;  // ダッシュの持続時間（短い無敵時間）
    public float dashCooldown = 1f;     // ダッシュのクールダウン時間
    public Color dashColor = new Color(1f, 1f, 1f, 0.5f); // ダッシュ時の色と透明度 (半透明の白)
    public GameObject gameOverUI;

    // === プライベート変数 ===
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Vector2 moveInput;
    private bool isDashing = false;     // ダッシュ中かどうか
    private float dashTimeLeft;
    private float lastDashTime = -10f;  // 最後にダッシュした時間

    public GameObject gameOverObejct;

    // === 初期化処理 ===
    void Start()
    {
        // 必須コンポーネントの取得
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        // ダッシュ中の色変化のために、初期の色を保存しておく
        // (ここではシンプルにするため、Startでは特に何もしません)
        gameOverObejct.SetActive(false);
    }

    // === フレームごとの処理 ===
    void Update()
    {
        // 1. 入力の取得
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;

        // 2. ダッシュ入力のチェック
        // Spaceキーが押され、かつクールダウンが終了しているかチェック
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastDashTime + dashCooldown)
        {
            StartDash();
        }
    }

    // === 物理演算の処理 (移動処理) ===
    void FixedUpdate()
    {
        if (isDashing)
        {
            // ダッシュ中の移動
            rb.linearVelocity = moveInput * dashSpeed;
            dashTimeLeft -= Time.fixedDeltaTime;

            // ダッシュ終了の判定
            if (dashTimeLeft <= 0)
            {
                EndDash();
            }
        }
        else
        {
            // 通常の移動
            rb.linearVelocity = moveInput * moveSpeed;
        }
    }

    // === ダッシュ開始処理 ===
    void StartDash()
    {
        isDashing = true;
        dashTimeLeft = dashDuration;
        lastDashTime = Time.time;
        
        // **色変更**：ダッシュカラーに変更
        sr.color = dashColor;
    }

    // === ダッシュ終了処理 ===
    void EndDash()
    {
        isDashing = false;
        rb.linearVelocity = Vector2.zero; // ダッシュ後の慣性を止める
        
        // **色変更**：元の不透明な色に戻す（ここでは白を想定）
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1.0f);
        
        // ※もし元の色を保存しておきたい場合は、Start()で `originalColor = sr.color;` などとしておき、ここで `sr.color = originalColor;` としてください。
    }


    // === 当たり判定処理 ===
    // 物理的な衝突（Is Triggerにチェックなし）が発生したときに呼ばれる

    void OnTriggerEnter2D(Collider2D other)
    {
        // ダッシュ中（isDashing == true）ならダメージを受けない処理をここに書くと本格的になります
        if (isDashing) return;
        if (!other.gameObject.CompareTag("Hazard")) return;
        

        IEnemy enemy = other.gameObject.GetComponent<IEnemy>();
        if (enemy.GetEnemyStatus() == EnemyStatus.Inactive) return;


        Debug.Log("Game Over!");
        gameOverObejct.SetActive(true);
            // ここでシーンのリロードや爆発エフェクトなどを呼ぶ
            Destroy(gameObject);

            if (other.gameObject.CompareTag("Hazard"))
            {
                if (gameOverUI != null)
                {
                    gameOverUI.SetActive(true);
                }
            }
            Destroy(gameObject);
        }
}




using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // === Inspectorで設定する変数 ===
    [Header("参照")]
    public Conductor conductor;          // Conductorスクリプト（拍数管理）
    public GameObject[] hazardPrefabs;    // 敵のPrefabのリスト（様々な種類の敵）
    public Transform spawnPointRight;    // 敵が出現する右側の位置

    [Header("テスト設定")]
    public int beatOffset = 4;           // 最初に出現を開始する拍数（例: 4拍目から）

    // 内部変数
    private int lastBeat = -1; // 最後に処理した拍数（同じ拍で二重に出現させるのを防ぐ）

    // === フレームごとの処理 ===
    void Update()
    {
        // Conductorから現在の拍数（小数点以下を含む）を取得し、整数に変換
        int currentBeat = (int)conductor.songPositionInBeats;

        // 「拍が変わった瞬間」だけ処理をする
        if (currentBeat > lastBeat)
        {
            // === 譜面データ（8-bit_Aggressive1用） ===

            // 0拍目（曲のスタート）は処理しないことが多いので、1拍目以降で考える
            if (currentBeat == 0)
            {
                lastBeat = currentBeat;
                return;
            }

            // フェーズ 1: 基本リズム (1拍ごとの連打)
            // 1拍目から8拍目まで、1拍ごとに普通の敵を出す（8連打）
            if (currentBeat >= 1 && currentBeat <= 8)
            {
                SpawnEnemy(0); // Element 0: 普通の敵
            }

            // フェーズ 2: タメと加速 (4拍ごと)
            // 12拍目、16拍目: 4拍ごとに速い敵を出す
            else if (currentBeat == 12 || currentBeat == 16)
            {
                SpawnEnemy(1); // Element 1: 速い敵
            }

            // フェーズ 3: 巨大な障害物 (8拍ごと)
            // 24拍目、32拍目: 8拍ごとに巨大な敵を出す
            else if (currentBeat % 8 == 0)
            {
                SpawnEnemy(2); // Element 2: 巨大な敵
            }

            // フェーズ 4: 複合パターン (40拍目以降の繰り返し)
            // 2拍ごとにランダムな敵を出す (高速な攻撃)
            else if (currentBeat >= 40 && currentBeat % 2 == 0)
            {
                // 敵のリストの0番目と1番目 (普通/速い) のどちらかをランダムで選ぶ
                int randomIndex = Random.Range(0, 2);
                SpawnEnemy(randomIndex);
            }

            // === 処理終わり ===

            // 最後に処理した拍を更新
            lastBeat = currentBeat;

            if (currentBeat >= 44 && currentBeat % 5 == 0)
            {
                SpawnEnemy(3); // Element 3 に登録した球体敵を出す
            }

            // 例: 50拍目: 速い敵と球体を同時に出す
            if (currentBeat == 50)
            {
                SpawnEnemy(1); // 速い四角
                SpawnEnemy(3); // 球体
            }
        }
    }

    // === 敵を生成する関数 ===
    // index: hazardPrefabsリストの何番目の敵を出すか
    void SpawnEnemy(int index)
    {
        // 出現位置とPrefabがInspectorで設定されているかチェック
        if (spawnPointRight == null || hazardPrefabs.Length == 0)
        {
            Debug.LogError("EnemySpawnerの設定が不足しています！");
            return;
        }

        // 指定された番号がリストの範囲内かチェック
        if (index >= 0 && index < hazardPrefabs.Length)
        {
            // 敵を生成 (Instantiate)
            Instantiate(hazardPrefabs[index], spawnPointRight.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning($"敵のPrefabインデックス {index} はリストの範囲外です。");
        }
    }
}

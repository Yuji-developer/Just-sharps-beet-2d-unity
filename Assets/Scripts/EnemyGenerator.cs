using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyGenerator : MonoBehaviour
{
    // インスペクターでこのリストに敵の出現スケジュールを並べる
    public List<EnemyGroup> spawnSchedule = new List<EnemyGroup>();

    private int currentGroupIndex = 0; // 次に処理するグループの番号
    public float currentBeat = 0;      // 現在の拍数（または移動距離）

    void Update()
    {
        // 全てのスケジュールが終わっていないか確認
        if (currentGroupIndex < spawnSchedule.Count)
        {
            // 現在のタイミングが、設定した出現時間を超えたか判定
            if (currentBeat >= spawnSchedule[currentGroupIndex].beatTime)
            {
                // コルーチンを使って、指定された数と間隔で敵を生成開始
                StartCoroutine(SpawnGroup(spawnSchedule[currentGroupIndex]));
                // 次のグループへ進む
                currentGroupIndex++;
            }
        }

        // テスト用：時間を進める（実際は曲の進行に合わせる）
        currentBeat += Time.deltaTime;
    }

    IEnumerator SpawnGroup(EnemyGroup group)
    {
        for (int i = 0; i < group.count; i++)
        {
            // 敵を生成
            Instantiate(group.enemyPrefab, transform.position, Quaternion.identity);
            // 指定された間隔（interval）だけ待機
            yield return new WaitForSeconds(group.interval);
        }
    }
}

using UnityEngine;

public class PulseOnBeat : MonoBehaviour
{
    public Conductor conductor; // Conductorをドラッグ＆ドロップ
    public float pulseIntensity = 1.2f; // どれくらい大きくなるか
    public float returnSpeed = 5f;      // 元のサイズに戻る速さ

    private Vector3 initialScale;

    void Start()
    {
        initialScale = transform.localScale;
    }

    void Update()
    {
        // 1拍ごとに「ドゥン！」と大きくする
        // (conductor.songPositionInBeats が整数のタイミングに近いとき)
        float beat = conductor.songPositionInBeats;

        // 簡易的なビート検知：拍数の小数点が 0.0 ~ 0.1 の間ならビートとみなす
        // ※より正確にするなら「拍が変わった瞬間」を検知するロジックが必要です
        if (beat % 1.0f < 0.1f)
        {
            // サイズを少し大きくする
            transform.localScale = initialScale * pulseIntensity;
        }

        // 常に元のサイズに戻ろうとする (線形補間)
        transform.localScale = Vector3.Lerp(transform.localScale, initialScale, Time.deltaTime * returnSpeed);
    }
}

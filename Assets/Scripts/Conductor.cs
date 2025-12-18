using UnityEngine;
using UnityEngine.UI;
public class Conductor : MonoBehaviour
{
    public AudioSource musicSource;
    public float bpm = 120f; // 曲のBPMに合わせて変更してください

    private float secPerBeat; // 1拍あたりの秒数
    private float songPosition; // 曲の再生位置（秒）
    public float songPositionInBeats; // 曲の再生位置（拍）
    private float dspSongTime; // 曲が始まった正確なシステム時間
    public Text bgmNameText; // InspectorにTextオブジェクトをドラッグ&ドロップ
    void Start()
    {
        secPerBeat = 60f / bpm;
        dspSongTime = (float)AudioSettings.dspTime; // 高精度な時間を取得
        musicSource.Play();

        if (bgmNameText != null)
        {
            bgmNameText.text = "BGM : 8-bit_Aggressive1";

            // オプション: 3秒後にテキストを消したい場合
            // StartCoroutine(HideTextAfterDelay(3f)); 
        }
    }

    void Update()
    {
        // 現在の曲の位置を計算
        songPosition = (float)(AudioSettings.dspTime - dspSongTime);
        songPositionInBeats = songPosition / secPerBeat;

        // デバッグ用：拍数をコンソールに表示
        // Debug.Log("Beat: " + (int)songPositionInBeats);
    }

    // 他のスクリプトから現在の拍数を取得するための関数
    public float GetBeat()
    {
        return songPositionInBeats;
    }
}


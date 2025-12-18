using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public string gameSceneName = "MainGameScene";

    public void StartGame()
    {
        Debug.Log("ゲーム開始" + gameSceneName + "へ移動します");
        SceneManager.LoadScene(gameSceneName);
    }

    // Update is called once per frame
    void QuitGame()
    {
        Debug.Log("ゲーム終了");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

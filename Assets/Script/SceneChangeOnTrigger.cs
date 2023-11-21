using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeOnTrigger : MonoBehaviour
{
    public string targetSceneName; // 이동할 씬의 이름

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어와 충돌하면 지정한 씬으로 이동
            SceneManager.LoadScene(targetSceneName);
        }
    }
}

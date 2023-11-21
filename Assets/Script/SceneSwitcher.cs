using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // 이 메서드는 버튼의 OnClick 이벤트에 연결됩니다.
    public void LoadSceneZero()
    {
        // SceneManager를 사용하여 0번째 씬을 로드합니다.
        SceneManager.LoadScene(0);
    }
}

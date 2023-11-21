using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    private float currentTime = 0.0f;
    private bool isMenuOpen = false;
    private string lastSceneName;
    public GameState gameState;    
    
    public int move;

    public Text timerText;
    public GameObject menuObject;
    public GameObject optionObject;
    public Transform playerTransform;
    public Transform Camera;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        UpdateTimerText();
        move = PlayerPrefs.GetInt("_move");
        if(move == 2)
        {
            //첫번째 npc위치
        }
        if(move == 3)
        {
            //두번째 npc위치
        }
        if(move == 4)
        {
            //세번째 npc위치
        }
        if(move == 5)
        {
            //네번째 npc위치
        }
        if(move == 6)
        {
            //다섯번째 npc위치
        }
        if(move == 7)
        {
            //여섯번째 npc위치
        }
    }
    public void Destroy()
    {
        move = 1;
    }
    public void plus()
    {
        move++;
    }
    public void save()
    {
        PlayerPrefs.SetInt("_move",move);
    }
    public void ResetTimerAndPlayerPosition()
    {
        // Ÿ�̸� �ʱ�ȭ
        currentTime = 0.0f;
        UpdateTimerText();

        // �÷��̾� ��ġ �̵�
        playerTransform.position = new Vector3(-3.461f, -3.458f, playerTransform.position.z);
        Camera.position = new Vector2(0, playerTransform.position.y);
    }

    private void OnApplicationQuit()
    {
    }

    private void Update()
    {
        if (!isMenuOpen && !optionObject.activeSelf)
        {
            currentTime += Time.deltaTime;
            UpdateTimerText();

            // 타이머 값 갱신 시 PlayerPrefs에 저장
            PlayerPrefs.SetFloat("GameTimer_CurrentTime", currentTime);
            PlayerPrefs.Save();
        }
    }

    public void UpdateTimerText()
    {
        int milliseconds = Mathf.FloorToInt((currentTime * 100.0f) % 100);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        int minutes = Mathf.FloorToInt((currentTime / 60.0f) % 60);
        int hours = Mathf.FloorToInt((currentTime / 3600.0f) % 24);

        timerText.text = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", hours, minutes, seconds, milliseconds);
    }

    public void OpenMenu()
    {
        menuObject.SetActive(true);
        isMenuOpen = true;
        currentTime = 0.0f;
        UpdateTimerText();
    }

    public void CloseMenu()
    {
        menuObject.SetActive(false);
        isMenuOpen = false;
    }

    public void OpenOption()
    {
        optionObject.SetActive(true);
        isMenuOpen = true;
        currentTime = 0.0f;
        UpdateTimerText();
    }

    public void CloseOption()
    {
        optionObject.SetActive(false);
        isMenuOpen = false;
    }

    public void ChangeScene(string sceneName)
    {
        PlayerPrefs.SetString("LastSceneName", SceneManager.GetActiveScene().name);
        gameState = new GameState(currentTime, playerTransform.position);
        string jsonGameState = JsonUtility.ToJson(gameState);
        PlayerPrefs.SetString("GameState", jsonGameState);
        PlayerPrefs.Save();

        SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == lastSceneName)
        {
            currentTime = gameState.currentTime;
            playerTransform.position = gameState.playerPosition;
            UpdateTimerText();
        }
        else
        {
            currentTime = 0.0f;
        }
    }
}
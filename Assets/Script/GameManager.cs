using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // GameManager의 인스턴스를 저장하기 위한 변수
    public float volume; // 소리 설정을 저장하기 위한 변수

    public AudioSource BGM;

    public Slider mainSceneSlider;
    public Slider gameSceneSlider;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 중복된 GameManager 오브젝트가 있을 경우 파괴
        }
    }

    // 소리 설정을 변경하는 함수
    public void SetVolume(float newVolume)
    {
        volume = newVolume;

        if (BGM != null) // BGM AudioSource가 파괴되지 않았는지 체크합니다.
        {
            BGM.volume = volume; // 소리 설정을 적용하는 코드 작성
        }
    }

    // 메인 씬에서 호출되는 함수
    public void SaveVolume()
    {
        volume = mainSceneSlider.value; // 메인 씬의 슬라이더 값으로 소리 설정 저장
    }

    public void ApplyVolume()
    {
        if (gameSceneSlider != null)
        {
            // 기존에 등록된 이벤트 리스너를 제거합니다.
            gameSceneSlider.onValueChanged.RemoveAllListeners();

            // 저장된 소리 설정을 슬라이더에 적용합니다.
            gameSceneSlider.value = volume;

            // 새로운 이벤트 리스너를 등록합니다.
            gameSceneSlider.onValueChanged.AddListener((float newValue) =>
            {
                SetVolume(newValue);
            });
        }

        if (BGM != null)
        {
            // 저장된 소리 설정을 게임 씬에 적용합니다.
            BGM.volume = volume;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void UpdateVolume(float newVolume)
    {
        if (mainSceneSlider != null)
        {
            mainSceneSlider.value = newVolume; // 슬라이더의 값을 변경합니다.
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Menu")
        {
            // 메인 씬의 슬라이더를 찾아서 연결합니다.
            mainSceneSlider = GameObject.Find("Canvas/Option/Panel/Image/Music").GetComponent<Slider>();
            // 슬라이더의 값이 변경될 때마다 SetVolume 함수를 호출하도록 이벤트 리스너를 추가합니다.
            mainSceneSlider.onValueChanged.AddListener((float newValue) =>
            {
                SetVolume(newValue);
            });

            // 저장된 볼륨 값을 슬라이더에 적용합니다.
            mainSceneSlider.value = volume;
        }
        else if (scene.name == "First")
        {
            // 게임 씬의 슬라이더를 찾아서 연결합니다.
            gameSceneSlider = GameObject.Find("manager/Canvas/Option/Panel/Image/Music").GetComponent<Slider>();
        }

        if (scene.name == "First") // 게임 씬의 이름으로 변경
        {
            ApplyVolume(); // GameManager의 ApplyVolume 함수 호출 시 volume 변수를 매개변수로 전달
        }
    }
}

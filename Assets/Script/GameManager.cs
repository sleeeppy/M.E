using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // GameManager�� �ν��Ͻ��� �����ϱ� ���� ����
    public float volume; // �Ҹ� ������ �����ϱ� ���� ����

    public AudioSource BGM;

    public Slider mainSceneSlider;
    public Slider gameSceneSlider;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject); // �ߺ��� GameManager ������Ʈ�� ���� ��� �ı�
        }
    }

    // �Ҹ� ������ �����ϴ� �Լ�
    public void SetVolume(float newVolume)
    {
        volume = newVolume;

        if (BGM != null) // BGM AudioSource�� �ı����� �ʾҴ��� üũ�մϴ�.
        {
            BGM.volume = volume; // �Ҹ� ������ �����ϴ� �ڵ� �ۼ�
        }
    }

    // ���� ������ ȣ��Ǵ� �Լ�
    public void SaveVolume()
    {
        volume = mainSceneSlider.value; // ���� ���� �����̴� ������ �Ҹ� ���� ����
    }

    public void ApplyVolume()
    {
        if (gameSceneSlider != null)
        {
            // ������ ��ϵ� �̺�Ʈ �����ʸ� �����մϴ�.
            gameSceneSlider.onValueChanged.RemoveAllListeners();

            // ����� �Ҹ� ������ �����̴��� �����մϴ�.
            gameSceneSlider.value = volume;

            // ���ο� �̺�Ʈ �����ʸ� ����մϴ�.
            gameSceneSlider.onValueChanged.AddListener((float newValue) =>
            {
                SetVolume(newValue);
            });
        }

        if (BGM != null)
        {
            // ����� �Ҹ� ������ ���� ���� �����մϴ�.
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
            mainSceneSlider.value = newVolume; // �����̴��� ���� �����մϴ�.
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Menu")
        {
            // ���� ���� �����̴��� ã�Ƽ� �����մϴ�.
            mainSceneSlider = GameObject.Find("Canvas/Option/Panel/Image/Music").GetComponent<Slider>();
            // �����̴��� ���� ����� ������ SetVolume �Լ��� ȣ���ϵ��� �̺�Ʈ �����ʸ� �߰��մϴ�.
            mainSceneSlider.onValueChanged.AddListener((float newValue) =>
            {
                SetVolume(newValue);
            });

            // ����� ���� ���� �����̴��� �����մϴ�.
            mainSceneSlider.value = volume;
        }
        else if (scene.name == "First")
        {
            // ���� ���� �����̴��� ã�Ƽ� �����մϴ�.
            gameSceneSlider = GameObject.Find("manager/Canvas/Option/Panel/Image/Music").GetComponent<Slider>();
        }

        if (scene.name == "First") // ���� ���� �̸����� ����
        {
            ApplyVolume(); // GameManager�� ApplyVolume �Լ� ȣ�� �� volume ������ �Ű������� ����
        }
    }
}

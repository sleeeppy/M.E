using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        slider.value = GameManager.instance.volume; // 슬라이더 초기화
    }

    public void SetMusicVolume()
    {
        float volume = slider.value;
        GameManager.instance.SetVolume(volume); // GameManager의 SetVolume 함수를 호출하여 소리 설정 변경
    }
}

using UnityEngine;
using UnityEngine.UI;

public class SliderEventHandler : MonoBehaviour
{
    public GameManager gameManager; // GameManager 오브젝트를 참조하기 위한 변수

    private Slider slider; // 슬라이더 컴포넌트를 참조하기 위한 변수

    private void Start()
    {
        slider = GetComponent<Slider>(); // 슬라이더 컴포넌트를 참조합니다.
        slider.onValueChanged.AddListener(OnSliderValueChanged); // 슬라이더 값이 변경될 때 호출할 함수를 등록합니다.
    }

    private void OnSliderValueChanged(float newValue)
    {
        if (gameManager != null) // gameManager가 null이 아닌지 체크합니다.
        {
            gameManager.SetVolume(newValue); // GameManager의 SetVolume 함수를 호출하여 소리 설정을 변경합니다.
        }
    }

}

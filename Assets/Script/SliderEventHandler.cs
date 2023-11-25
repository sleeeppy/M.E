using UnityEngine;
using UnityEngine.UI;

public class SliderEventHandler : MonoBehaviour
{
    public GameManager gameManager; // GameManager ������Ʈ�� �����ϱ� ���� ����

    private Slider slider; // �����̴� ������Ʈ�� �����ϱ� ���� ����

    private void Start()
    {
        slider = GetComponent<Slider>(); // �����̴� ������Ʈ�� �����մϴ�.
        slider.onValueChanged.AddListener(OnSliderValueChanged); // �����̴� ���� ����� �� ȣ���� �Լ��� ����մϴ�.
    }

    private void OnSliderValueChanged(float newValue)
    {
        if (gameManager != null) // gameManager�� null�� �ƴ��� üũ�մϴ�.
        {
            gameManager.SetVolume(newValue); // GameManager�� SetVolume �Լ��� ȣ���Ͽ� �Ҹ� ������ �����մϴ�.
        }
    }

}

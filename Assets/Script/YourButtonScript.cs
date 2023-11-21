using System.Collections;
using UnityEngine;

public class YourButtonScript : MonoBehaviour
{
    public Shot shotScript; // Shot 스크립트 연결
    public VideoOption videoOptionScript; // VideoOption 스크립트 연결
    public GameObject uiObject; // UI 오브젝트 연결

    private bool isUIVisible = false; // UI가 보이는지 여부

    private void Start()
    {
        // 초기에 UI를 숨김
        uiObject.SetActive(false);
    }

    private void Update()
    {
        // esc 키를 눌러서 UI를 토글
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isUIVisible)
            {
                ToggleUI();
            }
        }
    }

    public void ToggleUI()
    {
        isUIVisible = !isUIVisible; // UI 상태를 반전

        if (isUIVisible)
        {
            // Shot 스크립트 비활성화
            shotScript.enabled = false;

            // VideoOption 스크립트 비활성화
            videoOptionScript.enabled = false;

            // UI 오브젝트 활성화
            uiObject.SetActive(true);
        }
        else
        {
            // UI 오브젝트 비활성화


            // Shot 스크립트 활성화
            shotScript.enabled = true;

            // VideoOption 스크립트 활성화
            videoOptionScript.enabled = true;
            uiObject.SetActive(false);
        }
    }
}

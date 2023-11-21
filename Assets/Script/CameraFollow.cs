using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 플레이어의 Transform을 저장할 변수
    public float minY = -1f; // 카메라의 Y축 좌표가 내려갈 수 있는 최소 값

    private bool shouldFollow = true; // 카메라가 플레이어를 따라가는지 여부를 결정하는 플래그 변수
    private float speed = 5.0f; // 카메라 이동 속도

    void LateUpdate()
    {
        Vector3 cameraPosition = transform.position;

        if (shouldFollow)
        {
            cameraPosition.y = Mathf.Max(target.position.y, minY);
        }

        if (Input.GetKey(KeySetting.keys[KeyAction.Upar]))
        {
            cameraPosition.y += speed * Time.deltaTime;
            shouldFollow = false;
        }

        if (Input.GetKey(KeySetting.keys[KeyAction.Down]))
        {
            cameraPosition.y -= speed * Time.deltaTime;

            if (cameraPosition.y < minY)
                cameraPosition.y = minY;

            shouldFollow = false;
        }

        else if (!Input.anyKey)
        {
            shouldFollow = true;
        }

        transform.position = cameraPosition;
    }
}
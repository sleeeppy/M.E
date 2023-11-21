using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ske : MonoBehaviour
{
    public bool Gwang = false;
    public bool Sax = false;
    public bool destroy = false;
    public Image whiteScreen; // �Ͼ� ȭ�� �̹���
    public TextMeshProUGUI iceText; // ���� �ؽ�Ʈ (TextMeshProUGUI ������Ʈ)
    public GameObject Hea;
    public GameObject me;

    public bool El = false;
    public bool Tr = false;

    private float flashDuration = 0.3f; // �Ͼ� ȭ�� ���� �ð�
    private float iceTextDuration = 2f; // ���� �ؽ�Ʈ ���� �ð�
    private bool isFlashing = false;
    private float flashTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        whiteScreen.gameObject.SetActive(false);
        iceText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(destroy == true)
        {
            me.SetActive(false);
        }
        if(Tr == true)
        {
                            // Sax�� true�� ��� ȭ���� �Ͼ�� �����
                if (!isFlashing)
                {
                    isFlashing = true;
                    flashTimer = 0f;
                    whiteScreen.gameObject.SetActive(true);
                    
                    
                }

                // ȭ���� �Ͼ�� ������ �� �ٽ� ������
                flashTimer += Time.deltaTime;
                if (flashTimer >= flashDuration)
                {
                    
                    whiteScreen.gameObject.SetActive(false);
                    Hea.SetActive(true);
                    destroy = true;
                    isFlashing = false;
                    Tr = false;
                    
                    
                }
        }
        if(El == true)
        {
                            // Sax�� false�� ��� ���� �ؽ�Ʈ ǥ���ϱ�
                iceText.gameObject.SetActive(true);
                StartCoroutine(HideIceText());
        }
        if(Sax == true)
        {
        if (Input.GetKey(KeySetting.keys[KeyAction.SANGHO]))
        {
            if (Gwang)
            {
                Tr = true;
            }
            else
            {
                El = true;
            }
        }
        else
        {
        }
        }
    }

    IEnumerator HideIceText()
    {
        yield return new WaitForSeconds(iceTextDuration);
        iceText.gameObject.SetActive(false);
        El = false;
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Player"))
        {
            Sax = true;
        }
    }
    void OnTriggerExit2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Player"))
        {
            Sax = false;
        }
    }
}

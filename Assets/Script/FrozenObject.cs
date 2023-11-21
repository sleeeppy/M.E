using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenObject : MonoBehaviour
{
    private bool isFrozen = false;
    private float slideSpeed = 1.0f;
    public bool mung = false;

    private void Update()
    {
        // isFrozen�� true�� ��쿡�� x ��ǥ�� ������
        if (isFrozen && mung == false)
        {
            float slideAmount = slideSpeed * Time.deltaTime;
            transform.Translate(new Vector3(slideAmount, 0f, 0f));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Frozen �±׸� ���� ������Ʈ�� �浹�ϸ� isFrozen�� true�� ����
        if (collision.collider.CompareTag("Frozen"))
        {
            isFrozen = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // �浹���� ����� isFrozen�� false�� ����
        if (collision.collider.CompareTag("Frozen"))
        {
            isFrozen = false;
        }
    }
}
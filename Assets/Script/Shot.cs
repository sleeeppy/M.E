using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
  



public class Shot : MonoBehaviour
{
    [SerializeField] MouseHookSSD[] hook;
    public GameObject Menu;
    public GameObject Left;
    public GameObject Right;

    public float coolTime = 0.2f;
    public float coolTime1 = 0.2f;
    bool isCool = true;
    bool isCool1 = true;

    void Start()
    {
        Left.SetActive(true);
        Right.SetActive(true);

    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            MfLeft();
        }
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            MfRight();
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            CoolLeft();
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            CoolRight();
        }
    }
    public void MfLeft()
    {
        if (Menu.activeSelf == false)
        {
            ShotHook(0);
        }
    }
    public void MfRight()
    {
        if (Menu.activeSelf == false)
        {
            ShotHook(1);
        }
    }
    public void CoolLeft()
    {
        if (isCool)
        {
            Left.SetActive(true);
            ReSetHook0();
            StopCoroutine(CoolDown());
            StartCoroutine(CoolDown());
        }
    }
    public void CoolRight()
    {
        if (isCool1)
        {
            Right.SetActive(true);
            ReSetHook1();
            StopCoroutine(CoolDown1());
            StartCoroutine(CoolDown1());
        }
    }

    void ShotHook(int num)
    {
        hook[num].Follow();
    }

    void ReSetHook0()
    {
        hook[0].ResetHookPos();
    }
    void ReSetHook1()
    {
        hook[1].ResetHookPos1();
    }
    IEnumerator CoolDown()
    {
        isCool = false;
            
        yield return new WaitForSeconds(coolTime);

        isCool = true;
    }
    IEnumerator CoolDown1()
    {
        isCool1 = false;

        yield return new WaitForSeconds(coolTime1);

        isCool1 = true;
    }
}
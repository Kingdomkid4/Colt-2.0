using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Melee : MonoBehaviour
{
    public bool isMeele;

    int quadrant = 0;
    public float meeleRange = 10;
    public float damage = 100f;
    public float XSensitivity = 2f;
    public float YSensitivity = 2f;
    private float screenCenterY;
    private float screenCenterX;
    private float xOffset;
    private float yOffset;

    public GameObject barrel;
    public Animator animator;

    void Update()
    {
        xOffset = Input.mousePosition.x;
        yOffset = Input.mousePosition.y;

        isMeele = WeaponSwitching.meele;

        float yRot = CrossPlatformInputManager.GetAxis("Mouse X") * XSensitivity;
        float xRot = CrossPlatformInputManager.GetAxis("Mouse Y") * YSensitivity;

        if (isMeele)
        {
            screenCenterX = Screen.width / 2;
            screenCenterY = Screen.height / 2;

            Debug.Log("X" + (xOffset - screenCenterX));

            Debug.Log("Y" + (yOffset - screenCenterY));

            /*if (/*right screenCenterX - Input.mousePosition.x)
            {
                
            }else if (/*rightInput.mousePosition.y >= 430f && Input.mousePosition.y <= 465)
            {

            }*/


            if (Input.GetButtonDown("Fire1"))
            {
                //Debug.Log(quadrant);
                Attack(quadrant);
            }
        }
       
    }

    IEnumerator AttackAnimation()
    {
        if (quadrant == 0)
        {
            animator.SetTrigger("Stab");
        }
        else if (quadrant == 1)
        {
            animator.SetTrigger("DownSlash");
        }
        else if (quadrant == 2)
        {
            animator.SetTrigger("DownAngleRight");
        }
        else if (quadrant == 3)
        {
            animator.SetTrigger("Right");
        }
        else if (quadrant == 4)
        {
            animator.SetTrigger("UpAngleRight");
        }
        else if (quadrant == 5)
        {
            animator.SetTrigger("UpSlash");
        }
        else if (quadrant == 6)
        {
            animator.SetTrigger("UpAngleLeft");
        }
        else if (quadrant == 7)
        {
            animator.SetTrigger("Left");
        }
        else
        {
            animator.SetTrigger("DownAngleRight");
        }

        yield return new WaitForSeconds(0.45f);
    }

    void Attack(int quadrant)
    {
        StartCoroutine(AttackAnimation());

        Debug.Log("did it get to attack?");
        RaycastHit hit;

        if (Physics.Raycast(barrel.transform.position, barrel.transform.forward, out hit, meeleRange))
        {
            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                target.block = Random.Range(0, 8);

                Debug.Log(target.block);

                if (target.block != quadrant)
                {
                    Debug.Log("Successfull attack");
                    target.TakeDamage(damage);
                }
                else
                {
                    Debug.Log("Failed attack");
                }
            }
        }

    }
}

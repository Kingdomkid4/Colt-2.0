using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    //public GameObject octagon;
    public int selectedWeapon = 0;
    static public bool meele = false;

	void Start ()
    {
        SelectWeapon();
	}
	

	void Update ()
    {

        int previousSelectedWeapon = selectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = transform.childCount - 1;
            }
            else
            {
                selectedWeapon--;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            meele = false;
            selectedWeapon = 0;
            //octagon.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            meele = false;
            selectedWeapon = 1;
           // octagon.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.F) && transform.childCount >= 3)
        {
            meele = true;
            
            selectedWeapon = 2;

        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}

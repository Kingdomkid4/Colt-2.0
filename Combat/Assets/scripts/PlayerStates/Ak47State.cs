using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ak47State :  IPlayerState
{
    private float bulletSpreadMultiplier;
    private float currentBS;

    private int currentAmmo = -1;

    public PrimaryWeapon AK47;

    private float bulletCount = 1f;
    private float nextTimeToFire = 0f;

    private Player player;

    IEnumerator Reload()
    {
        bulletSpreadMultiplier = 0f;
        bulletCount = 0;

        AK47.isReloading = true;

        Debug.Log("Reloading...");

        AK47.animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(AK47.reloadTime - 0.25f);
        AK47.animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);

        currentAmmo = AK47.clipMax;

        AK47.isReloading = false;
    }

    public void Enter(Player player)
    {
        this.player = player;

        if (currentAmmo == -1)
        {
            currentAmmo = AK47.clipMax;
        }
    }

    #region Other Enters
    public void Enter(Player player, PrimaryWeapon primaryWeponStats, GameObject rifle, SideArm sideArmStats, GameObject sideArm)
    {
        throw new System.NotImplementedException();
    }

    public void Enter(Player player, GameObject meleeWeapon, MeleeWeapon weaponStats)
    {
        throw new System.NotImplementedException();
    }
    #endregion

    public void Execute()
    {
                
        if (AK47.isReloading)
        {
            return;
        }

        if (currentAmmo <= 0)
        {
            // look into this MonoBehaviour.StartCoroutine(Reload());
            return;
        }



        if (Time.time >= nextTimeToFire)
            {
                if (bulletCount < 15)
                {
                    bulletCount++;
                    bulletSpreadMultiplier += 0.4f;
                }

                nextTimeToFire = Time.time + 1f / AK47.fireRate;
                Shoot();
            }

        

        if (Time.time - nextTimeToFire >= 0.5f)
        {
            Debug.Log("Reset");
            bulletSpreadMultiplier = 0f;
            bulletCount = 0;
        }
    }

    public void Exit()
    {
        
    }

    void Shoot()
    {
        if (AK47.flash)
        {
            AK47.muzzleFlash.Play();
        }

        currentAmmo--;

        Vector3 fireDirection = AK47.barrel.transform.forward;

        Quaternion fireRotation = Quaternion.LookRotation(fireDirection);

        Quaternion randomRotation = Random.rotation;

        fireRotation = Quaternion.RotateTowards(fireRotation, randomRotation, Random.Range(0.0f, currentBS * bulletSpreadMultiplier));

        RaycastHit hit;
        if (Physics.Raycast(AK47.barrel.transform.position, fireRotation * Vector3.forward, out hit, AK47.range))
        {

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(AK47.damage);

                if (target.meterial == "Flesh")
                {
                    GameObject impactFleshGO = MonoBehaviour.Instantiate(AK47.fleshImapctEffect, hit.point, Quaternion.LookRotation(hit.normal));

                    MonoBehaviour.Destroy(impactFleshGO, 0.5f);

                    return;
                }
            }

            GameObject impactGO = MonoBehaviour.Instantiate(AK47.imapctEffect, hit.point, Quaternion.LookRotation(hit.normal));

            MonoBehaviour.Destroy(impactGO, 1f);

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryShootingState :  IPlayerState
{
    private float bulletSpreadMultiplier;
    private float currentBS;

    private int currentAmmo = -1;

    private float bulletCount = 1f;
    private float nextTimeToFire = 0f;

    private Player player;

    private PrimaryWeapon rifle;
    private GameObject primaryGun;

    private SideArm sideArm;
    private GameObject secondaryGun;

    /*IEnumerator Reload()
    {
        bulletSpreadMultiplier = 0f;
        bulletCount = 0;

        rifle.isReloading = true;

        Debug.Log("Reloading...");

        rifle.animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(rifle.reloadTime - 0.25f);
        rifle.animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);

        currentAmmo = rifle.clipMax;

        rifle.isReloading = false;
    }
    */

    public void Enter(Player player, SideArm sideArmStats, GameObject sideArm)
    {
        this.player = player;

        this.sideArm = sideArmStats;

        sideArm = secondaryGun;

        if (currentAmmo == -1)
        {
            currentAmmo = sideArmStats.clipMax;
        }
    }
    

    #region Other Enters

    public void Enter(Player player)
    {
        throw new System.NotImplementedException();
    }

    public void Enter(Player player, GameObject meleeWeapon, MeleeWeapon weaponStats)
    {
        throw new System.NotImplementedException();
    }
    public void Enter(Player player, PrimaryWeapon primaryWeponStats, GameObject rifle, SideArm sideArmStats, GameObject sideArm)
    {

        throw new System.NotImplementedException();
    }

    public void Enter(Player player, PrimaryWeapon primaryWeponStats, GameObject rifle)
    {
        throw new System.NotImplementedException();
    }
    #endregion

    public void Execute()
    {
                
        if (rifle.isReloading || sideArm.isReloading)
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

                nextTimeToFire = Time.time + 1f / rifle.fireRate;
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
        if (rifle.flash)
        {
            rifle.muzzleFlash.Play();
        }

        currentAmmo--;

        Vector3 fireDirection = rifle.barrel.transform.forward;

        Quaternion fireRotation = Quaternion.LookRotation(fireDirection);

        Quaternion randomRotation = Random.rotation;

        fireRotation = Quaternion.RotateTowards(fireRotation, randomRotation, Random.Range(0.0f, currentBS * bulletSpreadMultiplier));

        RaycastHit hit;
        if (Physics.Raycast(rifle.barrel.transform.position, fireRotation * Vector3.forward, out hit, rifle.range))
        {

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(rifle.damage);

                if (target.material == "Flesh")
                {
                    GameObject impactFleshGO = MonoBehaviour.Instantiate(rifle.fleshImapctEffect, hit.point, Quaternion.LookRotation(hit.normal));

                    MonoBehaviour.Destroy(impactFleshGO, 0.5f);

                    return;
                }
            }

            GameObject impactGO = MonoBehaviour.Instantiate(rifle.imapctEffect, hit.point, Quaternion.LookRotation(hit.normal));

            MonoBehaviour.Destroy(impactGO, 1f);

        }
    }
}

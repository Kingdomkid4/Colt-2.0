using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float HipBulletSpread = 1f;
    public float ADSBulletSpread = 0.5f;
    private float bulletSpreadMultiplier;
    private float currentBS;
    public bool fullAuto;
    public bool flash;

    private bool isADS = false;

    public int clipMax = 10;
    private int currentAmmo = -1;
    public float reloadTime = 1f;
    private bool isReloading = false;
    private float bulletCount = 1f;

    public GameObject barrel;
    public GameObject gun;
    public ParticleSystem muzzleFlash;
    public GameObject fleshImapctEffect;
    public GameObject imapctEffect;
    public GameObject Crosshair;

    private float nextTimeToFire = 0f;

    public Animator animator;

    private Vector3 recoil = new Vector3();

    private Vector3 defaultPosition = new Vector3();

    public float recoilM = 1.2f;

    void Start()
    {
        if (currentAmmo == -1)
        {
            currentAmmo = clipMax;
        }

        defaultPosition = new Vector3(gun.transform.localPosition.x, gun.transform.localPosition.y, gun.transform.localPosition.z);

         recoil = new Vector3(gun.transform.localPosition.x + Random.Range(-0.4f, 0.4f), gun.transform.localPosition.y + Random.Range(-0.4f, 0.4f), gun.transform.localPosition.z * recoilM);

        Debug.Log(recoil);

    }

    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            isADS = true;
            animator.SetBool("ADS", true);
            ADS();
            Crosshair.SetActive(false);
        }
        else
        {
            isADS = false;
            animator.SetBool("ADS", false);
            Crosshair.SetActive(true);
        }

        if (isADS)
        {
            currentBS = ADSBulletSpread;
            Debug.Log(currentBS);
        }
        else
        {
            currentBS = HipBulletSpread;
            Debug.Log(currentBS);
        }
        if (isReloading)
        {
            return;
        }

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

       StartCoroutine(Recoil(defaultPosition, recoil));

        if (fullAuto)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                if (bulletCount < 15 && isADS)
                {
                    bulletCount++;
                    bulletSpreadMultiplier += 0.2f;
                }
                else if(bulletCount < 15)
                {
                    bulletCount++;
                    bulletSpreadMultiplier += 0.4f;
                }

                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
            {
                if (bulletCount < 15 && isADS)
                {
                    bulletCount++;
                    bulletSpreadMultiplier += 0.2f;
                }
                else if (bulletCount < 15)
                {
                    bulletCount++;
                    bulletSpreadMultiplier += 0.4f;
                }

                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }

        if (Time.time - nextTimeToFire >= 0.5f )
        {
            Debug.Log("Reset");
            bulletSpreadMultiplier = 0f;
            bulletCount = 0;
        }
    }

    IEnumerator ADS()
    {
        yield return new WaitForSeconds(0.15f);
    }

    IEnumerator Reload()
    {
        bulletSpreadMultiplier = 0f;
        bulletCount = 0;

        isReloading = true;

        Debug.Log("Reloading...");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - 0.25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);

        currentAmmo = clipMax;

        isReloading = false;
    }

    IEnumerator Recoil(Vector3 start, Vector3 end)
    {
        float speed = 1f;
        float startTime = Time.time;
        float progress = 0;

        while (progress < speed)
        {
            //camera.transform.localPosition = Vector3.Lerp(start, end, (Time.time - startTime) / speed);

            gun.transform.localPosition = Vector3.Lerp(start, end, (Time.time - startTime) / speed);

            yield return null;
            progress = Time.time - startTime;
        }
    }

    void Shoot()
    {
        if (flash)
        {
            muzzleFlash.Play();
        }

        currentAmmo--;

        Vector3 fireDirection = barrel.transform.forward;

        Quaternion fireRotation = Quaternion.LookRotation(fireDirection);

        Quaternion randomRotation = Random.rotation;

        fireRotation = Quaternion.RotateTowards(fireRotation, randomRotation, Random.Range(0.0f, currentBS * bulletSpreadMultiplier));

        RaycastHit hit;
        if (Physics.Raycast(barrel.transform.position, fireRotation * Vector3.forward, out hit, range))
        {
            //Debug.DrawRay(barrel.transform.position, barrel.transform.forward, Color.green, 2, false);
            //Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);

                if (target.meterial == "Flesh")
                {
                    GameObject impactFleshGO = Instantiate(fleshImapctEffect, hit.point, Quaternion.LookRotation(hit.normal));

                    Destroy(impactFleshGO, 0.5f);

                    return;
                }
            }

                GameObject impactGO = Instantiate(imapctEffect, hit.point, Quaternion.LookRotation(hit.normal));

                Destroy(impactGO, 1f);

        }
    }
}

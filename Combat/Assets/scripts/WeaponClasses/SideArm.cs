using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideArm : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;

    public float HipBulletSpread = 1f;
    public float ADSBulletSpread = 0.5f;

    public bool fullAuto;
    public bool flash;

    public int clipMax = 10;
    public float reloadTime = 1f;
    public bool isReloading = false;


    public GameObject barrel;
    public GameObject gun;
    public ParticleSystem muzzleFlash;
    public GameObject fleshImapctEffect;
    public GameObject imapctEffect;
    /*remove?*/
    public GameObject Crosshair;

    public Animator animator;

    public Vector3 recoil = new Vector3();

    public Vector3 defaultPosition = new Vector3();

    public float recoilM = 1.2f;
}
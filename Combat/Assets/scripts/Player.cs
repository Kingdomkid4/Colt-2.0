using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int selectedWeapon = 0;
    public bool melee = false;
    public float health = 100;
    public float regenAmmount = 10;
    public float TimeSinceLastDamage;
    public bool isCrouched = false;
    public bool isProne = false;
    public bool isJumpping = false;
    public bool isSwimming = false;
    public bool isRunning = false;

    public GameObject rifle;
    public GameObject sideArm;
    public GameObject meleeWeapon;
    public GameObject Granade;

    public MeleeWeapon m9Kinfe = new MeleeWeapon();
    public PrimaryWeapon  aK47 = new PrimaryWeapon ();
    public SideArm  glock9Mill = new SideArm ();

    private IPlayerState currentState;

	void Start ()
    {
        m9Kinfe.damage = 100;
        m9Kinfe.range = 5; //tbd
        
        aK47.damage = 10f;
        aK47.range = 100f;
        aK47.fireRate = 15f;
        aK47.HipBulletSpread = 1f;
        aK47.ADSBulletSpread = 0.5f;
        aK47.clipMax = 10;
        aK47.reloadTime = 1f;
        aK47.recoilM = 1.2f;

        glock9Mill.damage = 20f;
        glock9Mill.range = 100f;
        glock9Mill.fireRate = 15f;
        glock9Mill.HipBulletSpread = 1f;
        glock9Mill.ADSBulletSpread = 0.5f;
        glock9Mill.clipMax = 10;
        glock9Mill.reloadTime = 1f;
        glock9Mill.recoilM = 1.2f;


    }
	

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ChangeState(new MeleeState(), meleeWeapon, m9Kinfe);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            ChangeState(new Ak47State(),  aK47, rifle, glock9Mill, sideArm);
        }


        if (currentState != null)
        {
            currentState.Execute();
        }
        
	}

    public void ChangeState(IPlayerState newState, PrimaryWeapon primaryWeponStats, GameObject rifle, SideArm sideArmStats, GameObject sideArm)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this, primaryWeponStats, rifle, sideArmStats, sideArm);
    }

    public void ChangeState(IPlayerState newState, GameObject meleeWeapon, MeleeWeapon weaponStats)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this, meleeWeapon, weaponStats);
    }

    public void ChangeState(IPlayerState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this);
    }


}

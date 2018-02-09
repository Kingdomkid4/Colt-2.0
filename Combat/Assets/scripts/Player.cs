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

    public GameObject octagon;

    public MeleeWeapon m9Knife = new MeleeWeapon();
    public PrimaryWeapon aK47 = new PrimaryWeapon();
    public PrimaryWeapon AKM = new PrimaryWeapon();
    public SideArm glock9Mill = new SideArm();
    public Explosive fragGranade = new Explosive();

    public PrimaryWeapon primaryWeapon = new PrimaryWeapon();

    private IPlayerState currentState;

    void Start()
    {
        m9Knife.damage = 100;
        m9Knife.range = 5; //tbd

        aK47.damage = 10f;
        aK47.range = 100f;
        aK47.fireRate = 15f;
        aK47.HipBulletSpread = 1f;
        aK47.ADSBulletSpread = 0.5f;
        aK47.clipMax = 10;
        aK47.reloadTime = 1f;
        aK47.recoilM = 1.2f;

        AKM.damage = 10f;
        AKM.range = 100f;
        AKM.fireRate = 15f;
        AKM.HipBulletSpread = 1f;
        AKM.ADSBulletSpread = 0.5f;
        AKM.clipMax = 10;
        AKM.reloadTime = 1f;
        AKM.recoilM = 1.2f;

        glock9Mill.damage = 20f;
        glock9Mill.range = 100f;
        glock9Mill.fireRate = 15f;
        glock9Mill.HipBulletSpread = 1f;
        glock9Mill.ADSBulletSpread = 0.5f;
        glock9Mill.clipMax = 10;
        glock9Mill.reloadTime = 1f;
        glock9Mill.recoilM = 1.2f;
    }


    void Update()
    {

        int previousSelectedWeapon = selectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= 2)
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
                selectedWeapon = 1;
            }
            else
            {
                selectedWeapon--;
            }
        }

        if (selectedWeapon == 0)
        {
            rifle.SetActive(true);
            sideArm.SetActive(false);
        }
        else
        {
            rifle.SetActive(false);
            sideArm.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            melee = false;
            selectedWeapon = 0;
            octagon.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            melee = false;
            selectedWeapon = 1;
            octagon.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            melee = true;
            octagon.SetActive(true);
            selectedWeapon = 2;

        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }

        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeState(new MeleeState(), meleeWeapon, m9Knife);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            ChangeState(new MeleeState(), meleeWeapon, m9Knife);
        }

        if (Input.GetButtonDown("Fire1") && selectedWeapon == 0)
        {
            ChangeState(new PrimaryShootingState(), aK47, rifle);
        }
        else if (Input.GetButtonDown("Fire1") && Input.GetButtonDown("Fire2") && selectedWeapon == 0)
        {
            ChangeState(new PrimaryShootingADSState(), AKM, rifle);
        }
        else if (Input.GetButtonDown("Fire2") && selectedWeapon == 0)
        {
            ChangeState(new PrimaryADSState(), aK47, rifle);
        }

        if (Input.GetButtonDown("Fire1") && selectedWeapon == 1)
        {
            ChangeState(new SideArmShootingState(), glock9Mill, sideArm);
        }
        else if (Input.GetButtonDown("Fire1") && Input.GetButtonDown("Fire2") && selectedWeapon == 1)
        {
            ChangeState(new SideArmShootingADSState(), glock9Mill, sideArm);
        }
        else if (Input.GetButtonDown("Fire2") && selectedWeapon == 1)
        {
            ChangeState(new SideArmADSState(), glock9Mill, sideArm);
        }


        if (currentState != null)
        {
            currentState.Execute();
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

    public void ChangeState(IPlayerState newState, PrimaryWeapon primaryWeponStats, GameObject rifle, SideArm sideArmStats, GameObject sideArm)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this, primaryWeponStats, rifle, sideArmStats, sideArm);
    }

    public void ChangeState(IPlayerState newState, PrimaryWeapon primaryWeponStats, GameObject rifle)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this, primaryWeponStats, rifle);
    }

    public void ChangeState(IPlayerState newState, SideArm sideArmStats, GameObject sideArm)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this, sideArmStats, sideArm);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADSState : IPlayerState
{
    #region Other Enters
    public void Enter(Player player, PrimaryWeapon primaryWeponStats, GameObject rifle, SideArm sideArmStats, GameObject sideArm)
    {
        throw new System.NotImplementedException();
    }

    public void Enter(Player player, PrimaryWeapon primaryWeponStats, GameObject rifle)
    {
        throw new System.NotImplementedException();
    }

    public void Enter(Player player, SideArm sideArmStats, GameObject sideArm)
    {
        throw new System.NotImplementedException();
    }

    public void Enter(Player player, GameObject meleeWeapon, MeleeWeapon weaponStats)
    {
        throw new System.NotImplementedException();
    }
    #endregion

    public void Enter(Player player)
    {
        
    }

    

    public void Execute()
    {
        
    }

    public void Exit()
    {
        
    }
}

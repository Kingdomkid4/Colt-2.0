using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IPlayerState
{

    public void Enter(Player player)
    {
        throw new System.NotImplementedException();
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
        
    }

    public void Exit()
    {
        
    }

}

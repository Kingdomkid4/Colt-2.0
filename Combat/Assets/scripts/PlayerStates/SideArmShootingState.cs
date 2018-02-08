using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SideArmShootingState : IPlayerState
{
    public void Enter(Player player, PrimaryWeapon primaryWeponStats, GameObject rifle, SideArm sideArmStats, GameObject sideArm)
    {
        throw new NotImplementedException();
    }

    public void Enter(Player player, PrimaryWeapon primaryWeponStats, GameObject rifle)
    {
        throw new NotImplementedException();
    }

    public void Enter(Player player, SideArm sideArmStats, GameObject sideArm)
    {
        throw new NotImplementedException();
    }

    public void Enter(Player player, GameObject meleeWeapon, MeleeWeapon weaponStats)
    {
        throw new NotImplementedException();
    }

    public void Enter(Player player)
    {
        throw new NotImplementedException();
    }

    public void Execute()
    {
        throw new NotImplementedException();
    }

    public void Exit()
    {
        throw new NotImplementedException();
    }
}
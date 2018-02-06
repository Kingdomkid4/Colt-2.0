﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeState : IPlayerState
{
    public Player player;

    public void Enter(Player player)
    {
        this.player = player;
        player.selectedWeapon = 3;
        player.melee = true;
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState
{
    void Execute();
    void Enter(Player player, PrimaryWeapon primaryWeponStats, GameObject rifle, SideArm sideArmStats, GameObject sideArm);
    void Enter(Player player, PrimaryWeapon primaryWeponStats, GameObject rifle);
    void Enter(Player player, SideArm sideArmStats, GameObject sideArm);
    void Enter(Player player, GameObject meleeWeapon , MeleeWeapon weaponStats);
    void Enter(Player player);
    void Exit();
}
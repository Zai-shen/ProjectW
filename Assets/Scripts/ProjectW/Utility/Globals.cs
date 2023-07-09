using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    public static List<GameObject> Enemies = new List<GameObject>();

    public static Vector3 MapSize = new Vector3(100f, 1f, 100f);
    
    public static LayerMask PlayerMask = 1 << 6;
    public static LayerMask GroundMask = 1 << 7;
    public static LayerMask EnemyMask = 1 << 8;
    public static LayerMask DamageableLayers = PlayerMask | EnemyMask;


    // for tracking ingame progress
    //  Ingame is the actual value used for calculations, 
    // Ingame Bonus is the amount that gets added with each level up in that domain
    public static int FlourPlayer = 0;
    public static int FlourLevel = 0;
    public static int FlourNextLevel =100;
    public static int Score = 0;
    public static int DMGIngame = 100;
    public static int DMGIngameBonus = 10;
    public static int AttackSpeedIngame = 100;
    public static int AttackSpeedIngameBonus = 100;
    public static int KnockBackIngame = 100;
    public static int KnockBackIngameBonus = 100;
    public static int SuckerRadiusIngame = 100;
    public static int SuckerRadiusIngameBonus = 100;
    public static int SuckerDegreeIngame = 100;
    public static int SuckerDegreeIngameBonus = 100;
    public static int ShieldDurationIngame = 100;
    public static int ShieldDurationIngameBonus = 100;
    public static int WeaponRadiusIngame = 100;
    public static int WeaponRadiusIngameBonus = 100;

}
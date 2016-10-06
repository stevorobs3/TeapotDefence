
using UnityEngine;

public class SteamDamageUpgrade : Upgrade
{
    public static readonly SteamDamageUpgrade Level0 = new SteamDamageUpgrade(0,   3.5f);
    public static readonly SteamDamageUpgrade Level1 = new SteamDamageUpgrade(100, 5f);
    public static readonly SteamDamageUpgrade Level2 = new SteamDamageUpgrade(200, 10f);
    public static readonly SteamDamageUpgrade Level3 = new SteamDamageUpgrade(300, 15f);

    public static SteamDamageUpgrade[] Upgrades = new SteamDamageUpgrade[4]
    {
        Level0, Level1, Level2, Level3
    };

    public static UpgradeHelper<SteamDamageUpgrade> UpgradeManager = new UpgradeHelper<SteamDamageUpgrade>(Upgrades);

    private SteamDamageUpgrade(int cost, float value) : base(cost, value, "DAMAGE") { }
}


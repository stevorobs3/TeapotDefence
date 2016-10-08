
public class SteamDamageUpgrade : Upgrade
{
    public static readonly SteamDamageUpgrade Level0 = new SteamDamageUpgrade(0,   3.5f, 0);
    public static readonly SteamDamageUpgrade Level1 = new SteamDamageUpgrade(100, 5f, 1);
    public static readonly SteamDamageUpgrade Level2 = new SteamDamageUpgrade(200, 10f, 2);
    public static readonly SteamDamageUpgrade Level3 = new SteamDamageUpgrade(300, 15f, 3);
    public static readonly SteamDamageUpgrade Level4 = new SteamDamageUpgrade(300, 20f, 4);
    public static readonly SteamDamageUpgrade Level5 = new SteamDamageUpgrade(300, 25f, 5);

    public static SteamDamageUpgrade[] Upgrades = new SteamDamageUpgrade[]
    {
        Level0, Level1, Level2, Level3, Level4, Level5
    };

    public static UpgradeHelper<SteamDamageUpgrade> UpgradeManager = new UpgradeHelper<SteamDamageUpgrade>(Upgrades);

    private SteamDamageUpgrade(int cost, float value, int level) : base(cost, value, level, "DAMAGE") { }
}


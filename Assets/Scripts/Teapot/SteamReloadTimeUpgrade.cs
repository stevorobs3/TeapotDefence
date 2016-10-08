
public class SteamReloadTimeUpgrade : Upgrade
{
    public static readonly SteamReloadTimeUpgrade Level0 = new SteamReloadTimeUpgrade(0, 1, 0);
    public static readonly SteamReloadTimeUpgrade Level1 = new SteamReloadTimeUpgrade(20, 0.9f, 1);
    public static readonly SteamReloadTimeUpgrade Level2 = new SteamReloadTimeUpgrade(40, 0.8f, 2);
    public static readonly SteamReloadTimeUpgrade Level3 = new SteamReloadTimeUpgrade(60, 0.7f, 3);
    public static readonly SteamReloadTimeUpgrade Level4 = new SteamReloadTimeUpgrade(60, 0.6f, 4);
    public static readonly SteamReloadTimeUpgrade Level5 = new SteamReloadTimeUpgrade(60, 0.5f, 5);

    public static SteamReloadTimeUpgrade[] Upgrades = new SteamReloadTimeUpgrade[]
    {
        Level0, Level1, Level2, Level3, Level4, Level5
    };

    public static UpgradeHelper<SteamReloadTimeUpgrade> UpgradeManager = new UpgradeHelper<SteamReloadTimeUpgrade>(Upgrades);

    private SteamReloadTimeUpgrade(int cost, float value, int level) : base(cost, value, level, "RELOAD TIME") { }
}
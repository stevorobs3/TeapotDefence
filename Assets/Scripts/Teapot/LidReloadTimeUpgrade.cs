
public class LidReloadTimeUpgrade : Upgrade
{
    public static readonly LidReloadTimeUpgrade Level0 = new LidReloadTimeUpgrade(0, 1, 0);
    public static readonly LidReloadTimeUpgrade Level1 = new LidReloadTimeUpgrade(20, 0.9f, 1);
    public static readonly LidReloadTimeUpgrade Level2 = new LidReloadTimeUpgrade(40, 0.8f, 2);
    public static readonly LidReloadTimeUpgrade Level3 = new LidReloadTimeUpgrade(60, 0.7f, 3);
    public static readonly LidReloadTimeUpgrade Level4 = new LidReloadTimeUpgrade(60, 0.6f, 4);
    public static readonly LidReloadTimeUpgrade Level5 = new LidReloadTimeUpgrade(60, 0.5f, 5);

    public static LidReloadTimeUpgrade[] Upgrades = new LidReloadTimeUpgrade[]
    {
        Level0, Level1, Level2, Level3, Level4, Level5
    };

    public static UpgradeHelper<LidReloadTimeUpgrade> UpgradeManager = new UpgradeHelper<LidReloadTimeUpgrade>(Upgrades);

    private LidReloadTimeUpgrade(int cost, float value, int level) : base(cost, value, level, "RELOAD TIME") { }
}
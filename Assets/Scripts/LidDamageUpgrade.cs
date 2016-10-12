
public class LidDamageUpgrade : Upgrade
{
    public static readonly LidDamageUpgrade Level0 = new LidDamageUpgrade(0,   10f, 0);
    public static readonly LidDamageUpgrade Level1 = new LidDamageUpgrade(100, 15f, 1);
    public static readonly LidDamageUpgrade Level2 = new LidDamageUpgrade(200, 20f, 2);
    public static readonly LidDamageUpgrade Level3 = new LidDamageUpgrade(300, 25f, 3);
    public static readonly LidDamageUpgrade Level4 = new LidDamageUpgrade(300, 30f, 4);
    public static readonly LidDamageUpgrade Level5 = new LidDamageUpgrade(300, 50f, 5);

    public static LidDamageUpgrade[] Upgrades = new LidDamageUpgrade[]
    {
        Level0, Level1, Level2, Level3, Level4, Level5
    };

    public static UpgradeHelper<LidDamageUpgrade> UpgradeManager = new UpgradeHelper<LidDamageUpgrade>(Upgrades);

    private LidDamageUpgrade(int cost, float value, int level) : base(cost, value, level, "DAMAGE") { }
}


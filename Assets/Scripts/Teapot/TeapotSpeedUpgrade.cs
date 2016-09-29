
public class TeapotSpeedUpgrade : Upgrade
{
    public static readonly TeapotSpeedUpgrade Level0 = new TeapotSpeedUpgrade(0, 1);
    public static readonly TeapotSpeedUpgrade Level1 = new TeapotSpeedUpgrade(20, 2);
    public static readonly TeapotSpeedUpgrade Level2 = new TeapotSpeedUpgrade(40, 3);
    public static readonly TeapotSpeedUpgrade Level3 = new TeapotSpeedUpgrade(60, 4);

    public static TeapotSpeedUpgrade[] Upgrades = new TeapotSpeedUpgrade[4]
    {
        Level0, Level1, Level2, Level3
    };

    public static UpgradeHelper<TeapotSpeedUpgrade> UpgradeManager = new UpgradeHelper<TeapotSpeedUpgrade>(Upgrades);

    private TeapotSpeedUpgrade(int cost, float value) : base(cost, value, "Speed") { }
}
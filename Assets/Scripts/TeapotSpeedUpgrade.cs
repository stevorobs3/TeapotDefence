
public class TeapotSpeedUpgrade : TeapotUpgrade
{
    public static readonly TeapotSpeedUpgrade Level0 = new TeapotSpeedUpgrade(20, 1);
    public static readonly TeapotSpeedUpgrade Level1 = new TeapotSpeedUpgrade(40, 2);
    public static readonly TeapotSpeedUpgrade Level2 = new TeapotSpeedUpgrade(60, 3);
    public static readonly TeapotSpeedUpgrade Level3 = new TeapotSpeedUpgrade(80, 4);

    public static TeapotSpeedUpgrade[] Upgrades = new TeapotSpeedUpgrade[4]
    {
        Level0, Level1, Level2, Level3
    };

    public static TeapotUpgradeManager<TeapotSpeedUpgrade> UpgradeManager = new TeapotUpgradeManager<TeapotSpeedUpgrade>(Upgrades);

    private TeapotSpeedUpgrade(int cost, float value) : base(cost, value, "Speed") { }
}
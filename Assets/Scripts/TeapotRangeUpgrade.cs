
public class TeapotRangeUpgrade : TeapotUpgrade
{
    public static readonly TeapotRangeUpgrade Level0 = new TeapotRangeUpgrade(1, -1, 1, 20, 1);
    public static readonly TeapotRangeUpgrade Level1 = new TeapotRangeUpgrade(0.8f, -1.4f, 1.4f, 40, 2);
    public static readonly TeapotRangeUpgrade Level2 = new TeapotRangeUpgrade(1.2f, -2f, 1.7f, 60, 3);
    public static readonly TeapotRangeUpgrade Level3 = new TeapotRangeUpgrade(2f, -3f, 2.7f, 80, 4);

    public static TeapotUpgradeManager<TeapotRangeUpgrade> UpgradeManager = new TeapotUpgradeManager<TeapotRangeUpgrade>(Upgrades);

    public static TeapotRangeUpgrade[] Upgrades = new TeapotRangeUpgrade[4]
    {
        Level0, Level1, Level2, Level3
    };

    public readonly float StartLifetime;
    public readonly float XCollider;
    public readonly float YCollider;

    private TeapotRangeUpgrade(float startLifetime, float xCollider, float yCollider, int cost, float value) : base(cost, value, "Range")
    {
        StartLifetime = startLifetime;
        XCollider = xCollider;
        YCollider = yCollider;
    }
}

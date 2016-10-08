
public class LidClipSizeUpgrade : Upgrade
{
    public static readonly LidClipSizeUpgrade Level0 = new LidClipSizeUpgrade(0, 3, 0);
    public static readonly LidClipSizeUpgrade Level1 = new LidClipSizeUpgrade(100, 4, 1);
    public static readonly LidClipSizeUpgrade Level2 = new LidClipSizeUpgrade(200, 5, 2);
    public static readonly LidClipSizeUpgrade Level3 = new LidClipSizeUpgrade(300, 6, 3);
    public static readonly LidClipSizeUpgrade Level4 = new LidClipSizeUpgrade(400, 7, 4);
    public static readonly LidClipSizeUpgrade Level5 = new LidClipSizeUpgrade(500, 8, 5);

    public static LidClipSizeUpgrade[] Upgrades = new LidClipSizeUpgrade[]
    {
        Level0, Level1, Level2, Level3, Level4, Level5
    };

    public static UpgradeHelper<LidClipSizeUpgrade> UpgradeManager = new UpgradeHelper<LidClipSizeUpgrade>(Upgrades);

    private LidClipSizeUpgrade(int cost, float value, int level) : base(cost, value, level, "CLIPSIZE") { }
}

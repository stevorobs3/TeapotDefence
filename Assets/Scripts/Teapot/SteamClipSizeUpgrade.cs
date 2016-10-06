
public class SteamClipSizeUpgrade : Upgrade
{
    public static readonly SteamClipSizeUpgrade Level0 = new SteamClipSizeUpgrade(0, 3);
    public static readonly SteamClipSizeUpgrade Level1 = new SteamClipSizeUpgrade(100, 4);
    public static readonly SteamClipSizeUpgrade Level2 = new SteamClipSizeUpgrade(200, 5);
    public static readonly SteamClipSizeUpgrade Level3 = new SteamClipSizeUpgrade(300, 6);
    public static readonly SteamClipSizeUpgrade Level4 = new SteamClipSizeUpgrade(400, 7);
    public static readonly SteamClipSizeUpgrade Level5 = new SteamClipSizeUpgrade(500, 8);

    public static SteamClipSizeUpgrade[] Upgrades = new SteamClipSizeUpgrade[]
    {
        Level0, Level1, Level2, Level3, Level4, Level5
    };

    public static UpgradeHelper<SteamClipSizeUpgrade> UpgradeManager = new UpgradeHelper<SteamClipSizeUpgrade>(Upgrades);

    private SteamClipSizeUpgrade(int cost, float value) : base(cost, value, "CLIPSIZE") { }
}

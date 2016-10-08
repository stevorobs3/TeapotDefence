
public class PlantationRegenUpgrade : Upgrade
{
    public static readonly PlantationRegenUpgrade Level0 = new PlantationRegenUpgrade(0, 0, 0);
    public static readonly PlantationRegenUpgrade Level1 = new PlantationRegenUpgrade(20, 1f, 1);
    public static readonly PlantationRegenUpgrade Level2 = new PlantationRegenUpgrade(40, 1.5f, 2);
    public static readonly PlantationRegenUpgrade Level3 = new PlantationRegenUpgrade(60, 2f, 3);
    

    public static PlantationRegenUpgrade[] Upgrades = new PlantationRegenUpgrade[4]
    {
        Level0, Level1, Level2, Level3
    };

    private PlantationRegenUpgrade(int cost, float value, int level) : base(cost, value, level, "Regen") {}
}
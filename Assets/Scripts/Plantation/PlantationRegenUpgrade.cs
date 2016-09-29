
public class PlantationRegenUpgrade : Upgrade
{
    public static readonly PlantationRegenUpgrade Level0 = new PlantationRegenUpgrade(0, 0);
    public static readonly PlantationRegenUpgrade Level1 = new PlantationRegenUpgrade(20, 1f);
    public static readonly PlantationRegenUpgrade Level2 = new PlantationRegenUpgrade(40, 1.5f);
    public static readonly PlantationRegenUpgrade Level3 = new PlantationRegenUpgrade(60, 2f);
    

    public static PlantationRegenUpgrade[] Upgrades = new PlantationRegenUpgrade[4]
    {
        Level0, Level1, Level2, Level3
    };

    private PlantationRegenUpgrade(int cost, float value) : base(cost, value, "Regen") {}
}
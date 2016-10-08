
public class PlantationHealthUpgrade : Upgrade
{
    public static readonly PlantationHealthUpgrade Level0 = new PlantationHealthUpgrade(0, 10, 0);
    public static readonly PlantationHealthUpgrade Level1 = new PlantationHealthUpgrade(20, 15, 1);
    public static readonly PlantationHealthUpgrade Level2 = new PlantationHealthUpgrade(40, 20, 2);
    public static readonly PlantationHealthUpgrade Level3 = new PlantationHealthUpgrade(60, 30, 3);

    public static PlantationHealthUpgrade[] Upgrades = new PlantationHealthUpgrade[]
    {
        Level0, Level1, Level2, Level3
    };

    private PlantationHealthUpgrade(int cost, float value, int level) : base(cost, value, level, "Health") {}
}
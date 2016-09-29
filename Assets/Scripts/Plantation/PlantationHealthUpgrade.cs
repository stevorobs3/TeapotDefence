
public class PlantationHealthUpgrade : Upgrade
{
    public static readonly PlantationHealthUpgrade Level0 = new PlantationHealthUpgrade(0, 10);
    public static readonly PlantationHealthUpgrade Level1 = new PlantationHealthUpgrade(20, 15);
    public static readonly PlantationHealthUpgrade Level2 = new PlantationHealthUpgrade(40, 20);
    public static readonly PlantationHealthUpgrade Level3 = new PlantationHealthUpgrade(60, 30);

    public static PlantationHealthUpgrade[] Upgrades = new PlantationHealthUpgrade[4]
    {
        Level0, Level1, Level2, Level3
    };

    private PlantationHealthUpgrade(int cost, float value) : base(cost, value, "Health") {}
}
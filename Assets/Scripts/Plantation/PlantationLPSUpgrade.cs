using UnityEngine;

public class PlantationLPSUpgrade : Upgrade
{
    public static readonly PlantationLPSUpgrade Level0 = new PlantationLPSUpgrade(0, 1, 0);
    public static readonly PlantationLPSUpgrade Level1 = new PlantationLPSUpgrade(20, 1.25f, 1);
    public static readonly PlantationLPSUpgrade Level2 = new PlantationLPSUpgrade(40, 1.5f, 2);
    public static readonly PlantationLPSUpgrade Level3 = new PlantationLPSUpgrade(60, 2f, 3);
    

    public static PlantationLPSUpgrade[] Upgrades = new PlantationLPSUpgrade[4]
    {
        Level0, Level1, Level2, Level3
    };


    private PlantationLPSUpgrade(int cost, float value, int level) : base(cost, value, level, "LPS") { }
}
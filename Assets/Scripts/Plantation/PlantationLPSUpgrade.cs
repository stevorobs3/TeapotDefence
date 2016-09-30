using UnityEngine;

public class PlantationLPSUpgrade : Upgrade
{
    public static readonly PlantationLPSUpgrade Level0 = new PlantationLPSUpgrade(0, 1);
    public static readonly PlantationLPSUpgrade Level1 = new PlantationLPSUpgrade(20, 1.25f);
    public static readonly PlantationLPSUpgrade Level2 = new PlantationLPSUpgrade(40, 1.5f);
    public static readonly PlantationLPSUpgrade Level3 = new PlantationLPSUpgrade(60, 2f);
    

    public static PlantationLPSUpgrade[] Upgrades = new PlantationLPSUpgrade[4]
    {
        Level0, Level1, Level2, Level3
    };


    private PlantationLPSUpgrade(int cost, float value) : base(cost, value, "LPS") { }
}
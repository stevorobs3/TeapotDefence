using UnityEngine;

public class PlantationLeavesUpgrade : Upgrade
{
    public static readonly PlantationLeavesUpgrade Level0 = new PlantationLeavesUpgrade(0, 2, 0, Color.white);
    public static readonly PlantationLeavesUpgrade Level1 = new PlantationLeavesUpgrade(20, 3, 1, Color.red);
    public static readonly PlantationLeavesUpgrade Level2 = new PlantationLeavesUpgrade(40, 4, 2, Color.green);
    public static readonly PlantationLeavesUpgrade Level3 = new PlantationLeavesUpgrade(60, 5f, 3, Color.blue);
    

    public static PlantationLeavesUpgrade[] Upgrades = new PlantationLeavesUpgrade[4]
    {
        Level0, Level1, Level2, Level3
    };


    private PlantationLeavesUpgrade(int cost, float value, int level, Color color) : base(cost, value, level, "Leaf Value")
    {
        Colour = color;
    }
    public readonly Color Colour;
}
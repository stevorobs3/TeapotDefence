using UnityEngine;

public class PlantationLeavesUpgrade : Upgrade
{
    public static readonly PlantationLeavesUpgrade Level0 = new PlantationLeavesUpgrade(0, 2, Color.white);
    public static readonly PlantationLeavesUpgrade Level1 = new PlantationLeavesUpgrade(20, 3, Color.red);
    public static readonly PlantationLeavesUpgrade Level2 = new PlantationLeavesUpgrade(40, 4, Color.green);
    public static readonly PlantationLeavesUpgrade Level3 = new PlantationLeavesUpgrade(60, 5f, Color.blue);
    

    public static PlantationLeavesUpgrade[] Upgrades = new PlantationLeavesUpgrade[4]
    {
        Level0, Level1, Level2, Level3
    };


    private PlantationLeavesUpgrade(int cost, float value, Color color) : base(cost, value, "Leaf Value")
    {
        _colour = color;
    }

    private Color _colour;

    public Color Colour
    {
        get
        {
            return _colour;
        }
    }
}
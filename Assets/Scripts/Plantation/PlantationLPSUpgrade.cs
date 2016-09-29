using UnityEngine;

public class PlantationLPSUpgrade : Upgrade
{
    public static readonly PlantationLPSUpgrade Level0 = new PlantationLPSUpgrade(0, 1, Color.white);
    public static readonly PlantationLPSUpgrade Level1 = new PlantationLPSUpgrade(20, 1.25f, Color.red);
    public static readonly PlantationLPSUpgrade Level2 = new PlantationLPSUpgrade(40, 1.5f, Color.green);
    public static readonly PlantationLPSUpgrade Level3 = new PlantationLPSUpgrade(60, 2f, Color.blue);
    

    public static PlantationLPSUpgrade[] Upgrades = new PlantationLPSUpgrade[4]
    {
        Level0, Level1, Level2, Level3
    };


    private PlantationLPSUpgrade(int cost, float value, Color32 colour) : base(cost, value, "LPS")
    {
        _colour = colour;
    }
    private Color32 _colour;

    public Color32 Colour
    {
        get
        {
            return _colour;
        }
    }
}
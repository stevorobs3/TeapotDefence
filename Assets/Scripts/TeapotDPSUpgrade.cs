
using UnityEngine;

public class TeapotDPSUpgrade : TeapotUpgrade
{
    public static readonly TeapotDPSUpgrade Level0 = new TeapotDPSUpgrade(20, 5, Color.white);
    public static readonly TeapotDPSUpgrade Level1 = new TeapotDPSUpgrade(40, 7, Color.red);
    public static readonly TeapotDPSUpgrade Level2 = new TeapotDPSUpgrade(60, 9, Color.green);
    public static readonly TeapotDPSUpgrade Level3 = new TeapotDPSUpgrade(80, 11, Color.blue);


    public static TeapotDPSUpgrade[] Upgrades = new TeapotDPSUpgrade[4]
    {
        Level0, Level1, Level2, Level3
    };


    private TeapotDPSUpgrade(int cost, float value, Color32 colour) : base(cost, value, "DPS")
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


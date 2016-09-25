public abstract class TeapotUpgrade
{
    private int _cost;
    private float _value;
    private string _title;

    public TeapotUpgrade(int cost, float value, string title) {
        _value = value;
        _cost = cost;
        _title = title;
    }        

    public float Value
    {
        get
        {
            return _value;
        }
    }

    public int Cost
    {
        get
        {
            return _cost;
        }
    }

    public string Title
    {
        get
        {
            return _title;
        }
    }
}
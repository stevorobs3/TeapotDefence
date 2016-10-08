public abstract class Upgrade
{
    public Upgrade(int cost, float value, int level, string title) {
        Value = value;
        Cost = cost;
        Title = title;
        Level = level;
    }

    public readonly float Value;
    public readonly int Cost;
    public readonly string Title;
    public readonly int Level;
}

public class SpawnWave
{
    private static readonly SpawnWave Level0 = new SpawnWave(10, 1, 0f, 3f);
    private static readonly SpawnWave Level1 = new SpawnWave(10, 2, 1f, 2.5f);
    private static readonly SpawnWave Level2 = new SpawnWave(20, 5, 1f, 2.25f);
    private static readonly SpawnWave Level3 = new SpawnWave(15, 10, 1f, 2f);
    private static readonly SpawnWave Level4 = new SpawnWave(10, 15, 1f, 1.5f);
    public static readonly SpawnWave[] Levels = new SpawnWave[]
    {
        Level0,
        Level1,
        Level2,
        Level3,
        Level4,
    };

    private SpawnWave(
        int   numCafetieres,
        int   numItalianStoves,
        float timeBeforeFirstSpawn,
        float timeBetweenSpawns
    )
    {
        NumCafetieres        = numCafetieres;
        NumItalianStoves     = numItalianStoves;
        TimeBeforeFirstSpawn = timeBeforeFirstSpawn;
        TimeBetweenSpawns    = timeBetweenSpawns;
    }

    public readonly int NumCafetieres;
    public readonly int NumItalianStoves;
    public readonly float TimeBetweenSpawns;
    public readonly float TimeBeforeFirstSpawn;

    public int Count
    {
        get
        {
            return NumCafetieres + NumItalianStoves;
        }
    }

}

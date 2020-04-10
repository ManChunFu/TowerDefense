using System.Collections.Generic;

public class Map
{
    public IEnumerable<MapCell> GridCells { get; set; }
    public IEnumerable<SpawnWavesData> SpawnWaves { get; set; }
}
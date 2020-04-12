using System.Collections.Generic;

public class Maps
{
    public int RowCounts { get; set; }
    public int ColumnCounts { get; set; }

    public IEnumerable<MapCell> GridCells { get; set; }
    public IEnumerable<SpawnWavesData> SpawnWaves { get; set; }
}
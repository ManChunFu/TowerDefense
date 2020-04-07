using System;
using System.Collections.Generic;
using System.IO;
using TowerDefense;

public class MapReader
{
    public Map ReadMap(MapTypes mapType)
    {
        string filePath = ProjectPaths.RESOURCES_MAP_SETTINGS + Enum.GetName(typeof(MapTypes), mapType) + ".txt";

        HashSet<MapCell> resultMap = new HashSet<MapCell>();
        List<string> lines = new List<string>();
        List<SpawnWavesData> spawnWaves = new List<SpawnWavesData>();

        using (StreamReader sr = new StreamReader(filePath))
        {
            bool readingMap = true;
            int spawnWavesIndex = 1;
            do
            {
                string line = sr.ReadLine();

                if (line == "#")
                    readingMap = false;
                else if (readingMap)
                    lines.Add(line);
                else
                {
                    string[] spawnNumbers = line.Split(' ');
                    if (spawnNumbers.Length == 2)
                        spawnWaves.Add(new SpawnWavesData(spawnWavesIndex++, int.Parse(spawnNumbers[0]), int.Parse(spawnNumbers[1])));
                }
            } while (!sr.EndOfStream);
        }

        for (int lineIndex = lines.Count - 1, columnIndex = 0; lineIndex >= 0; lineIndex--, columnIndex++)
        {
            string line = lines[lineIndex];
            for (int rowIndex = 0; rowIndex < line.Length; rowIndex++)
            {
                char item = line[rowIndex];

                resultMap.Add(new MapCell(rowIndex, columnIndex, item));
            }
        }
        return new Map { GridCells = resultMap, SpawnWaves = spawnWaves };
    }
}

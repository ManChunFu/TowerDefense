public class MapCell
{
    public int XPos2D { get;  }
    public int YPos2D { get;  }
    public char ObjectType { get; }
    
    public MapCell(int row, int column, char objectType)
    {
        XPos2D = row;
        YPos2D = column;
        ObjectType = objectType;
    }
}
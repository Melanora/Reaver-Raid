using UnityEngine;

public static class BlockMatrixStraight
{

    public static int[,] matrix {get; private set;} 

    static BlockMatrixStraight()
    {
        matrix = new int[,] 
        {
            {0,0,0,0}, // 0 - zero
            {1,1,2,2}, // 1 - narrow
            {2,2,3,3}, // 2 - mid
            {3,2,2,4}, // 3 - wide
            {4,4,4,3}, // 4 - island

        };
    }


}

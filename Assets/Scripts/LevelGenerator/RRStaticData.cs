using UnityEngine;



    public class BlockLines
    {
        public int[] Lines {get; private set;}

        public BlockLines(int l1, int l2, int l3, int l4)
        {
            Lines = new int[] {l1, l2, l3, l4};
        }

    }

public static class RRStaticData
{
    public static int BlockHeight {get; private set;}
    public static int VariantsCount {get; private set;}

    public static int[,] Variants {get; private set;} 
    public static int[,] BridgeVariants {get; private set;} 

    static RRStaticData()
    {
        // river width offset from previous
        Variants = new int[,] 
        {            
                {0, 0, 0, 0}, //  0
                {0, 2, 0, 0}, //  1
                {0, 2, 2, 0}, //  2
                {0, 2, 2, 2}, //  3
                {2, 2, 2, 2}  //  4
        };

        // river width absolute
        BridgeVariants = new int[,] 
        {            
                {2, 2, 2, 4}, // normal bridge
                {4, 4, 4, 6}  // wide bridge
        };

        VariantsCount = Variants.GetLength(0);
        BlockHeight = Variants.GetLength(1);
    }




}


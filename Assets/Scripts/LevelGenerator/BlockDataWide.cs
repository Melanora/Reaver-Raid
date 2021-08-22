using UnityEngine;

public static class BlockDataWide
{
    // {r, o, i}
    // r - river width
    // o - horizontal offset from center
    // i - island width
    //
    // max river width: 36 - wide, 20 - straight
    //

    public static int[,,] Variants {get; private set;} 
    public static int VariantsCount {get; private set;}
    public static int BlockHeight {get; private set;}



    static BlockDataWide()
    {
        BlockHeight = 4;

        Variants = new int[,,] 
        {            
            {   // variant zero
                {4,0,0}, 
                {4,0,0}, 
                {4,0,0}, 
                {4,0,0} 
            },
            {   
                {8,0,0}, 
                {8,0,0}, 
                {8,0,0}, 
                {8,0,0} 
            },
            {   
                {12,0,0}, 
                {12,0,0}, 
                {12,0,0}, 
                {12,0,0} 
            },
            {   
                {16,0,0}, 
                {16,0,0}, 
                {16,0,0}, 
                {16,0,0} 
            },
            {   
                {20,0,0}, 
                {20,0,0}, 
                {20,0,0}, 
                {20,0,0} 
            }
        };


    }




}


using UnityEngine;

public class LFSR
{
    private int _hi, _low;

    public int hi 
    {
        get
        {
            return _hi;
        }
    }

    public int low 
    {
        get
        {
            return _low;
        }
    }


    public LFSR()
    {
        _hi = 1;
        _low = 1;
    }

    public void Seed(int seedHi, int seedLow)
    {
        _hi = seedHi & 255;
        _low = seedLow & 255;
    }

    public void Next()
    {
        int acc, carry;
        acc = _hi;
        acc = acc << 3;
        acc = acc ^ _hi;
        acc = acc << 1;
        carry = (acc & 256) >> 8;
        _low = (_low << 1) | carry;
        carry = (_low & 256) >> 8;
        _hi = (_hi << 1) | carry;
        _low &= 255;
        _hi &= 255;
    }

    public void Randomize()
    {
        _hi = (int)(Random.value*255);
        _low = (int)(Random.value*255);
    }


}


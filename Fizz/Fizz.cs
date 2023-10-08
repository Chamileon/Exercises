using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Fizz : ICheckNumber
{
    private string shout;
    public Fizz(int input, string whatToShout) 
    {
        filter = (n) => n % input == 0 && n != 0;
        shout = whatToShout;
    }
    public Predicate<int> filter;
    public void CheckNumber(int i)
    {
        var flag = filter(i);
        if (flag) Debug.Log(shout);
    }
}

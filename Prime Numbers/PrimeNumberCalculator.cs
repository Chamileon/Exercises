using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CalculatorMode { None, Check, ListPrimes }
public class PrimeNumberCalculator : MonoBehaviour
{
    public CalculatorMode calculatorMode = CalculatorMode.None;
    public long checkedNumber;
    public delegate void PrimeNumberCallback(CalculatorMode mode);
    public PrimeNumberCallback primeNumberCallback;
    public void CheckNumber(CalculatorMode mode) 
    {
        if(mode == CalculatorMode.Check)
        {
            bool flag = PrimeCollector.DivisibleByOther(checkedNumber);
            if (!flag) { Debug.Log(checkedNumber + " is prime."); }
        }
        
    }
    public void ShowListPrimes(CalculatorMode mode) 
    {
        if(mode == CalculatorMode.ListPrimes) 
        {
            foreach (var prime in PrimeCollector.Primes)
            {
                Debug.Log(prime + " is prime.");
            }
        }
    }
    private void Start()
    {
        primeNumberCallback += CheckNumber;
        primeNumberCallback += ShowListPrimes;
    }
    IEnumerator CheckNumb(CalculatorMode mode) 
    {
        yield return new WaitUntil(PrimeCollector.ControllerFlag);
        if (mode == CalculatorMode.Check)
        {
            CheckNumber(mode);
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space)) 
        {
            primeNumberCallback(calculatorMode);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PrimeCollector : MonoBehaviour
{
    /// Exercise:
    /// Confirm that the input number is prime or not.
    /// Extra:
    /// AutoPrimeCalculator.
    public static PrimeCollector collector;
    public delegate void PrimeCollectorDelegate();
    private PrimeCollectorDelegate _collectorDelegate;
    public static List<long> Primes = new List<long>();
    private static long _current = 0;
    public static long ActualNumber { get { return _current ;}  private set { _current = value; } }
    public static long lastPrime;
    private static readonly int _maxIterations = 1000;
    private static bool _controllerFlag = false;
    public static Func<bool> ControllerFlag;
    public static void AddPrime(long input) 
    {
        Primes.Add(input);
        lastPrime = input;
    }
    private void Init()
    {
        collector = this;
        ControllerFlag = GetFlagController;
        Debug.Log("Initializing...");
    }

    private bool GetFlagController() 
    {
        return _controllerFlag;
    }

    private void Destroy() 
    {
        Debug.Log("Im dying...");
        Destroy(gameObject);
    }
    private void Awake()
    {
        _collectorDelegate = new PrimeCollectorDelegate(Init);
    }
    private void Start()
    {
        _collectorDelegate();
        _collectorDelegate = new PrimeCollectorDelegate(StartCalculatePrimes);
        _collectorDelegate();
    }
    public void StartCalculatePrimes() 
    {
        StartCoroutine(CalculatePrimes());
    }
    private IEnumerator CalculatePrimes()
    {
        ActualNumber = 1;
        for (int i = 0; i < _maxIterations; i++)
        {
            if (Primes.Count == 0) { AddPrime(ActualNumber); ActualNumber++; }
            else
            {
                if (DivisibleByOther(ActualNumber))
                {
                    Debug.Log("The " + ActualNumber + " is not a prime.");
                    ActualNumber++;
                }
                else
                {
                    Debug.Log("The " + ActualNumber + " is a prime.");
                    AddPrime(ActualNumber); ActualNumber++;
                }
                yield return null;
            }

        }
        
        _controllerFlag = true;
    }
    
    
    public static bool DivisibleByOther(long input) 
    {
        bool flag = false;
        foreach (long prime in Primes) 
        {
            if (input % prime == 0 && prime != 1 && prime != input) { flag = true; Debug.Log(input + " can be divided by: " + prime); break; }
        }
        return flag;
    }


    
}


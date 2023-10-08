using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public interface ICheckNumber 
{
    public void CheckNumber(int i);
} 
public class Room : MonoBehaviour
{
    [SerializeField][Range(0.1f, 3.5f)]
    private float time;
    private List<int> numbers = new List<int>();
    private Action<int> CheckNumber;
    private List<ICheckNumber> fizzs = new List<ICheckNumber>();
    private void InitNumbers() 
    {
        numbers.Clear();
        for (int i = 0; i < 100; i++) { numbers.Add(i); }
    }
    void Start()
    {
        InitNumbers();
        fizzs.Add(CreateAFizz(2, "Fizz!!"));
        fizzs.Add(CreateAFizz(3, "Buzz!!"));
        fizzs.Add(CreateAFizz(5, "Wazza!!"));
        fizzs.Add(CreateAFizz(7, "Wizzi!!"));
        StartCoroutine(Count(this.time));
    }
    private IEnumerator Count(float time) 
    {
        foreach (int numb in numbers) 
        {
            Debug.Log(numb);
            yield return new WaitForSecondsRealtime(time);
            Debug.Log(".");
            yield return new WaitForSecondsRealtime(time);
            Debug.Log("..");
            yield return new WaitForSecondsRealtime(time);
            Debug.Log("...");
            new WaitForSecondsRealtime(time * 0.5f);
            CheckNumber(numb);
        }
        yield return null;
    }
    private Fizz CreateAFizz(int input, string shout) 
    {
        Fizz fizz = new Fizz(input, shout);
        CheckNumber += fizz.CheckNumber;
        return fizz;
    }

    
}

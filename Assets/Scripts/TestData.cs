using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TestData
{
    string name;
    int age;

    public string GetName() => name;

    public TestData()
    {
        this.name = "None";
        this.age = 0;
    }
    public TestData(string name, int age)
    {
        this.name = name;
        this.age = age;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage
{
    public static Storage instance;

    int wood;
    int blackberry;

    private Storage()
    {
        if (instance == null)
        {
            instance = new Storage();
        }
        wood = 0;
        blackberry = 0;
    }

    public void AddResource(string resourceName, int amount)
    {
        switch (resourceName) {
            case "wood":
                wood += amount;
                break;
            case "blackBerry":
                blackberry += amount;
                break;
            default:
                break;
        }

    }

    public void TakeResource(string resourceName, int amount)
    {
        switch (resourceName)
        {
            case "wood":
                if (wood >= amount)
                    wood -= amount;
                break;
            case "blackBerry":
                if (blackberry >= amount)
                    blackberry -= amount;
                break;
            default:
                break;
        }

    }
}
public class StorageComponent : MonoBehaviour
{
    Storage storage;

    void Start()
    {
        storage = Storage.instance;
    }

    public void AddResource(string resourceName, int amount)
    {
        storage.AddResource(resourceName, amount);
    }

    public void TakeResource(string resourceName, int amount)
    {
        storage.TakeResource(resourceName, amount);
    }
}

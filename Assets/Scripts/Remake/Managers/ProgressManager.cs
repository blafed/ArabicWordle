using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class ProgressManager : Manager<ProgressManager>
{
    
    private IProgressSaver saver;
    private IProgressLoader loader;

    private ProgressData data;
    
    public bool isReady { get; private set; }

    protected override void Init()
    {
        saver = GetComponentInChildren<IProgressSaver>();
        loader = GetComponentInChildren<IProgressLoader>();
        StartCoroutine(LoadProgress());
    }

    ProgressData CreateDefaultData()
    {
        ProgressData data = new ProgressData();
        foreach (var p in ProgressConfig.instance.products)
        {
            data.products.Add(new ProductData
            {
                code =  p.code,
                amount = p.defaultAmount
            });
        }

        return data;
    }

    IEnumerator LoadProgress()
    {
        loader.Load();
        yield return new WaitUntil(() => loader.isDone);
        if (loader.data != null)
            data = loader.data;


        foreach (var x in data.products)
        {
            FlowOut.UpdateProduct(x);
        }
    }


    public void ApplyAdReward(){}

    public void IncAmount(ProductCode code, int amount)
    {
        SetAmount(code, GetAmount(code) + amount);
    }

    public void SetAmount(ProductCode code, int amount)
    {
        var p = data.products.Find(x => x.code == code);
        p.amount = amount;
        saver.Save(p);
        FlowOut.UpdateProduct(p);
    }

    public int GetAmount(ProductCode code)
    {
        var p=  data.products.Find(x => x.code == code);
        return p.amount;
    }
}

[System.Serializable]
public class ProductData
{
    public ProductCode code;
    public int amount;
}

[System.Serializable]
public class ProgressData
{
    public List<ProductData> products = new List<ProductData>();
}

public interface IProgressSaver
{
    bool isDone { get; }
    void SaveAll(ProgressData progressData);
    void Save(ProductData product);
}

public interface IProgressLoader
{
    bool isDone { get; }
    ProgressData data { get; }
    void Load();
}
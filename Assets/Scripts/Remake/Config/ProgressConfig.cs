using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = nameof(ProgressConfig), menuName = "Config/" + nameof(ProgressConfig))]

public class ProgressConfig : Config<ProgressConfig>
{
    
    public List<ProductInfo> products = new List<ProductInfo>();

    public ProductInfo GetProduct(ProductCode code)
    {
        //TODO this can be enhanced via caching
        return products.Find(x => x.code == code);
    }
    
    
    [System.Serializable]
    public class ProductInfo
    {
        [System.Serializable]
        public struct IconAmount
        {
            public int minAmount;
        }
        public ProductCode code;
        public string key;
        public int defaultAmount;
        [Header("UI")] public Sprite icon;
        public List<IconAmount> iconPerAmount = new List<IconAmount>();
        [FormerlySerializedAs("uiText")]
        public string text;
        [TextArea]
        [FormerlySerializedAs("uiHelp")]
        public string help;
    }
}

public enum ProductCode
{
    Unknown,
    Coins,
    Hints,
    Eliminations,
    HighScore,
    Score,
    
}

public interface IProduct
{
    ProductCode code { get; }
    int value { get; }
}

[System.Serializable]
public struct ProductEffect
{
    public ProductCode product;
    public int amount; //can be negative for decrease
}
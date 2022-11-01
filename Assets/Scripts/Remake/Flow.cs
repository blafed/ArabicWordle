using UnityEngine;


public static class FlowIn
{
    public static bool initial = true;
    public static bool showRewardedAd;
    public static bool playClassic;
    public static ElementCode open;
    


    public static void Clear()
    {
        initial = false;
        showRewardedAd = false;
        playClassic = false;
        open = ElementCode.None;
    }
}

public static class FlowOut
{
    public static string coins;
    public static bool rewardedAdLoaded;
    public static ProductCode rewardedAdProduct;
    public static int rewardedAdAmount;

    public static void UpdateProduct(ProductData productData)
    {
        int amount = productData.amount;
        switch (productData.code)
        {
            case ProductCode.Coins:
                coins = amount.ToString();
                break;
        }
    }

    public static string ProductAmount(ProductCode productCode)
    {
        switch (productCode)
        {
            case ProductCode.Coins:
                return coins;
        }

        return "";
    }
    
    public static string GetText(FlowOutText t)
    {
        switch (t)
        {
            case FlowOutText.Coins:
                return coins;
        }

        return "";
    }
}

public enum FlowOutText
{
    Unknown,
    Coins,
}


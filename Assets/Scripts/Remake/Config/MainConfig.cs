using UnityEngine;

[CreateAssetMenu(fileName = nameof(MainConfig), menuName = "Config/" + nameof(MainConfig))]
public class MainConfig : Config<MainConfig>
{
    public bool devMode = false;
}

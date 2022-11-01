using UnityEngine;

[CreateAssetMenu(fileName = nameof(UIConfig), menuName = "Config/" + nameof(UIConfig))]
public class UIConfig : Config<UIConfig>
{
    public float popupFadeTime = 0.3f;
}

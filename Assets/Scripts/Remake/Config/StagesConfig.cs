using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = nameof(StagesConfig), menuName = "Config/" + nameof(StagesConfig))]
public class StagesConfig : Config<StagesConfig>
{
    public StageCode initialStage;
    public StageCode homeStage;

    public List<StageInfo> stages = new List<StageInfo>();

    
}

[System.Serializable]
public class StageInfo
{
    public StageCode code;
    public StageTransition transition;
    public GameObject[] enterPrefabs;
    public ElementCode[] enterElements;
    [Header("For automatic transition")] public float duration;
    public StageCode nextStage;
}

public enum StageTransition
{
    Automatic,
    Manual
}
public enum StageCode
{
    Unknown,
    Intro,
    Main,
    Game,
    Store,
}


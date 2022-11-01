using System;
using System.Collections.Generic;
using UnityEngine;

public class StagesManager : Manager<StagesManager>
{

    class EnterObject
    {
        public GameObject gameObject;
        public IStageObject stageObject;
        public bool isInstantiated;
        public bool initialActive;
    }
    public StageCode stage { get; private set; }
    private List<EnterObject> enterObjects = new List<EnterObject>();

    private float timeCounter;
    private StageInfo stageInfo;

    protected override void Init()
    {
        Go(StagesConfig.instance.initialStage);
    }

    private void Update()
    {
        timeCounter += Time.deltaTime;
        if (stageInfo.transition == StageTransition.Automatic)
        {
            if (timeCounter > stageInfo.duration)
                Go(stageInfo.nextStage);
        }
    }


    public void Go(StageCode stage)
    {
        print($"Entering stage '{stage}'");
        if (this.stage != 0) ExitCurrent();
        this.stage = stage;
        EnterCurrent();
    }

    public void GoHome()
    {
        Go(StagesConfig.instance.homeStage);
    }

    void ExitCurrent()
    {
        foreach (var x in enterObjects)
        {
            if (x.stageObject != null)
                x.stageObject.OnStageExit();
            if (x.isInstantiated)
                Destroy(x.gameObject);
            else
                x.gameObject.SetActive(x.initialActive);
        }

        enterObjects.Clear();
    }

    void EnterCurrent()
    {
        timeCounter = 0;
        var info = StagesConfig.instance.stages.Find(x => x.code == stage);
        stageInfo = info;
        foreach (var x in info.enterPrefabs)
        {
            var go = Instantiate(x);
            IStageObject obj;
            if (go.TryGetComponent(out obj))
                obj.OnStageEnter(info);
            enterObjects.Add(new EnterObject{gameObject = go, stageObject = obj, isInstantiated = true});
        }

        foreach (var x in info.enterElements)
        {
            var element = ElementManager.instance.Find(x);
            if (element == null)
            {
                Debug.LogError($"Cannot find element '{x}'");
                continue;
            }
            var go = element.gameObject;
            IStageObject obj;
            if (go.TryGetComponent(out obj))
                obj.OnStageEnter(info);
            enterObjects.Add(new EnterObject {gameObject = go, stageObject = obj, isInstantiated = false, initialActive = go.activeSelf});
            go.SetActive(true);
        }
    }
    
    

}

public interface IStageObject
{
    GameObject gameObject { get; }
    void OnStageEnter(StageInfo stageInfo);
    void OnStageExit();
}
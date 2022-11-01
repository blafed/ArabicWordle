using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class IntroPanel : Element, IStageObject, IElement
{
	public Image splash;
	public Image logo;

	public void OnStageEnter(StageInfo stageInfo)
	{
		var dur = stageInfo.duration;
		Color initColor = splash.color;
		Sequence seq = DOTween.Sequence();
		seq.Append(splash.DOColor(initColor, dur * 0.25f));
		seq.AppendInterval(dur * 0.5f);
		seq.Append(splash.DOColor(Color.black, dur * 0.25f));
	}

	public void OnStageExit()
	{
	}

	public override ElementCode code => ElementCode.Page_Intro;
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class HintButton : MonoBehaviour, IHighlightable
{
	public TextMeshProUGUI countText;
	private Button button;
	private WordGuessManager wordGuessManager;
	[SerializeField] private Sprite activeSprite;
	[SerializeField] private Sprite inactiveSprite;

	private bool limitReached;


	public Button Button => button;
	public Image Image => Button.image;
	public Color DefaultColor { get; }
	public UIElement Element => UIElement.hint;

	private Color defaultColor;


	// Start is called before the first frame update
	void Awake()
	{
		button = GetComponent<Button>();
		defaultColor = Image.color;
	}

	private void Start()
	{
        PagesManager.Instance.highlightables.Add(this);

        SetCounter();
		wordGuessManager = GameManager.Instance.wordGuessManager;
		//countText.text = GameManager.Instance.HintsAvailable.ToString();
		GameManager.Instance.OnNewWord += ResetButton;
		GameManager.Instance.OnTextChanged += SetCounter;
	}

	void SetText()
	{
		countText.text = GameManager.Instance.HintsAvailable.ToString();
	}


	public void ResetButton()
	{
		limitReached = false;
		SetCounter();
	}

	public void SetCounter()
	{
		int endValue = GameManager.Instance.HintsAvailable;
        if (GameManager.Instance.IsTutorial)
            countText.DOText(endValue.ToString(), 0.25f);
        else
            countText.text = "";
		button.GetComponent<Image>().sprite = (endValue == 0 || limitReached) ? inactiveSprite : activeSprite;
	}

	public void SetInteractable(bool interactable)
	{
		button.interactable = interactable;
	}

	public void ShowHint()
	{
		if (GameManager.Instance.HintsAvailable >= 0 && limitReached)
		{
			NotificationsManager.Instance.SpawnMessage(0);
			return;
		}

		if (!GameManager.Instance.IsTutorial)
			if ((!GameManager.Instance.devMode && GameManager.Instance.HintsAvailable <= 0))
			{
				//PopupManager.Instance.OpenPopup(3);
				PagesManager.Instance.FlipPage(2);
				GameManager.Instance.SwitchState("store");
				return;
			}



		if (wordGuessManager.lettersHinted.Count <= 0)
		{
			return;
		}


		string word = GameManager.Instance.CurrentWordSimplified;
		Transform currentRow = wordGuessManager.wordGrid.GetChild(wordGuessManager.rowIndex);
		List<TextMeshProUGUI> letters = new List<TextMeshProUGUI>();

		foreach (Transform box in currentRow)
		{
			letters.Add(box.GetComponentInChildren<TextMeshProUGUI>());
		}

		int i = Random.Range(0, wordGuessManager.lettersHinted.Count);
		int index = wordGuessManager.lettersHinted[i];
		wordGuessManager.lettersHinted.RemoveAt(i);
		string hint = word[index].ToString();
		TextMeshProUGUI hintLetter;
		if (letters[4 - index].transform.parent.GetChild(1).childCount == 1)
		{
			letters[4 - index].transform.parent.GetChild(1).GetChild(0).gameObject.SetActive(true);

			hintLetter = letters[4 - index].transform.parent.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
			hintLetter.transform.position = transform.position;
		}
		else
		{
			hintLetter = Instantiate(letters[4 - index].gameObject, transform.position, Quaternion.identity, letters[4 - index].transform.parent.GetChild(1)).GetComponent<TextMeshProUGUI>();
			hintLetter.color = wordGuessManager.hintTextColor;
		}
		hintLetter.text = hint;
		Sequence seq = DOTween.Sequence();
		seq.Append(hintLetter.rectTransform.DOMove(letters[4 - index].rectTransform.position, 0.5f).SetEase(Ease.InOutSine));
		seq.Join(letters[4 - index].transform.parent.GetChild(1).GetComponent<CanvasGroup>().DOFade(1, 0.1f));
		seq.Append(hintLetter.rectTransform.DOShakeScale(0.05f, 0.5f, 1, 20));
		//seq.Join(letters[4 - index].transform.parent.GetChild(1).GetComponent<Image>().DOColor(wordGuessManager.hintColor, 0.1f));
		print(letters[4 - index].transform.parent.childCount);
		GameManager.Instance.HintsAvailable--;
		SetCounter();
		wordGuessManager.hintCalled = true;

		if (!GameManager.Instance.IsTutorial)
		{
			GameManager.Instance.timesHintUsed++;
			if (GameManager.Instance.timesHintUsed >= GameManager.Instance.hintLimit)
			{
				limitReached = true;
				button.GetComponent<Image>().sprite = inactiveSprite;
			}
		}
	}
}

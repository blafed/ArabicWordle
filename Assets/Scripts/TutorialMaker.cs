// using UnityEngine;
// using System.Collections;
// using System;
// using System.Collections.Generic;
// using System.Linq;

// public class TutorialMaker : Singleton<TutorialMaker>
// {

//     [System.Serializable]
//     class StepWords
//     {
//         public string[] words;
//         StepWords() { }
//         public StepWords(params string[] words)
//         {
//             this.words = words;

//         }

//         public string Last => words.Length > 0 ? words[words.Length - 1] : "";
//     }
//     [System.Serializable]
//     class Hints
//     {
//         public string welcome = "مرحبا بك في الجولة التعليمية لشرح اساسيات اللعبة";
//         public string findHidden = "هناك كلمة خفية وعليك ان تخمنها";
//         public string enterRandom = "ادخل كلمة عشوائية من خمس حروف, على سبيل المثال {0}";
//         public string inWord = "الاحرف المعلمة بالاحمر تعني ان الكلمة الخفية تحوي هذه الاحرف";
//         public string inWordMore = "يوجد الكثير من الاحرف الحمراء هذا يعني انك اقتربت من الحل";
//         public string hint = "تلميح: ادخل {0}";
//         public string inPlace = "الاحرف الزرقاء مثل الاحرف الحمراء ولكنها الان في ترتيبها الصحيح";
//         public string outWord = "الاحرف التي ادخلتها غير موجودة في الكلمة الخفية, حاول ادخال احرف غيرها";
//     }



//     [SerializeField]
//     StepWords[] stepWords = new StepWords[]
//     {
//         new StepWords( "ارقام","احباب","مراحب","مرحبا"),
//         new StepWords( "اعمدة","مائدة","مداومة","مدينة"),
//         new StepWords( "اسماك","تمساح", "محسود", "محاسب","محاسن"),
//     };
//     [SerializeField]
//     Hints hints;


//     public bool DisableTutorial { get; set; }
//     StepWords CurrentStepWords { get; set; }
//     string GoalWord => CurrentStepWords.Last;


//     private bool moveNext = false;
//     private float? timePoint;
//     private string notInWordLetters = "";
//     private int step;
//     private bool tutorialFinished = false;
//     private List<string> enteredWords = new List<string>();
//     private string lastEnteredWord;
//     private int highestInWord = 0;



//     private void Start()
//     {
//         GameManager.Instance.OnStateChanged += () =>
//         {
//             if (!DisableTutorial && GameManager.Instance.CurrentState.stateName == "Menu")
//             {
//                 BeginTutorial();
//                 DisableTutorial = true;
//             }
//         };
//         WordGuessManager.Instance.wordNotGuessedEvent.AddListener(OnWordNotGuessed);
//         WordGuessManager.Instance.wordGuessedEvent.AddListener(ResetTutorial);
//     }

//     public void BeginTutorial()
//     {
//         CurrentStepWords = stepWords[UnityEngine.Random.Range(0, stepWords.Length)];

//         var gamePage = FindObjectOfType<Game>();
//         WordGuessManager.Instance.wordMode = WordGuessManager.WordMode.single;
//         WordGuessManager.Instance.wordSingle = CurrentStepWords.Last;
//         PagesManager.Instance.FlipPage(gamePage);
//         GameManager.Instance.SetGameType(GameType.Classic);
//         GameManager.Instance.SwitchState("game");
//         tutorialFinished = false;

//         StartCoroutine(Tutorial());
//         print("Tutorial Began");
//     }

//     public void EndTutorial()
//     {
//         PagesManager.Instance.GoBack();
//         ResetTutorial();
//     }

//     public void ResetTutorial()
//     {
//         moveNext = false;
//         step = 0;
//         highestInWord = 0;
//         tutorialFinished = true;
//         StopAllCoroutines();
//         enteredWords.Clear();
//     }




//     int InPlace(string value, string target)
//     {
//         int counter = 0;
//         if (value.Length != target.Length)
//         {
//             Debug.LogError("Unexpected error");
//             return 0;
//         }
//         for (int i = 0; i < target.Length; i++)
//         {
//             if (value[i] == target[i])
//                 counter++;
//         }
//         return counter;
//     }
//     int InWord(string value, string target)
//     {
//         int counter = 0;
//         if (value.Length != target.Length)
//         {
//             Debug.LogError("Unexpected error");
//             return 0;
//         }
//         for (int i = 0; i < value.Length; i++)
//         {
//             if (target.Contains(value[i]))
//                 counter++;
//         }
//         return counter;
//     }




//     IEnumerator Tutorial()
//     {
//         ShowHint(hints.welcome);
//         yield return new WaitUntil(() => ShouldMoveNextTimeout(2));

//         while (!tutorialFinished)
//         {
//             ShowHint(string.Format(hints.enterRandom,
//                 CurrentStepWords.words[UnityEngine.Random.Range(9,  CurrentStepWords.words.Length - 1)]));
//             yield return new WaitUntil(ShouldMoveNext);


//             ShowHint(string.Format(hints.));

//         }

//     }
//     void ShowHint(string text)
//     {
//         //TODO
//     }
//     bool ShouldMoveNext()
//     {
//         if (moveNext)
//         {
//             moveNext = false;
//             return true;
//         }
//         return false;
//     }
//     bool ShouldMoveNextTimeout(float timeout)
//     {
//         if (!timePoint.HasValue)
//             timePoint = Time.time;
//         if (Time.time > timePoint.Value + timeout || moveNext)
//         {
//             timePoint = null;
//             return true;
//         }
//         return false;
//     }


//     void OnWordNotGuessed() {
//         moveNext = true;
//         lastEnteredWord = WordGuessManager.Instance.LastEnteredWord;
//         enteredWords.Add(lastEnteredWord);
//         foreach (var c in lastEnteredWord)
//             if (!GoalWord.Contains(c))
//                 notInWordLetters += c;

//     }
//     void OnWordGuessed() {
//         ResetTutorial();
//     }




// }
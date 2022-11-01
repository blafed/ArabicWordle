using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class Tutorial
{
    [System.Serializable]
    public class Hints
    {
        public const string Welcome = "مرحبا بك في الجولة التعليمية لشرح اساسيات اللعبة";
        public const string FindHidden = "هناك كلمة خفية وعليك ان تخمنها";
        public static string EnterRandom(string arg) => string.Format("ادخل كلمة عشوائية من خمس حروف, على سبيل المثال {0}", arg);
        public const string InWord = "الاحرف المعلمة بالاحمر تعني ان الكلمة الخفية تحوي هذه الاحرف";
        public const string InWordMore = "يوجد الكثير من الاحرف الحمراء هذا يعني انك اقتربت من الحل";
        public static string Hint(string arg)=>string.Format( "تلميح: ادخل {0}", arg);
        public const string InPlace = "الاحرف الزرقاء مثل الاحرف الحمراء ولكنها الان في ترتيبها الصحيح";
        public const string NotInWord = "حاول عدم ادخال الاحرف الرمادية لانها غير موجودة في الكلمة الخفية";
        public const string NoHintExist = "حاول ان تدخل كلمة مكونة من الاحرف الزرقاء والحمراء";
        public const string HintTool = "استخدم اداة العدسة لكشف حرف مجهول من الكلمة";
        public const string EliminateTool = "استخدم اداة السهم لازالة 3 حروف من لوحة المفاتيح";
        public const string InWord2 = "هذه الاحرف من ضمن الكلمة";
        public const string InPlace2 = "هذه الاحرف من ضمن الكلمة وفي مكانها الصحيح";
        public const string NotInWord2 = "هذه الاحرف ليست من ضمن الكلمة";
        public const string Tut1 = "حاول ان تخمن الكلمة المجهولة من 5 احرف";

        public static string CorrectWord(string s) => string.Format("الان خمن الكلمة. تلميح: الكلمة هي \"{0}\"", s);

    }

    public static string[][] StepWords =
    {
        new string[]{ "ارقام","احباب","مراحب","مرحبا" },
        new string[] {"اعمدة","مائدة","مداومة","مدينة"},
        new string[]{ "اسماك","تمساح", "محسود", "محاسب","محاسن" },
    };

    public class Data
    {
        public Stage stage;
        public WordResult lastWordResult;
        public int highestInWord;
        public int highestInPlace;
        public string notInWordLetters = "";
        public List<string> enteredWords = new List<string>();
        public IEnumerator coroutine;
        public bool moveNext = false;
        public float timePoint;
        public int tries = 0;
        public int StepWordsIndex { get; }
        public string[] SelectedStepWords => StepWords[StepWordsIndex];
        public string GoalWord => SelectedStepWords[SelectedStepWords.Length - 1];
        public string GetHintWord()
        {
            int existWords = 0;
            foreach (var x in SelectedStepWords)
                if (enteredWords.Contains(x))
                    existWords++;
            int rand = Random.Range(0, SelectedStepWords.Length - existWords);

            int counter = 0;

            foreach(var x in SelectedStepWords)
            {
                if (enteredWords.Contains(x))
                    continue;
                if (counter == rand)
                    return x;
                counter++;
            }
            return "";
        }

        public bool ShouldMoveNext()
        {
            if (moveNext)
            {
                moveNext = false;
                return true;
            }
            return false;
        }
        public WordResult GetWordResult(string word)
        {
            if (word.Length != GoalWord.Length)
            {
                Debug.LogError("Unexpected error");
            }

            int inWord = 0, inPlace = 0, notInWord = 0;
            for (int i = 0; i < GoalWord.Length; i++)
            {
                if (word[i] == GoalWord[i])
                    inPlace++;
            }
            for (int i = 0; i < GoalWord.Length; i++)
            {
                if (GoalWord.Contains(word[i]))
                    inWord++;
            }
            foreach(var x in word)
            {
                if (notInWordLetters.Contains(x))
                    notInWord++;
            }

            return new WordResult
            {
                inWord = inWord,
                inPlace = inPlace,
                notInWord = notInWord
            };
        }



        public Data()
        {
            StepWordsIndex = Random.Range(0, StepWords.Length);
        }

    }
    public struct WordResult
    {
        public int inWord;
        public int inPlace;
        public int notInWord;
    }

    public enum UIElement
    {
        Keyboard,
        EliminateTool,
        HintTool,
        Coins,
        BackArrow,
        Grid,
        Rows,
        Cells,
    }                   

    public enum Stage
    {
        Welcome,
        GuessTheWord,
        AddLetter,
        PressEnter,
        LookToNotInWord,
        LookToInWord,
        LookToInPlace,
        TryEliminate,
        TryHint,
        AddWord,
        AddCorrectWord,
        

    }
}

public interface IHighlightable
{
    Transform transform { get; }
    UIElement Element { get; }
    Button Button{ get; }
    Image Image { get; }
}
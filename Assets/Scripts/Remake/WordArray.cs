using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public static class WordArray
{
	public static readonly string[] WordList =
		{
			"أبحاث",
			"أبريل",
			"أبصار",
			"أثقال",
			"أحذية",
			"أحياء",
			"أخبار",
			"أخلاق",
			"أرضية",
			"أركان",
			"أريحا",
			"أزياء",
			"أسئلة",
			"أسبوع",
			"أستاذ",
			"أسلوب",
			"أعمال",
			"أغنية",
			"أقمشة",
			"ألبوم",
			"أمعاء",
			"أمواج",
			"إبريق",
			"إحصاء",
			"إسلام",
			"إسهال",
			"إعادة",
			"إعلان",
			"إعلان",
			"إنجيل",
			"إنشاء",
			"إنفاق",
			"ابتسم",
			"ابزيم",
			"اجتمع",
			"احترم",
			"احتشد",
			"احتفل",
			"اختطف",
			"اختفى",
			"ارتبط",
			"ارتدى",
			"ارتفع",
			"ارتكب",
			"اريكة",
			"استغل",
			"استفز",
			"استلف",
			"استلم",
			"استمر",
			"استمع",
			"اسلوب",
			"اشتبك",
			"اشترى",
			"اشمأز",
			"اعتبر",
			"اعتدى",
			"اعتذر",
			"اعتذر",
			"اعترف",
			"اعترف",
			"اعتقد",
			"اعتمد",
			"اعتنق",
			"افتخر",
			"اقتحم",
			"اقتصد",
			"اقتصر",
			"اقتنع",
			"اقليم",
			"اكتشف",
			"اكتفى",
			"امتنع",
			"امرأة",
			"انتشر",
			"انتظر",
			"انتقد",
			"انتقم",
			"انتهك",
			"انتهى",
			"انخفض",
			"اندلع",
			"انفجر",
			"انفصل",
			"انفضح",
			"باردة",
			"بامية",
			"بحرية",
			"بحرين",
			"بحيرة",
			"بداية",
			"بدنية",
			"برغوث",
			"بركان",
			"برمجة",
			"بضاعة",
			"بطاقة",
			"بطاقة",
			"بطالة",
			"بطولة",
			"بعوضة",
			"بقالة",
			"بلوزة",
			"بودرة",
			"بورصة",
			"بوشار",
			"بوصلة",
			"بيروت",
			"تأمين",
			"تاريخ",
			"تاكسي",
			"تبادل",
			"تباهى",
			"تثاؤب",
			"تجاهل",
			"تجاوز",
			"تجديف",
			"تجسيد",
			"تجميل",
			"تخدير",
			"تداول",
			"تدهور",
			"تذكرة",
			"ترابي",
			"ترخيص",
			"ترفيه",
			"ترقية",
			"ترويج",
			"تسخين",
			"تسلية",
			"تشاور",
			"تشريح",
			"تشرين",
			"تشويه",
			"تصريح",
			"تصنيف",
			"تصوير",
			"تصوير",
			"تظاهر",
			"تعادل",
			"تعامل",
			"تعاون",
			"تعليق",
			"تغطية",
			"تفاقم",
			"تفاهم",
			"تفصيل",
			"تقاعد",
			"تقاعد",
			"تقديم",
			"تقليد",
			"تقويم",
			"تقويم",
			"تكليف",
			"تلفون",
			"تلميذ",
			"تمثال",
			"تمريض",
			"تمرين",
			"تمساح",
			"تمهيد",
			"تمويل",
			"تنازل",
			"تناول",
			"تنجيد",
			"تنفسي",
			"تنمية",
			"تنورة",
			"تهجئة",
			"توابل",
			"توازن",
			"توراة",
			"توصيل",
			"توظيف",
			"ثانية",
			"ثعبان",
			"ثقافة",
			"ثلاجة",
			"ثنائي",
			"جائزة",
			"جامعة",
			"جاموس",
			"جبيرة",
			"جراحة",
			"جريدة",
			"جزائر",
			"جزيرة",
			"جمباز",
			"جمبري",
			"جمهور",
			"جنازة",
			"جوافة",
			"جوهرة",
			"جوهري",
			"حادثة",
			"حاسبة",
			"حاسوب",
			"حاسوب",
			"حافظة",
			"حافلة",
			"حديقة",
			"حصيلة",
			"حضارة",
			"حضانة",
			"حقيبة",
			"حلزون",
			"حموضة",
			"حنجرة",
			"خارطة",
			"خالية",
			"خجلان",
			"خدامة",
			"خرشوف",
			"خرطوم",
			"خريطة",
			"خزانة",
			"خسارة",
			"خسارة",
			"خطوبة",
			"خطيئة",
			"خميرة",
			"خنزير",
			"خياطة",
			"دائرة",
			"داعية",
			"دراجة",
			"دراجة",
			"دعاية",
			"دولاب",
			"ديوان",
			"ذاكرة",
			"راهبة",
			"ردفان",
			"رذيلة",
			"رسالة",
			"رطوبة",
			"رقابة",
			"رمادي",
			"رماية",
			"رمضان",
			"رهينة",
			"رواية",
			"رياضة",
			"زائدة",
			"زاوية",
			"زبدية",
			"زراعة",
			"زرافة",
			"زعنفة",
			"زوبعة",
			"زيتون",
			"سباحة",
			"سبانخ",
			"سبورة",
			"ستارة",
			"سجادة",
			"سحابة",
			"سرطان",
			"سفينة",
			"سلبية",
			"سماعة",
			"سماعة",
			"سنجاب",
			"سودان",
			"سوريا",
			"سياحة",
			"سيارة",
			"سيارة",
			"سيناء",
			"سينما",
			"سينما",
			"شاحنة",
			"شامبو",
			"شبعان",
			"شخصية",
			"شخصية",
			"شرعية",
			"شريان",
			"شريحة",
			"شطرنج",
			"شعبان",
			"شهادة",
			"شوفان",
			"شيطان",
			"صابون",
			"صاروخ",
			"صحافة",
			"صحافة",
			"صحراء",
			"صحيفة",
			"صرافة",
			"صعوبة",
			"صنارة",
			"صناعة",
			"صنبور",
			"صندوق",
			"صنعاء",
			"صيدلة",
			"ضاحية",
			"ضريبة",
			"طائرة",
			"طائفة",
			"طائفي",
			"طابور",
			"طاولة",
			"طاولة",
			"طباعة",
			"طريقة",
			"طلاقة",
			"طوابع",
			"طوارئ",
			"ظاهرة",
			"عائلة",
			"عائلي",
			"عارضة",
			"عاصفة",
			"عاصمة",
			"عاطفي",
			"عالمي",
			"عالية",
			"عبارة",
			"عبقري",
			"عدسات",
			"عدوان",
			"عريضة",
			"عسكري",
			"عصابة",
			"عصفور",
			"عصيان",
			"عطشان",
			"عقلية",
			"عقيدة",
			"علاقة",
			"علاوة",
			"عمارة",
			"عمارة",
			"عملاق",
			"عملية",
			"عيادة",
			"غثيان",
			"غسالة",
			"غسالة",
			"غفران",
			"فائدة",
			"فاكهة",
			"فجعان",
			"فراشة",
			"فرامل",
			"فرحان",
			"فرشاة",
			"فستان",
			"فضائي",
			"فضولي",
			"فكاهة",
			"فيضان",
			"قائمة",
			"قاعدة",
			"قاموس",
			"قاهرة",
			"قبيلة",
			"قداحة",
			"قذيفة",
			"قرنفل",
			"قزحية",
			"قصيدة",
			"قصيرة",
			"قلاية",
			"قلبية",
			"قنبلة",
			"قواعد",
			"قيامة",
			"قيامة",
			"كارثة",
			"كرتون",
			"كريمة",
			"كزبرة",
			"كفاءة",
			"كلمات",
			"كمثرى",
			"كنيسة",
			"لافتة",
			"لياقة",
			"لياقة",
			"ليمون",
			"مأدبة",
			"مأساة",
			"مؤخرة",
			"مئذنة",
			"مائدة",
			"مانجو",
			"مباشر",
			"مبالغ",
			"متأخر",
			"متخلف",
			"متدين",
			"متصفح",
			"متطرف",
			"متطوع",
			"متعصب",
			"متعفن",
			"متفرق",
			"متنوع",
			"متهور",
			"متوسط",
			"مثانة",
			"مجاور",
			"مجتهد",
			"مجهول",
			"محاسب",
			"محايد",
			"محترف",
			"محروق",
			"محصور",
			"محفظة",
			"مخترع",
			"مختلف",
			"مخطوب",
			"مدافع",
			"مدبلج",
			"مدرسة",
			"مدرسة",
			"مدونة",
			"مدينة",
			"مذهول",
			"مرادف",
			"مراسل",
			"مراقب",
			"مراكش",
			"مراهق",
			"مربية",
			"مرتاح",
			"مرتبة",
			"مرتقب",
			"مرجان",
			"مرحاض",
			"مروحة",
			"مزارع",
			"مسألة",
			"مساحة",
			"مساعد",
			"مسافة",
			"مسبحة",
			"مستعد",
			"مستند",
			"مستوى",
			"مسطرة",
			"مسكين",
			"مسلوق",
			"مسواك",
			"مسيرة",
			"مشاغب",
			"مشترك",
			"مشروب",
			"مشروع",
			"مشروع",
			"مشغول",
			"مشكلة",
			"مشهور",
			"مصباح",
			"مصطلح",
			"مصفاة",
			"مصلحة",
			"مصيبة",
			"مضغوط",
			"مطرقة",
			"مطفأة",
			"معالج",
			"معتدل",
			"معجزة",
			"معجون",
			"معركة",
			"معقول",
			"معيار",
			"مغارة",
			"مغسلة",
			"مغوار",
			"مفاجئ",
			"مفتاح",
			"مفروش",
			"مقابل",
			"مقالة",
			"مقاول",
			"مقبرة",
			"مقبول",
			"مقتصد",
			"مقدار",
			"مقطوع",
			"مقياس",
			"مكتئب",
			"مكتبة",
			"مكتبة",
			"مكسور",
			"مكنسة",
			"مكواة",
			"مكياج",
			"ملائة",
			"ملابس",
			"ملحوظ",
			"ملعقة",
			"مناسب",
			"مناعة",
			"منتخب",
			"منتدى",
			"منتصف",
			"منحاز",
			"منخار",
			"منخفض",
			"مندهش",
			"منديل",
			"منشأة",
			"منشار",
			"منطقة",
			"منقار",
			"مهارة",
			"مهندس",
			"موهبة",
			"ميثاق",
			"ميدان",
			"ميزان",
			"ميلاد",
			"ميناء",
			"نافذة",
			"نباتي",
			"نظارة",
			"نظارة",
			"نظرية",
			"نعامة",
			"نفاية",
			"نموذج",
			"نهاية",
			"هراوة",
			"هندسة",
			"هيمنة",
			"واقعي",
			"والدة",
			"وزارة",
			"وسائط",
			"وساطة",
			"وظيفة",
			"وقاية",
			"ولاية",
			"وليمة",
			"وهران"
		};
	public static string[] WordsArrays(string letter)
	{
		//List<string> words = new List<string>();
		var csv = Resources.Load<TextAsset>($"Words/{letter}");
		var words = csv.text.Split(",");
		return words;
	}

	public static readonly Dictionary<string, string[]> AllWordsDict = new Dictionary<string, string[]>(){
		{"ا", WordsArrays("alif")},
		{"ب", WordsArrays("baa2")},
		{"ت", WordsArrays("taa2")},
		{"ث", WordsArrays("thaa2")},
		{"ج", WordsArrays("geem")},
		{"ح", WordsArrays("haa2")},
		{"خ", WordsArrays("khaa2")},
		{"د", WordsArrays("daal")},
		{"ذ", WordsArrays("zay")},
		{"ر", WordsArrays("ra2")},
		{"ز", WordsArrays("zayn")},
		{"س", WordsArrays("seen")},
		{"ش", WordsArrays("sheen")},
		{"ص", WordsArrays("saad")},
		{"ض", WordsArrays("daad")},
		{"ط", WordsArrays("ta2")},
		{"ظ", WordsArrays("za2")},
		{"ع", WordsArrays("ain")},
		{"غ", WordsArrays("ghain")},
		{"ف", WordsArrays("faa2")},
		{"ق", WordsArrays("qaaf")},
		{"ك", WordsArrays("kaaf")},
		{"ل", WordsArrays("laam")},
		{"م", WordsArrays("meem")},
		{"ن", WordsArrays("noon")},
		{"ه", WordsArrays("ha2")},
		{"و", WordsArrays("waw")},
		{"ي", WordsArrays("ya2")}
	};

    public static readonly string[] Letters = {
        "ا",
        "ب",
        "ت",
        "ث",
        "ج",
        "ح",
        "خ",
        "د",
        "ذ",
        "ر",
        "ز",
        "س",
        "ش",
        "ص",
        "ض",
        "ط",
        "ظ",
        "ع",
        "غ",
        "ف",
        "ق",
        "ك",
        "ل",
        "م",
        "ن",
        "ه",
        "و",
        "ي",
        "ؤ",
        "ء",
        "ئ",
        "ء"
    };

	public static int LetterCode(string letter)
	{
		return Array.IndexOf(Letters, letter);
	}

	public static string SimplifyWord(string word)
	{
		word = Regex.Replace(word, @"[^\u0600-\u06ff]", "");
		word = Regex.Replace(word, @"[أ|إ|آ]", "ا");
		word = Regex.Replace(word, @"[ى]", "ي");
		return word;
	}

	public static bool WordNotInDictionary(string word)
	{
		//WordArray.Start();
		return (!WordArray.AllWordsDict.ContainsKey(word[0].ToString()) ||
		        System.Array.IndexOf(WordArray.AllWordsDict[word[0].ToString()], word) == -1);
	}
	

}
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GenericButton : MonoBehaviour, IHighlightable
{
    [SerializeField] UIElement element;

    public UIElement Element => element;
    public Button Button => button;
    public Image Image => Button.image;
    public Color DefaultColor => defaultColor;

    private Button button;
    private Color defaultColor;
    void Start()
    {
        HighlightManager.Instance.Add(this);
        button =GetComponent<Button>();
        defaultColor = Image.color;
    }

}

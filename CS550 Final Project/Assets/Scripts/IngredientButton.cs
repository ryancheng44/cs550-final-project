using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class IngredientButton : MonoBehaviour
{
    [HideInInspector] public Ingredient ingredient;
    [SerializeField] private Color selectedColor;

    private Image image;
    private bool isSelected = false;

    // Start is called before the first frame update
    void Start() => image = GetComponent<Image>();

    public void SelectIngredient()
    {
        isSelected = !isSelected;

        if (isSelected)
        {
            RecipeManager.instance.userIngredients.Add(ingredient);
            image.color = selectedColor;
        }
        else
        {
            RecipeManager.instance.userIngredients.Remove(ingredient);
            image.color = Color.white;
        }
    }
}

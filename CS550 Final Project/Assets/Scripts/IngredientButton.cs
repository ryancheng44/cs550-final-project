using UnityEngine;
using UnityEngine.UI;

// Require the GameObject this script is attached to have an Image component
[RequireComponent(typeof(Image))]
public class IngredientButton : MonoBehaviour
{
    // The Ingredient object this button represents
    [HideInInspector] public Ingredient ingredient;

    // The color the button will change to when selected
    [SerializeField] private Color selectedColor;

    // The Image component of the GameObject
    private Image image;

    // A flag to track whether the button is selected or not
    private bool selected = false;

    // Start is called before the first frame update
    // Get the Image component from the GameObject this script is attached to
    void Start() => image = GetComponent<Image>();

    // This method is called when the button is clicked
    public void OnClick()
    {
        // Toggle the selected state
        selected = !selected;

        if (selected)
        {
            // If the button is selected, add its ingredient to the user's ingredients
            // and change its color to the selected color
            RecipeManager.instance.userIngredients.Add(ingredient);
            image.color = selectedColor;
        }
        else
        {
            // If the button is deselected, remove its ingredient from the user's ingredients
            // and change its color back to white
            RecipeManager.instance.userIngredients.Remove(ingredient);
            image.color = Color.white;
        }
    }
}

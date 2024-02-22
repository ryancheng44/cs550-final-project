using UnityEngine;

public class RecipeButton : MonoBehaviour
{
    [HideInInspector] public Recipe recipe;
    public void OpenRecipe() => Application.OpenURL(recipe.websiteURL);
}

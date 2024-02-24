using UnityEngine;

public class RecipeButton : MonoBehaviour
{
    // The Recipe object this button represents
    [HideInInspector] public Recipe recipe;

    // Define a public method that will be called when the button is clicked
    // This method opens the URL of the recipe in the default web browser
    public void OnClick() => Application.OpenURL(recipe.websiteURL);
}

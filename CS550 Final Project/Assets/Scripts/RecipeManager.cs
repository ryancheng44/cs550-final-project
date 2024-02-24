using System.Collections.Generic;
using UnityEngine;

// This class manages the recipes in the game
public class RecipeManager : MonoBehaviour
{
    // Singleton instance of the RecipeManager
    public static RecipeManager instance;

    // Array of all recipes included in the program
    [SerializeField] private Recipe[] recipes;

    // Set of ingredients that the user currently has
    public HashSet<Ingredient> userIngredients = new HashSet<Ingredient>();

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // If there's no instance of RecipeManager yet, this becomes the instance
        if (instance == null)
            instance = this;
        else
            // If an instance already exists, destroy this object
            Destroy(gameObject);
    }

    // This method returns a list of recipes that can be made with the user's ingredients
    public List<Recipe> ReturnAvailableRecipes(bool ingredientSpotlightMode)
    {
        // List to store available recipes
        List<Recipe> availableRecipes = new List<Recipe>();

        // Loop through all recipes
        foreach (Recipe recipe in recipes)
        {
            // Create a set of ingredients for the current recipe
            HashSet<Ingredient> recipeIngredients = new HashSet<Ingredient>(recipe.ingredients);
            
            // If in ingredientSpotlightMode, check if userIngredients is a subset of recipeIngredients
            // If not in ingredientSpotlightMode, check if recipeIngredients is a subset of userIngredients
            // If the condition is true, add the recipe to the list of available recipes
            if (ingredientSpotlightMode ? userIngredients.IsSubsetOf(recipeIngredients) : recipeIngredients.IsSubsetOf(userIngredients))
                availableRecipes.Add(recipe);
        }

        // Return the list of available recipes
        return availableRecipes;
    }
}

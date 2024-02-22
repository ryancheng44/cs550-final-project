using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public static RecipeManager instance;

    [SerializeField] private Recipe[] recipes;
    [HideInInspector] public HashSet<Ingredient> userIngredients = new HashSet<Ingredient>();
    [HideInInspector] public List<Recipe> availableRecipes = new List<Recipe>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void DetermineAvailableRecipes(bool isSwapped)
    {
        availableRecipes.Clear();

        foreach (Recipe recipe in recipes)
        {
            HashSet<Ingredient> recipeIngredients = new HashSet<Ingredient>(recipe.ingredients);
            
            if (isSwapped ? userIngredients.IsSubsetOf(recipeIngredients) : recipeIngredients.IsSubsetOf(userIngredients))
                availableRecipes.Add(recipe);
        }
    }
}

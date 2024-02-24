using System; // Standard system namespace
using System.Collections.Generic; // Namespace for collections like lists
using UnityEngine; // Base Unity namespace for all Engine related features
using UnityEngine.UI; // Namespace for Unity's UI system
using TMPro; // Namespace for TextMeshPro, advanced text rendering

public class UIManager : MonoBehaviour
{
    // Serialized fields allow these variables to be set from the Unity Editor
    [SerializeField] private GameObject[] instructionsUI; // Array of GameObjects for instruction UI elements
    [SerializeField] private GameObject[] selectIngredientsUI; // Array of GameObjects for selecting ingredients UI
    [SerializeField] private GameObject[] availableRecipesUI; // Array of GameObjects for displaying available recipes UI

    [SerializeField] private GameObject ingredientButtonPrefab; // Prefab for ingredient buttons
    [SerializeField] private GameObject recipeButtonPrefab; // Prefab for recipe buttons
    [SerializeField] private GameObject noRecipesTextPrefab; // Prefab for displaying message when no recipes are available

    [SerializeField] private RectTransform selectIngredientsContent; // Container for ingredient buttons
    [SerializeField] private RectTransform availableRecipesContent; // Container for recipe buttons

    [SerializeField] private ScrollRect scrollRect; // ScrollRect component for scrolling through content

    [SerializeField] private TextMeshProUGUI availableRecipesText; // Text component for displaying currently active mode (ingredient spotlight or complete match)

    private float maxPreferredWidth; // Tracks the maximum width of ingredient buttons for layout purposes
    private bool selectIngredientsScreen = false; // Flag to track if the select ingredients screen is active
    private bool ingredientSpotlightMode = false; // Flag to track if the ingredient spotlight mode is active

    // Called when the user clicks the "Start" button
    public void StartProgram()
    {
        // Deactivates instruction UI elements
        foreach (GameObject gameObject in instructionsUI)
            gameObject.SetActive(false);

        SwapUI(); // Swaps between instruction UI and select ingredients UI

        // Dynamically creates ingredient buttons based on Ingredient enum
        foreach (Ingredient ingredient in Enum.GetValues(typeof(Ingredient)))
        {
            // Instantiates a new ingredient button and gets its TextMeshPro component
            TextMeshProUGUI instance = Instantiate(ingredientButtonPrefab, selectIngredientsContent).GetComponentInChildren<TextMeshProUGUI>();

            // Formats the ingredient's name for display
            string ingredientString = ingredient.ToString();
            for (int i = 1; i < ingredientString.Length; i++)
            {
                if (char.IsUpper(ingredientString[i])) // Inserts space before uppercase letters (not first char)
                {
                    ingredientString = ingredientString.Insert(i, " ");
                    i++; // Adjusts index due to added space
                }
            }
            instance.text = ingredientString; // Sets button text to formatted ingredient name

            // Adjusts maxPreferredWidth based on the width of the ingredient text
            if (instance.preferredWidth > maxPreferredWidth)
                maxPreferredWidth = instance.preferredWidth;

            // Sets the ingredient value for the button
            instance.GetComponentInParent<IngredientButton>().ingredient = ingredient;
        }

        // Adjusts cell size in the grid layout to accommodate the widest ingredient button
        GridLayoutGroup gridLayoutGroup = selectIngredientsContent.GetComponent<GridLayoutGroup>();
        Vector2 cellSize = gridLayoutGroup.cellSize;
        cellSize.x = maxPreferredWidth + 100f; // Adds padding to the width
        gridLayoutGroup.cellSize = cellSize;
    }

    // Called when player is done selecting ingredients, switches UI and sets up recipes
    public void Done()
    {
        SwapUI(); // Swaps UI elements
        SetRecipes(); // Sets up recipes based on selected ingredients
    }

    // Toggles ingredient spotlight mode and updates recipes
    public void Switch()
    {
        ClearRecipes(); // Clears existing recipe displays
        ingredientSpotlightMode = !ingredientSpotlightMode; // Toggles spotlight mode
        SetRecipes(); // Updates recipes based on new mode
    }

    // Allows editing of selected ingredients, clears recipes and swaps UI
    public void Edit()
    {
        ClearRecipes(); // Clears existing recipe displays
        SwapUI(); // Swaps UI elements
    }

    // Sets up the available recipes UI based on selected ingredients and mode
    private void SetRecipes()
    {
        // Sets text based on whether ingredient spotlight mode is active
        availableRecipesText.text = ingredientSpotlightMode ? "Ingredient Spotlight" : "Complete Match";

        // Gets available recipes based on mode and selected ingredients
        List<Recipe> availableRecipes = RecipeManager.instance.ReturnAvailableRecipes(ingredientSpotlightMode); // Retrieves recipes based on current mode and selected ingredients

        // Handles case when no recipes are available
        if (availableRecipes.Count == 0)
        {
            // Displays a message indicating no recipes can be found/made with selected ingredients
            TextMeshProUGUI instance = Instantiate(noRecipesTextPrefab, availableRecipesContent).GetComponent<TextMeshProUGUI>();
            instance.text = "Cannot " + (ingredientSpotlightMode ? "find" : "make") + " any recipes with the selected ingredients!";
            return; // Exits early as no recipes are available
        }

        // Creates UI elements for each available recipe
        foreach (Recipe recipe in availableRecipes)
        {
            // Instantiates a recipe button and sets its text to the recipe name
            TextMeshProUGUI instance = Instantiate(recipeButtonPrefab, availableRecipesContent).GetComponentInChildren<TextMeshProUGUI>();
            instance.text = recipe.name; // Sets button text to recipe name

            // Links the recipe data to the button for further actions
            instance.GetComponentInParent<RecipeButton>().recipe = recipe;
        }
    }

    // Clears the displayed recipes, preparing for a new list of recipes
    private void ClearRecipes()
    {
        // Destroys all child GameObjects of availableRecipesContent, effectively clearing the UI
        foreach (Transform child in availableRecipesContent)
            Destroy(child.gameObject);
    }

    // Toggles between select ingredients UI and available recipes UI
    private void SwapUI()
    {
        selectIngredientsScreen = !selectIngredientsScreen; // Toggles the state

        // Activates or deactivates UI elements based on the current state
        foreach (GameObject gameObject in selectIngredientsUI)
            gameObject.SetActive(selectIngredientsScreen);

        foreach (GameObject gameObject in availableRecipesUI)
            gameObject.SetActive(!selectIngredientsScreen);

        // Updates the scrollRect's content based on the current UI state
        scrollRect.content = selectIngredientsScreen ? selectIngredientsContent : availableRecipesContent;
    }
}

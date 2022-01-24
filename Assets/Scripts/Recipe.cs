using System;
using UnityEngine;

namespace MyGame {
    public enum IngredientUnit { Spoon, Cup, Bowl, Piece }

    // Custom serializable class
    [Serializable]
    public class Ingredient {
        public string name;
        public int amount = 1;
        public IngredientUnit unit;
    }

    [CreateAssetMenu(menuName = "Custom/Temp/Recipe")]
    public class Recipe : RecipeSO {
        public Ingredient potionResult;
        public Ingredient[] potionIngredients;
    }

    public abstract class RecipeSO : ScriptableObject {
        
    }
}
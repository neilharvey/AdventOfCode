import sys
import re
from functools import reduce

path = sys.argv[1] if len(sys.argv) > 1 else "example.txt"
with open(path) as f:
   input = f.read().splitlines()

foods = []

for line in input:
    parts = line.split(' (contains ')
    foods.append({ "ingredients":set(parts[0].split(' ')), "allergens":set(re.sub('[\),]', '', parts[1]).split(" "))})

ingredients = set([ingredient for food in foods for ingredient in food["ingredients"]])
allergens = set([allergen for food in foods for allergen in food["allergens"]])

cannot_contain_i = {ingredient:set() for ingredient in ingredients}
cannot_contain_a = {allergen:set() for allergen in allergens}

for food in foods:
    for allergen in food["allergens"]:
        ingredents_without_allergen = [i for i in ingredients if not i in food["ingredients"]]
        for ingredient in ingredents_without_allergen:
            cannot_contain_i[ingredient].add(allergen)
            cannot_contain_a[allergen].add(ingredient)

allergen_free_ingredients = set([i for (i,a) in cannot_contain_i.items() if len(a) == len(allergens)])
print(f"Part One: {len([ingredient for food in foods for ingredient in food['ingredients'] if ingredient in allergen_free_ingredients])}")

for food in foods:
    food["ingredients"] -= allergen_free_ingredients
    if len(food["allergens"]) == 1:
        allergen = next(iter(food["allergens"]))
        food["ingredients"] -= cannot_contain_a[allergen]

allergen_ingredients = {}

while len(allergen_ingredients) < len(allergens): 
    known_allergens = [(next(iter(food["allergens"])), next(iter(food["ingredients"]))) for food in foods if len(food["ingredients"]) == 1 and len(food["allergens"]) == 1]
    for (allergen,ingredient) in known_allergens:
        if not allergen in allergen_ingredients:
            allergen_ingredients[allergen] = ingredient

    for food in foods:
        food["allergens"] -= set([allergen for allergen in allergen_ingredients.keys()])
        food["ingredients"] -= set([ingredient for ingredient in allergen_ingredients.values()])

    foods = [food for food in foods if len(food["allergens"]) != 0]

canonical_dangerous_ingredient_list = ','.join([allergen_ingredients[allergen] for allergen in sorted(allergens)])
print(f"Part Two: {canonical_dangerous_ingredient_list}")
USE master
GO

--drop database if it exists
IF DB_ID('final_capstone') IS NOT NULL
BEGIN
	ALTER DATABASE final_capstone SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE final_capstone;
END

CREATE DATABASE final_capstone
GO

USE final_capstone
GO

--create tables
CREATE TABLE users (
	user_id int IDENTITY(1,1) NOT NULL,
	username varchar(50) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL
	CONSTRAINT PK_user PRIMARY KEY (user_id)
)

CREATE TABLE ingredients (
	ingredient_id int IDENTITY(1,1) NOT NULL,
	item_name varchar(50) NOT NULL,
	CONSTRAINT PK_ingredient PRIMARY KEY (ingredient_id)
)

CREATE TABLE recipes (
	recipe_id int IDENTITY(1,1) NOT NULL,
	user_id int NOT NULL,
	recipe_name varchar(50) NOT NULL,
	recipe_description text NOT NULL,
	instructions text NOT NULL,
	image_url varchar(2048),
	CONSTRAINT PK_recipe PRIMARY KEY (recipe_id),
	CONSTRAINT [FK_user_id] FOREIGN KEY (user_id) REFERENCES [users] (user_id),
)

CREATE TABLE recipes_ingredients (
	recipe_ingredient_id int IDENTITY(1,1) NOT NULL,
	recipe_id int NOT NULL,
	ingredient_id int NOT NULL,
	amount int NOT NUll,
	unit_of_measurement varchar(50) NOT NULL,
	CONSTRAINT PK_recipes_ingredients PRIMARY KEY (recipe_ingredient_id),
	CONSTRAINT [FK_recipe_id] FOREIGN KEY (recipe_id) REFERENCES [recipes] (recipe_id),
	CONSTRAINT [FK_ingredient_id] FOREIGN KEY (ingredient_id) REFERENCES [ingredients] (ingredient_id),
)

CREATE TABLE meal_plans (
	meal_plan_id int IDENTITY(1,1) NOT NULL,
	user_id int NOT NULL,
	meal_plan_name varchar(50) NOT NULL,
	start_date date NOT NULL,
	end_date date NOT NULL,
	CONSTRAINT PK_meal_plans PRIMARY KEY (meal_plan_id),
	CONSTRAINT [FK_MP_user_id] FOREIGN KEY (user_id) REFERENCES [users] (user_id),
)

CREATE TABLE meals (
	meal_id int IDENTITY(1,1) NOT NULL,
	meal_plan_id int NOT NULL,
	recipe_id int NOT NULL,
	day_of_the_week varchar(50) NOT NULL,
	meal_date date NOT NULL,
	meal_type varchar(50) NOT NULL,
	CONSTRAINT PK_meals PRIMARY KEY (meal_id),
	CONSTRAINT [FK_meal_plan_id] FOREIGN KEY (meal_plan_id) REFERENCES [meal_plans] (meal_plan_id),
	CONSTRAINT [FK_M_recipe_id] FOREIGN KEY (recipe_id) REFERENCES [recipes] (recipe_id)
)

--populate default data
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('Drew','8qZjG0/wdKOgeHG1/S8msdud50Y=', 'xuyjY2AMMi8=','user');

INSERT INTO ingredients(item_name) VALUES ('beef');

-- Chicken Fried Rice Ingredients
INSERT INTO ingredients(item_name) VALUES ('chicken breasts');
INSERT INTO ingredients(item_name) VALUES ('soy sauce');
INSERT INTO ingredients(item_name) VALUES ('corn starch');
INSERT INTO ingredients(item_name) VALUES ('oil');
INSERT INTO ingredients(item_name) VALUES ('sugar');
INSERT INTO ingredients(item_name) VALUES ('salt');
INSERT INTO ingredients(item_name) VALUES ('pepper');
INSERT INTO ingredients(item_name) VALUES ('rice');
INSERT INTO ingredients(item_name) VALUES ('onions');
INSERT INTO ingredients(item_name) VALUES ('scallions');

-- Salmon Burger Ingredients
INSERT INTO ingredients(item_name) VALUES ('salmon');
INSERT INTO ingredients(item_name) VALUES ('panko');
INSERT INTO ingredients(item_name) VALUES ('olive oil');
INSERT INTO ingredients(item_name) VALUES ('mustard');
INSERT INTO ingredients(item_name) VALUES ('paprika');
INSERT INTO ingredients(item_name) VALUES ('cayenne');
INSERT INTO ingredients(item_name) VALUES ('garlic powder');
INSERT INTO ingredients(item_name) VALUES ('onion powder');
INSERT INTO ingredients(item_name) VALUES ('buns');
INSERT INTO ingredients(item_name) VALUES ('mayo');

--Wonton Soup Ingredients
INSERT INTO ingredients(item_name) VALUES ('ground pork');
INSERT INTO ingredients(item_name) VALUES ('wine');
INSERT INTO ingredients(item_name) VALUES ('chicken stock');
INSERT INTO ingredients(item_name) VALUES ('wonton wrappers');
INSERT INTO ingredients(item_name) VALUES ('bok choy');

--Salt and Pepper chicken ingredients
INSERT INTO ingredients(item_name) VALUES ('chicken thighs');
INSERT INTO ingredients(item_name) VALUES ('MSG');
INSERT INTO ingredients(item_name) VALUES ('jalapenos');
INSERT INTO ingredients(item_name) VALUES ('shallots');
INSERT INTO ingredients(item_name) VALUES ('garlic');

--Snap Pea Salad ingredients
INSERT INTO ingredients(item_name) VALUES ('snap peas');
INSERT INTO ingredients(item_name) VALUES ('vinegar');
INSERT INTO ingredients(item_name) VALUES ('honey');
INSERT INTO ingredients(item_name) VALUES ('almonds');
INSERT INTO ingredients(item_name) VALUES ('sesame seeds');

--Scrambled Eggs with Chiles ingredients
INSERT INTO ingredients(item_name) VALUES ('chiles');
INSERT INTO ingredients(item_name) VALUES ('sesame oil');
INSERT INTO ingredients(item_name) VALUES ('eggs');

--Avocado Smoothie with Boba
INSERT INTO ingredients(item_name) VALUES ('tapioca pearls');
INSERT INTO ingredients(item_name) VALUES ('ice cubes');
INSERT INTO ingredients(item_name) VALUES ('avocados');
INSERT INTO ingredients(item_name) VALUES ('basil leaves');
INSERT INTO ingredients(item_name) VALUES ('limes');

--Garlic Shrimp ingredients
INSERT INTO ingredients(item_name) VALUES ('shrimp');
INSERT INTO ingredients(item_name) VALUES ('tomato paste');
INSERT INTO ingredients(item_name) VALUES ('ketchup');
INSERT INTO ingredients(item_name) VALUES ('ginger');
INSERT INTO ingredients(item_name) VALUES ('cilantro');

--Thai Basil Pork Belly ingredients
INSERT INTO ingredients(item_name) VALUES ('fish sauce');
INSERT INTO ingredients(item_name) VALUES ('oyster sauce');
INSERT INTO ingredients(item_name) VALUES ('pork belly');
INSERT INTO ingredients(item_name) VALUES ('bird chilles');
INSERT INTO ingredients(item_name) VALUES ('shrimp');

--Fluffy Cinnamon Rolls ingredients
INSERT INTO ingredients(item_name) VALUES ('heavy cream');
INSERT INTO ingredients(item_name) VALUES ('milk');
INSERT INTO ingredients(item_name) VALUES ('flour');
INSERT INTO ingredients(item_name) VALUES ('cinnamon');
INSERT INTO ingredients(item_name) VALUES ('butter');
INSERT INTO ingredients(item_name) VALUES ('brown sugar');








-- Chicken Fried Rice Recipe
INSERT INTO recipes(recipes.user_id, recipe_name, recipe_description, instructions, image_url) VALUES (3, 'Chicken Fried Rice', 'This classic chicken fried rice recipe is quick to make, no duss and definitely better than takeout.', 
'1. Combine the chicken and marinade ingredients. In a separate bowl, combine the hot water, sugar, light soy sauce, dark soy sauce, salt, and white pepper in a small bowl. Set aside.
2. Take your cooked rice and fluff it with a fork or with your hands (you can rinse your hands in cold water if the rice starts sticking to them). If you are using cold leftover rice, try to break up the clumps as best as possible.
3. Heat the wok over medium high heat and add 1 tablespoon of oil. Add the eggs and scramble them until just done. Remove from the wok immediately, and set aside.
4. Heat the wok until just smoking and spread another tablespoon oil around your wok. Sear the marinated chicken in one layer for 20 seconds. Stir-fry the chicken until about 80% done. Remove the chicken and set aside.
5. With the wok over medium high heat, add the final tablespoon of oil, and sauté the onions until translucent. Add the rice, and use your metal spatula to flatten out and break up any large clumps. If the rice is cold from the refrigerator, continue stir-frying until the rice is warmed up, which will take about 5 minutes. Sprinkling just a little water on large clumps of rice will help break them up more easily. If the rice was made fresh, cooking time will be faster. Just make sure that the rice is not too wet.
6. Once the rice is warmed (very important or the sauce will not mix as well and the color of the rice will not be as uniform), add the sauce mixture and mix with a scooping motion until the rice is evenly coated with sauce. Break up any remaining clumps of rice with the spatula. The rice should be hot by this time. Now add the cooked chicken, along with any juices from the bowl. Stir-fry for 1 minute.
7. Add the eggs, bean sprouts, and scallions, and continue stir-frying the rice for another 30 seconds. Then gather all of the rice into the middle of the wok to let the sides of the wok heat up.
8. After about 20 seconds, spread the Shaoxing wine around the perimeter of the wok and stir-fry for another 20 seconds. This step gives you a little of that extra "wok hei" that you taste when you get fried rice from a good Chinese restaurant. Serve!', 'https://images.pexels.com/photos/7758253/pexels-photo-7758253.jpeg');

INSERT INTO recipes_ingredients(recipes_ingredients.recipe_id, recipes_ingredients.ingredient_id, amount, unit_of_measurement) VALUES (1, 2, 1, 'lb')
INSERT INTO recipes_ingredients(recipes_ingredients.recipe_id, recipes_ingredients.ingredient_id, amount, unit_of_measurement) VALUES (1, 3, 1, 'oz')
INSERT INTO recipes_ingredients(recipes_ingredients.recipe_id, recipes_ingredients.ingredient_id, amount, unit_of_measurement) VALUES (1, 4, 1, 'gs')
INSERT INTO recipes_ingredients(recipes_ingredients.recipe_id, recipes_ingredients.ingredient_id, amount, unit_of_measurement) VALUES (1, 5, 2, 'oz')
INSERT INTO recipes_ingredients(recipes_ingredients.recipe_id, recipes_ingredients.ingredient_id, amount, unit_of_measurement) VALUES (1, 6, 3, 'tbs')
INSERT INTO recipes_ingredients(recipes_ingredients.recipe_id, recipes_ingredients.ingredient_id, amount, unit_of_measurement) VALUES (1, 7, 6, 'g')
INSERT INTO recipes_ingredients(recipes_ingredients.recipe_id, recipes_ingredients.ingredient_id, amount, unit_of_measurement) VALUES (1, 8, 3, 'tbs')
INSERT INTO recipes_ingredients(recipes_ingredients.recipe_id, recipes_ingredients.ingredient_id, amount, unit_of_measurement) VALUES (1, 9, 1, 'cup')
INSERT INTO recipes_ingredients(recipes_ingredients.recipe_id, recipes_ingredients.ingredient_id, amount, unit_of_measurement) VALUES (1, 10, 1, 'cup')
INSERT INTO recipes_ingredients(recipes_ingredients.recipe_id, recipes_ingredients.ingredient_id, amount, unit_of_measurement) VALUES (1, 11, 3, 'oz')

-- Salmon Burger Recipe
INSERT INTO recipes(recipes.user_id, recipe_name, recipe_description, instructions, image_url) VALUES (3, 'Salmon Burger', 'These salmon burgers are packed full of flavor by some choice seasoning. Who needs beef?', 
'1. First prepare the salmon patties. Take a 1-pound salmon fillet, and cut it into large chunks. If it has skin, remove it.
2. Divide into two portions. Transfer to a mixing bowl, and add the panko, egg, 2 tablespoons of olive oil, dijon mustard, and tarragon. Form into 3 patties, ½-inch thick.
3. In a separate small bowl, make the seasoning. Combine the salt, black pepper, paprika, cayenne, garlic powder, and onion powder. Sprinkle on both sides of each patty. 
4. Next, you’re ready to cook your salmon patties. Heat a cast-iron skillet over high heat with 2 tablespoons of oil. When it’s really hot, carefully place the salmon patties in the pan and cook for 2-3 minutes. When golden brown, flip the patties, and cook for another 2-3 minutes on the other side.
5. While the salmon patties are cooking, toss your burger buns into a toaster and butter them in preparation for your burger building.
6. Assemble as desired.', 'https://thewoksoflife.com/wp-content/uploads/2016/06/salmon-burgers-17.jpg');

--Wonton Soup Recipe
INSERT INTO recipes(recipes.user_id, recipe_name, recipe_description, instructions, image_url) VALUES (3, 'Wonton Soup', 'This simple wonton soup recipe is so delicious and easy to follow.', 
'1. Start by thoroughly washing the vegetables. Bring a large pot of water to a boil and blanch the vegetables just until wilted, about 60-90 seconds. Drain and rinse in cold water.
2. Grab a good clump of veg and carefully squeeze out as much water as you can. Very finely chop the vegetables
3. In a medium bowl, add the finely chopped vegetables, ground pork, 2 1/2 tablespoons sesame oil, pinch of white pepper, 1 tablespoon soy sauce, 1/2 teaspoon salt, and 1-2 tablespoons Shaoxing wine.
4. Fill a small bowl with water. Grab a wrapper and use your finger to moisten the edges of the wrapper. Add a little over a teaspoon of filling to the middle. Fold the wrapper in half and press the two sides together so you get a firm seal.
5. Hold the bottom two corners of the little rectangle you just made (the side where the filling is) and bring the two corners together. You can use a bit of water to make sure they stick.
6. Add the filling, and freeze for 1 hour.
7. Pour the hot chicken brother over frozen dumplings, and enjoy!', 'https://thewoksoflife.com/wp-content/uploads/2013/12/wonton-soup-recipe-8.jpg');

--Snap Pea Salad
INSERT INTO recipes(recipes.user_id, recipe_name, recipe_description, instructions, image_url) VALUES (3, 'Snap Pea Salad', 'This snap pea salad is easy, tasty, refreshing, and versatile enough to serve with anything!', 
'1. First, trim the snap peas of the stems and strings, and rinse them of any dirt or debris.
2. Toast the sesame seeds in a dry pan over medium heat until they’re 1-2 shades darker and fragrant. Remove the sesame seeds, and add the coriander seeds to the pan. Toast until fragrant. Grind the coriander seeds into a coarse powder using a mortar and pestle or spice grinder.
3. Slice the snap peas lengthwise into thin strips. Add them to a mixing bowl, along with the sesame seeds, ground coriander, rice vinegar, neutral oil, honey, sesame oil, salt, garlic, and almonds. Toss until everything is well-combined.
4. Taste and adjust seasoning to your liking, if needed. Serve!', 'https://thewoksoflife.com/wp-content/uploads/2023/07/snap-pea-salad-9-650x869.jpg');

--Scrambled Eggs with Chiles
INSERT INTO recipes(recipes.user_id, recipe_name, recipe_description, instructions, image_url) VALUES (3, 'Scrambled Eggs with Chiles', 'Make these fluffy Chinese scrambled eggs with salted chilies for breakfast, lunch, or dinner!', 
'1. In a medium bowl, add the eggs, sesame oil, and white pepper. Beat with chopsticks, a fork, or small whisk for about 1 minute, or until you see some frothy bubbles on the surface of the egg. Mix the cornstarch into a slurry with 1 tablespoon of water, and beat into the eggs.
2. Heat a wok over high heat, or a nonstick skillet over medium-high heat. When it’s really hot, add the oil, followed by the garlic chives. Cook until they’re wilted but still bright green
3. When the wok is steaming again pour in the eggs.
4. When the egg is just set (i.e., there may still be small pockets of runny bits), turn off the heat and plate immediately. The eggs will cook in their residual heat on the way to the dinner table.', 'https://thewoksoflife.com/wp-content/uploads/2022/11/fluffy-chinese-scrambled-eggs-chives-duo-jiao-8-650x883.jpg');

--Avocado Smoothie with Boba
INSERT INTO recipes(recipes.user_id, recipe_name, recipe_description, instructions, image_url) VALUES (3, 'Avocado Smoothie with Boba', 'Like most other healthy and flavor-loving humans, we have jumped on the bandwagon of enjoying fresh juices and green smoothies. Avocado smoothies that is.', 
'1. Boil a small pot of water. Cook the tapioca pearls per package instructions. When they’re done, drain and transfer to a small bowl with a small amount of room temperature water so they don’t harden and dry out.
2. Next, prepare your smoothie. Pile the rest of the ingredients into your blender. Blend until smooth.
3. In a tall glass, add a ¼ cup of tapioca pearls and pour your avocado smoothie over the top. Top with some additional lime zest if desired, add a straw, and enjoy!', 'https://thewoksoflife.com/wp-content/uploads/2016/05/avocado-smoothie-10.jpg');

--Chili Garlic Shrimp
INSERT INTO recipes(recipes.user_id, recipe_name, recipe_description, instructions, image_url) VALUES (1, 'Chili Garlic Shrimp', 'Chili garlic shrimp is a spicy treat and quick recipe to make—just chop some aromatics, sear jumbo tiger shrimp, and combine with the sauce!', 
'1. Rinse the shrimp under running water and pat dry with a paper towel. You can also slice the shrimp in the back to make them more plump after cooking.
2. Make the sauce by mixing the tomato paste, ketchup, chili garlic sauce, sugar, salt, white pepper and sesame oil in a medium bowl. 
3. Heat the oil in your wok until smoking hot. Fry the shrimp on both sides for 30 seconds each side. The shrimp should be 80% cooked. Turn off the heat, remove the shrimp from the wok, and set aside on a plate.
4. Turn the heat to medium-low, and leave the remainder of the oil in the wok.
5. Add the ginger and infuse for 15 seconds and add the garlic and shallot. Once caramelized (about 1 minute), add the sauce mixture and continue to stir and fry for another minute until incorporated. Add the chopped cilantro stems and then the Shaoxing wine.
6. Increase the heat to medium-high, and add the water to thin the sauce. Bring everything to a simmer.
7. Once the sauce is simmering, add the shrimp and any juices that may have collected on the plate. Toss the shrimp until they are completely coated in sauce. Serve, topped with chopped cilantro leaves.', 'https://thewoksoflife.com/wp-content/uploads/2022/01/chili-garlic-shrimp-17-650x942.jpg');

--Thai Basil Pork Belly
INSERT INTO recipes(recipes.user_id, recipe_name, recipe_description, instructions, image_url) VALUES (1, 'Thai Basil Pork Belly', 'Thai basil pork belly is a simple, bang-for-your-buck restaurant quality meal, requiring just 10 ingredients and 15 minutes.', 
'1. In a wok over medium heat, add the oil, garlic, and chilies. Cook for 1 minute. Crank up the heat to high, and add the pork belly. Stir-fry until caramelized and a little crisp, 2 minutes.
2. Add the sugar, fish sauce, oyster sauce, and soy sauces. Stir-fry for 1 minute, and add the basil. Stir-fry until the basil is wilted. Serve immediately.', 'https://thewoksoflife.com/wp-content/uploads/2020/06/thai-basil-pork-belly-6.jpg');

--Fluffy Cinnamon Rolls
INSERT INTO recipes(recipes.user_id, recipe_name, recipe_description, instructions, image_url) VALUES (1, 'Fluffy Cinnamon Rolls', 'These fluffy cinnamon rolls, made with our famous milk bread recipe, are tender, soft, and never tough or quick to go stale!', 
'1. In the bowl of a stand mixer fitted with a dough hook attachment, add these ingredients in the following order: room temperature heavy cream, milk, and egg, followed by the sugar, cake flour, bread flour, active dry yeast, and salt. There is no need to activate the yeast beforehand.
2. Turn on the mixer to the lowest setting, and let it go for 15 minutes, occasionally stopping the mixer to push the dough together. If you’re in a humid climate and the dough is too sticky, feel free to add a little more flour 1 tablespoon at a time until it comes together. It should be sticking to the bottom of the bowl, but not the sides. If you don’t have a mixer and would like to knead by hand, extend the kneading time by 5-10 minutes.
3. Cover the bowl with a damp towel and place in a warm spot for 1-2 hours, or until it has doubled in size.
4. In the meantime, grease two baking vessels on all sides with butter. We fit 12 rolls in a large oval casserole dish, and 4 into a smaller overflow casserole dish. You can also use two 9-inch cake pans. Keep in mind that these expand quite a bit. You want to ensure they have enough room to expand, but not so much room that they won’t end up hugging each other in the pan once baked.
5. Next, mix the cinnamon sugar filling. In a medium bowl, combine the brown sugar, cinnamon, butter, and salt until it’s a brown paste. If you need to, microwave the butter for 15-20 seconds to make it easier to stir.
6. After the dough has doubled in size, put it back in the mixer, and stir for another 5 minutes to get rid of any air bubbles.
7. Roll into desired shape.
8. Arrange the buns in the buttered baking pans with about ¾-inch between each bun. Proof for another 30-40 minutes.
9. 15 minutes into the last round of proofing, arrange a rack in the middle of the oven, and preheat to 350°F/175°C. After proofing the buns, bake for 20-25 minutes. Ours baked in 25 minutes. If you know your oven runs hot, proceed accordingly. The center buns will look a little pale relative to the ones on the edges, but don’t be tempted to bake them longer!
10. While the buns are baking, make the cream cheese icing. Whip the cream cheese and butter until fluffy and pale yellow. Add the vanilla extract and powdered sugar. For super thick icing, omit the milk. For a thinner, pourable consistency, add the milk.
11. When the buns are done baking, remove from the oven. Drizzle icing evenly over the buns as desired. Serve warm!', 'https://thewoksoflife.com/wp-content/uploads/2021/01/fluffy-cinnamon-rolls-18-650x900.jpg');
GO

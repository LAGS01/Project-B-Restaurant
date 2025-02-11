namespace Restaurant
{
    public class menuDis{
        public static string[] months = new string[] {"January", "February", "March", "April", "May", "June", "July", "August", "Septempber", "October", "November", "December"};
        public static int currentMonth = DateTime.Now.Month;
        public static string Month = months[currentMonth-1];

        public void menuDisplay(int menuNumber){
            if (menuNumber == 0)
            {
                //regular menu
                List<Dish> dishes = Dish.LoadDishesFromJson(Month); 
                //using string menu we add the id, name and price of every item in the list dishes to it so we can Console.WriteLine all this information.
                string menu = "";
                foreach (Dish dish in dishes)
                {
                    menu += $"{dish.ID,-3} {dish.Name,-30} ${dish.Price.ToString("F"),4}\n";
                }
                Console.WriteLine("Our Current Menu:");
                Console.WriteLine("========{food}========");
                Console.WriteLine(menu);
                Console.WriteLine("For information on dishes/drinks press 1 and then input the number assosiated with it.");
                Console.WriteLine("To filter (for vegan/glutenfree/vegetarian/sorting options) press 9");
                if (AdminMainMenu.adminLoggedIn && !UserLogin.userLoggedIn){
                    Console.WriteLine("Input 2 to Add or delete a dish");
                    Console.WriteLine("Input 8 to change the month for the menu");
                    Console.WriteLine("Input 7 to change something about a dish");
                }
                Console.WriteLine("Press Q to go back to the main menu");
            }

            if (menuNumber == 1){
                //vegan menu
                List<Dish> dishes = Dish.LoadDishesFromJson(Month); 
                //using string menu we add the id, name and price of every item in the list dishes to it so we can Console.WriteLine all this information.
                string menu = "";
                foreach (Dish dish in dishes){
                    if (dish.isVegan){
                        menu += $"{dish.ID,-3} {dish.Name,-30} ${dish.Price.ToString("F"),4}\n";
                    }
                }
                Console.WriteLine("\nOur vegan Menu:");
                Console.WriteLine("========{food}========");
                Console.WriteLine(menu);
                Console.WriteLine("For information on dishes/drinks press 1 and then input the number assosiated with it.");
                Console.WriteLine("Press escape to go back to the current menu");
            }

            if (menuNumber == 2){
                //gluten free menu
                Console.Clear();
                List<Dish> dishes = Dish.LoadDishesFromJson(Month); 
                string menu = "";
                foreach (Dish dish in dishes){
                    if (dish.isGlutenFree){
                        menu += $"{dish.ID,-3} {dish.Name,-30} ${dish.Price.ToString("F"),4}\n";
                    }
                }
                Console.WriteLine("\nOur Gluten free Menu:");
                Console.WriteLine("========{food}========");
                Console.WriteLine(menu);
                Console.WriteLine("For information on dishes/drinks press 1 and then input the number assosiated with it.");
                Console.WriteLine("press escape to go back to the current menu");
            }

            if (menuNumber == 3){
                //vegetarion menu
                Console.Clear();
                List<Dish> dishes = Dish.LoadDishesFromJson(Month); 
                string menu = "";
                foreach (Dish dish in dishes){
                    if (!dish.hasMeat && !dish.hasFish && !dish.hasShellFish){
                        menu += $"{dish.ID,-3} {dish.Name,-30} ${dish.Price.ToString("F"),4}\n";
                    }
                }
                Console.WriteLine("\nOur vegatarian Menu:");
                Console.WriteLine("========{food}========");
                Console.WriteLine(menu);
                Console.WriteLine("For information on dishes/drinks press 1 and then input the number assosiated with it.");
                Console.WriteLine("press escape to go back to the current menu");
            }

            //sorted by price
            if (menuNumber == 4){
                List<Dish> dishes = Dish.LoadDishesFromJson(Month); 
                string menu = "";
                foreach (Dish dish in menuDis.SortByPrice(dishes))
                {
                    menu += $"{dish.ID,-3} {dish.Name,-30} ${dish.Price.ToString("F"),4}\n";
                }
                Console.Clear();
                Console.WriteLine("Our Current Menu:");
                Console.WriteLine("========{food}========");
                Console.WriteLine(menu);
                Console.WriteLine("For information on dishes/drinks press 1 and then input the number assosiated with it.");
                Console.WriteLine("Press escape to go back to the current menu");
            }
        }

        //this is the methode that allows the admin to change the month in the json
        public static void changeMonth(string month){
            if (!(months.Contains(month)))
            {
                //this loop keeps going till there is a valid input
                do
                {
                    Console.Clear();
                    Console.WriteLine("Input the month");
                    Console.WriteLine("This isnt a valid input, please input a month");
                    month = Console.ReadLine();
                }
                while(!(months.Contains(month)));
            }
            if (month == Month){
                Console.WriteLine("This is already the month we are displaying");
                Console.Write("Press escape to go back to the menu");
                return;
            }
            Console.WriteLine($"You changed the menu to the month: {month}");
            Console.WriteLine("Press escape to go back to the menu");
            Month = month;
        }

        //this methode sorts the list by price
        public static List<Dish> SortByPrice(List<Dish> dishes)
        {
            ConsoleKeyInfo input;
            Console.Clear();
            Console.WriteLine("Pick how you want to sort to Prices");
            Console.WriteLine("1. From lowest to highest");
            Console.WriteLine("2. From highest to lowest");
            input =  Console.ReadKey(true);
            do
            {
                if (input.Key == ConsoleKey.D1)
                {
                    List<Dish> sortedPrice = dishes.OrderBy(d => d.Price).ToList();
                    return sortedPrice;
                }
                if (input.Key == ConsoleKey.D2)
                {
                    List<Dish> sortedDishes = dishes.OrderByDescending(d => d.Price).ToList();
                    return sortedDishes;
                }
                input = Console.ReadKey(true);
            }
            while (input.Key != ConsoleKey.D1 && input.Key != ConsoleKey.D2);
            if (input.Key == ConsoleKey.D1)
            {
                List<Dish> sortedPrice = dishes.OrderBy(d => d.Price).ToList();
                return sortedPrice;
            }
            if (input.Key == ConsoleKey.D2)
            {
                List<Dish> sortedDishes = dishes.OrderByDescending(d => d.Price).ToList();
                return sortedDishes;
            }
            return null;
        }
    }
}
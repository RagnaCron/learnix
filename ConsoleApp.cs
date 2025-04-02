using Newtonsoft.Json;

namespace LearnIx
{
    internal class ConsoleApp
    {
        private static List<Card> _loadedCards = [];

        public void Run()
        {
            Console.WriteLine("Welcome to LearnIx!");
            Console.WriteLine("-------------------");
            Help();
            while (true)
            {
                Console.Write("Enter a command: ");
                var command = Console.ReadLine().ToLower();
                switch (command)
                {
                    case "add":
                        AddCard();
                        break;
                    case "load":
                        LoadCards();
                        break;
                    case "edit":
                        ModifyCard();
                        break;
                    case "delete":
                        DeleteCard();
                        break;
                    case "show":
                        PrintCards();
                        break;
                    case "practice":
                        break;
                    case "repeat":
                        break;
                    case "exit":
                        ExitSystem();
                        break;
                    default:
                        Help();
                        break;
                }
            }
        }

        private void AddCard()
        {
            var card = new Card();
            EditCard(card);
            _loadedCards.Add(card);
        }

        private void LoadCards()
        {
            Console.WriteLine("Enter the path to the file:");
            _loadedCards = ReadFromJson(Console.ReadLine());
        }

        private static List<Card> ReadFromJson(string path)
        {
            return JsonConvert.DeserializeObject<List<Card>>(path);
        }

        private void ModifyCard()
        {
            PrintCards();
            Console.WriteLine("Enter the card index:");
            var input = Console.ReadLine();
            try
            {
                var cardIndex = Convert.ToInt32(input);
                EditCard(_loadedCards[cardIndex]);
            }
            catch (Exception e)
            {
                Console.WriteLine("Enter a valid card index");
            }
        }

        private void EditCard(Card card)
        {
            card.FrontSide = GetQuestionText();
            card.BackSide = GetSolutionText();
        }

        private static string GetSolutionText()
        {
            Console.WriteLine("Enter the solution:");
            return Console.ReadLine();
        }

        private static string GetQuestionText()
        {
            Console.WriteLine("Enter the question:");
            return Console.ReadLine();
        }

        private void PrintCards()
        {
            if (_loadedCards.Count == 0)
            {
                Console.WriteLine("No cards. Please add a card.");
                return;
            }

            var x = 0;
            _loadedCards.ForEach(c =>
            {
                Console.WriteLine("Card index {0}", x);
                Console.WriteLine("Question {0}", c.FrontSide);
                Console.WriteLine("Answer {0}", c.BackSide);
                x++;
            });
        }

        private void DeleteCard()
        {
            PrintCards();
            Console.WriteLine("Enter the card index:");
            var input = Console.ReadLine();
            try
            {
                var cardIndex = Convert.ToInt32(input);
                _loadedCards.RemoveAt(cardIndex);
            }
            catch (Exception e)
            {
                Console.WriteLine("Enter a valid card index");
            }
        }

        private static void Practice(List<Card> findAll)
        {
        }

        public static void RepeatWrong()
        {
            Practice(_loadedCards.FindAll(c => c.wasWrong));
        }

        private static void ExitSystem()
        {
            Environment.Exit(0);
        }

        private static void Help()
        {
            Console.WriteLine("Enter [help] for this help message");
            Console.WriteLine("Enter [add] for adding a card");
            Console.WriteLine("Enter [load] to load cards from a file");
            Console.WriteLine("Enter [edit] to edit a card");
            Console.WriteLine("Enter [delete] to delete a card");
            Console.WriteLine("Enter [show] show all cards");
            Console.WriteLine("Enter [practice] to practice");
            Console.WriteLine("Enter [repeat] to repeat wrong cards");
            Console.WriteLine("Enter [exit] to exit the program");
            Console.WriteLine();
        }
    }
}
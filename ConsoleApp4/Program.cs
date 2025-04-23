using System.Media;

namespace ConsoleApp4
{
    internal class Program
    {
        private static readonly Dictionary<string, string> responses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "how are you", "I'm just a bot, but I'm here to help you stay safe online!" },
            { "what's your purpose", "My purpose is to educate you about cybersecurity and help you avoid common online threats." },
            { "what can i ask you about", "You can ask me about password safety, phishing scams, safe browsing, and other cybersecurity topics." },
            { "password", "Always use strong, unique passwords and enable multi-factor authentication when possible." },
            { "phishing", "Phishing scams often come as emails or messages pretending to be someone you trust. Don’t click suspicious links!" },
            { "browsing", "Use updated browsers, avoid untrusted websites, and consider using an ad blocker for safer browsing." }
        };

        static void Main(string[] args)
        {


            // Play a voice greeting from a WAV file
            PlayVoiceGreeting("welcome.wav");
            // Display the ASCII art logo
            DisplayAsciiArtLogo();
            // Get the user's name
            string userName = GetUserName();
            // Display a welcome message to the user
            DisplayWelcomeMessage(userName);
            // Start the chat loop for user interaction
            ChatLoop(userName);
        }

        static void PlayVoiceGreeting(string filePath)
        {
            try
            {
                // Combine the current directory with the provided file path
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                Console.WriteLine($"[Debug] Looking for: {fullPath}");

                // Check if the file exists
                if (File.Exists(fullPath))
                {
                    using (SoundPlayer player = new SoundPlayer(fullPath))
                    {
                        player.PlaySync(); // Play the sound synchronously
                    }
                }
                else
                {
                    // File not found, display an error message
                    DisplayError($"Error: '{filePath}' was not found at specified location.");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during playback
                DisplayError($"Error playing audio: {ex.Message}");
            }
        }

        // Helper method to display error messages in red
        static void DisplayError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        private static void DisplayAsciiArtLogo()
        {
            string filePath = "ascii_logo.txt"; // Path to the ASCII art logo file
            try
            {
                // Check if the ASCII logo file exists
                if (File.Exists(filePath))
                {
                    Console.ForegroundColor = ConsoleColor.Cyan; // Set text color to cyan for the logo
                    string[] lines = File.ReadAllLines(filePath); // Read all lines from the file
                    foreach (string line in lines)
                    {
                        Console.WriteLine(line); // Print each line of the ASCII art
                    }
                    Console.ResetColor(); // Reset text color to default
                    Console.WriteLine(); // Print a new line for spacing
                }
                else
                {
                    // If the file is not found, display an error message
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"ASCII logo file '{filePath}' not found.");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur while reading the file
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error reading ASCII art: {ex.Message}");
                Console.ResetColor();
            }
        }


        private static string GetUserName()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Please enter your name: ");
            Console.ResetColor();
            string name = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(name))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Name cannot be empty. Please enter your name:");
                Console.ResetColor();
                name = Console.ReadLine();
            }
            return name.Trim();
        }

        private static void DisplayWelcomeMessage(string name)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("╔════════════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║  Welcome to the Cybersecurity Awareness Bot, {name,-30}║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();
        }

        private static void ChatLoop(string name)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("You can ask me questions about cybersecurity. Type 'exit' to quit.");
            Console.ResetColor();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{name}: ");
                Console.ResetColor();
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    TypingEffect("I didn't quite understand that. Could you rephrase?");
                    continue;
                }

                if (input.Trim().ToLower() == "exit")
                {
                    TypingEffect("Thank you for chatting! Stay safe online!");
                    break;
                }

                string response = GetResponse(input);
                TypingEffect(response);
            }
        }

        private static string GetResponse(string input)
        {
            input = input.ToLower();
            foreach (var pair in responses)
            {
                if (input.Contains(pair.Key.ToLower()))
                {
                    return pair.Value;
                }
            }
            return "I'm not sure how to respond to that. Try asking about passwords, phishing, or safe browsing.";
        }

        private static void TypingEffect(string message)
        {
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(30);
            }
            Console.WriteLine();
        }
    }
}


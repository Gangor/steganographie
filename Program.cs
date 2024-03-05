using McMaster.Extensions.CommandLineUtils;
using Steganography;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;


namespace Steganographie
{
    [Command("Steganographie", Description = "Stéganographie")]
    [Subcommand(typeof(Text), typeof(Image))]
    class Program
    {
        public static void Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        private int OnExecute(CommandLineApplication app, IConsole console)
        {
            console.WriteLine("You must specify at a subcommand.");
            app.ShowHelp();
            return 1;
        }

        [Command("Texte", Description = "Texte dans une image")]
        [Subcommand(typeof(Text.Read), typeof(Text.Write))]
        private class Text
        {
            private int OnExecute(CommandLineApplication app, IConsole console)
            {
                console.WriteLine("You must specify at a subcommand.");
                app.ShowHelp();
                return 1;
            }

            [Command("lecture", Description = "Lecture d'un message sur une image",
                    AllowArgumentSeparator = true,
                    UnrecognizedArgumentHandling = UnrecognizedArgumentHandling.StopParsingAndCollect)]
            class Read
            {
                [Required]
                [Option("--clef")]
                public string Clef { get; set; }

                [Required]
                [Option("--image")]
                public string Image { get; set; }

                private void OnExecute(IConsole console)
                {
                    if (!File.Exists(Image))
                    {
                        throw new Exception("L'emplacement vers le fichier d'image n'existe pas!");
                    }

                    if (!Utils.IsImage(Image))
                    {
                        throw new Exception("Le fichier spécifié n'est pas une image valide.");
                    }

                    try
                    {
                        var bitmap = new Bitmap(Image);
                        var decodedMessage = string.Empty;
                        var encodedMessage = SteganographyHelper.ExtractTextFromImage(bitmap);

                        try
                        {
                            decodedMessage = Crypto.DecryptStringAES(encodedMessage, Clef);
                        }
                        catch (Exception)
                        {
                            throw new Exception("Echec du déchiffrement du message!");
                        }

                        Console.WriteLine(decodedMessage);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Erreur : {ex.Message}");
                    }
                }
            }

            [Command("écriture", Description = "Ecriture d'un message sur une image",
                    AllowArgumentSeparator = true,
                    UnrecognizedArgumentHandling = UnrecognizedArgumentHandling.StopParsingAndCollect)]
            class Write
            {
                [Required]
                [Option("--clef")]
                public string Clef { get; set; }

                [Required]
                [Option("--image")]
                public string Image { get; set; }

                [Required]
                [Option("--output")]
                public string Output { get; set; }

                [Required]
                [Option("--message")]
                public string Message { get; set; }

                private void OnExecute(IConsole console)
                {
                    try
                    {
                        if (!File.Exists(Image))
                        {
                            throw new Exception("L'emplacement vers le fichier d'image n'existe pas!");
                        }

                        if (!Utils.IsImage(Image))
                        {
                            throw new Exception("Le fichier spécifié n'est pas une image valide.");
                        }

                        var bitmap = new Bitmap(Image);
                        var encodedMessage = string.Empty;

                        try
                        {
                            encodedMessage = Crypto.EncryptStringAES(Message, Clef);
                        }
                        catch (Exception)
                        {
                            throw new Exception("Echec du chiffrement du message!");
                        }

                        SteganographyHelper.HideTextInImage(bitmap, encodedMessage);

                        bitmap.Save(Output, ImageFormat.Png);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Erreur : {ex.Message}");
                    }
                }
            }
        }

        [Command("File", Description = "Intégrer un fichier dans une image")]
        [Subcommand(typeof(Image.Read), typeof(Image.Write))]
        private class Image
        {
            private int OnExecute(CommandLineApplication app, IConsole console)
            {
                console.WriteLine("You must specify at a subcommand.");
                app.ShowHelp();
                return 1;
            }

            [Command("lecture", Description = "Lecture d'un fichier dans une image",
                    AllowArgumentSeparator = true,
                    UnrecognizedArgumentHandling = UnrecognizedArgumentHandling.StopParsingAndCollect)]
            class Read
            {
                [Required]
                [Option("--clef")]
                public string Clef { get; set; }

                [Required]
                [Option("--image")]
                public string Image { get; set; }

                [Required]
                [Option("--output")]
                public string Output { get; set; }

                private void OnExecute(IConsole console)
                {
                    if (!File.Exists(Image))
                    {
                        throw new Exception("L'emplacement vers le fichier d'image n'existe pas!");
                    }

                    if (!Utils.IsImage(Image))
                    {
                        throw new Exception("Le fichier spécifié n'est pas une image valide.");
                    }

                    try
                    {
                        var bitmap = new Bitmap(Image);
                        var decodedImage = string.Empty;
                        var encodedImage = SteganographyHelper.ExtractTextFromImage(bitmap);

                        try
                        {
                            decodedImage = Crypto.DecryptStringAES(encodedImage, Clef);
                        }
                        catch (Exception)
                        {
                            throw new Exception("Echec du déchiffrement du message!");
                        }

                        var secret = Convert.FromBase64String(decodedImage);

                        File.WriteAllBytes(Output, secret);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Erreur : {ex.Message}");
                    }
                }
            }

            [Command("écriture", Description = "Ecriture d'un fichier dans une image",
                    AllowArgumentSeparator = true,
                    UnrecognizedArgumentHandling = UnrecognizedArgumentHandling.StopParsingAndCollect)]
            class Write
            {
                [Required]
                [Option("--clef")]
                public string Clef { get; set; }

                [Required]
                [Option("--image")]
                public string Image { get; set; }

                [Required]
                [Option("--secret-file")]
                public string SecretFile { get; set; }

                [Required]
                [Option("--output")]
                public string Output { get; set; }

                private void OnExecute(IConsole console)
                {
                    try
                    {
                        if (!File.Exists(Image))
                        {
                            throw new Exception("L'emplacement vers le fichier d'image de base n'existe pas!");
                        }

                        if (!Utils.IsImage(Image))
                        {
                            throw new Exception("Le fichier spécifié n'est pas une image de base valide.");
                        }

                        var bitmap = new Bitmap(Image);
                        var encodedMessage = string.Empty;

                        try
                        {
                            var file = File.ReadAllBytes(SecretFile);
                            var secret = Convert.ToBase64String(file);

                            encodedMessage = Crypto.EncryptStringAES(secret, Clef);
                        }
                        catch (Exception)
                        {
                            throw new Exception("Echec du chiffrement du message!");
                        }

                        SteganographyHelper.HideTextInImage(bitmap, encodedMessage);

                        bitmap.Save(Output, ImageFormat.Png);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Erreur : {ex.Message}");
                    }
                }
            }
        }
    }
}
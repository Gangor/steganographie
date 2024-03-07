using McMaster.Extensions.CommandLineUtils;
using Steganographie.Core;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace Steganographie
{
    partial class Program
    {
        private partial class Texte : SubCommand
        {
            partial class Lecture
            {
                [Required]
                [Option("--clef", Description = "Clef servant à chiffrer le contenue caché", ShowInHelpText = false)]
                public string Clef { get; set; }

                [Required]
                [FileExists]
                [Option("--image", Description = "Emplacement vers l'image de base qui servira à cacher du contenue", ShowInHelpText = false)]
                public string Image { get; set; }

                /// <summary>
                /// Processus exécuté lorsque la commande valide les prérequis
                /// </summary>
                /// <param name="console"></param>
                private void OnExecute(IConsole console)
                {
                    try
                    {
                        // On vérifie que le fichier est bien une image valide.
                        if (!Utils.IsImage(Image))
                        {
                            throw new Exception("Le fichier spécifié n'est pas une image valide.");
                        }

                        // On charge l'image et on récupére le contenue intégré dans l'image.

                        var bitmap = new Bitmap(Image);
                        var decodedMessage = string.Empty;
                        var encodedMessage = SteganographyHelper.extractText(bitmap);

                        // On déchiffre le contenue extrait de l'image et on affiche le texte dans la console
                        try
                        {
                            decodedMessage = Crypto.DecryptStringAES(encodedMessage, Clef);

                            console.WriteLine($"Message secret : {decodedMessage}");
                        }
                        catch (Exception)
                        {
                            throw new Exception("Echec du déchiffrement du message!");
                        }

                    }
                    catch (Exception ex)
                    {
                        console.Error.WriteLine($"Erreur : {ex.Message}");
                    }
                }
            }
        }
    }
}

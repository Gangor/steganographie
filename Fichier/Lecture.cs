using McMaster.Extensions.CommandLineUtils;
using Steganographie.Core;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace Steganographie
{
    public partial class Program
    {
        private partial class Fichier : SubCommand
        {
            partial class Lecture
            {
                [Required]
                [Option("--clef", Description = "Clef servant à chiffrer le contenue caché")]
                public string Clef { get; set; }

                [Required]
                [FileExists]
                [Option("--image", Description = "Emplacement vers l'image de base qui servira à cacher du contenue")]
                public string Image { get; set; }

                [Required]
                [Option("--output", Description = "Emplacement de l'image de sortie")]
                public string Output { get; set; }

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
                        var decodedImage = string.Empty;
                        var encodedImage = SteganographyHelper.extractText(bitmap);

                        // On déchiffre le contenue extrait de l'image
                        try
                        {
                            decodedImage = Crypto.DecryptStringAES(encodedImage, Clef);
                        }
                        catch (Exception)
                        {
                            throw new Exception("Echec du déchiffrement du message!");
                        }

                        // On convertie le base64 en tableau d'octet pour reformé le fichier
                        var secret = Convert.FromBase64String(decodedImage);

                        // On sauvegarde le fichier
                        File.WriteAllBytes(Output, secret);
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

using McMaster.Extensions.CommandLineUtils;
using Steganographie.Core;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Imaging;
using System.Drawing;

namespace Steganographie
{
    partial class Program
    {
        private partial class Fichier : SubCommand
        {
            partial class Ecriture
            {
                [Required]
                [Option("--clef", Description = "Clef servant à chiffrer le contenue caché")]
                public string Clef { get; set; }

                [Required]
                [FileExists]
                [Option("--image", Description = "Emplacement d'une image existante de base qui servira à cacher du contenue")]
                public string Image { get; set; }

                [Required]
                [FileExists]
                [Option("--secret-file", Description = "Emplacement vers le fichier à cacher dans l'image")]
                public string SecretFile { get; set; }

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
                            throw new Exception("Le fichier spécifié n'est pas une image de base valide.");
                        }

                        var bitmap = new Bitmap(Image);
                        var encodedMessage = string.Empty;

                        // On récupére le contenue du fichier sous la forme base64 et on chiffre
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

                        var textSize = encodedMessage.Length * 8;
                        var textSizeInKB = textSize / 1024;

                        // On vérifie que le contenue à caché peut rentrer dans l'image
                        if (textSizeInKB > SteganographyHelper.GetImageSizeInKB(bitmap))
                        {
                            throw new Exception($"L'image ne peut contenir un texte de taille plus importante que {SteganographyHelper.GetImageSizeInKB(bitmap)} KB");
                        }

                        // On intégre le texte dans l'image
                        SteganographyHelper.embedText(encodedMessage, bitmap);

                        // On sauvegarde l'image
                        bitmap.Save(Output, ImageFormat.Png);

                        console.WriteLine($"Image sauvegardé à l'emplacement : {Output}");
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

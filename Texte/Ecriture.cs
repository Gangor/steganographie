﻿using McMaster.Extensions.CommandLineUtils;
using Steganographie.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Imaging;
using System.Drawing;

namespace Steganographie
{
    partial class Program
    {
        private partial class Texte : SubCommand
        {
            partial class Ecriture
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

                [Required]
                [Option("--message", Description = "Message à cacher dans l'image")]
                public string Message { get; set; }

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

                        var bitmap = new Bitmap(Image);
                        var encodedMessage = string.Empty;

                        // On chiffre le message secret
                        try
                        {
                            encodedMessage = Crypto.EncryptStringAES(Message, Clef);
                        }
                        catch (Exception)
                        {
                            throw new Exception("Echec du chiffrement du message!");
                        }

                        // On intégre le texte dans l'image
                        // La méthode gére le cas de dépassement de capicité de stockage de l'image
                        SteganographyHelper.embedText(encodedMessage, bitmap);

                        // On sauvegarde l'image
                        bitmap.Save(Output, ImageFormat.Png);

                        Console.WriteLine($"Image sauvegardé à l'emplacement : {Output}");
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

using McMaster.Extensions.CommandLineUtils;
using Steganographie.Core;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;


namespace Steganographie
{
    /// <summary>
    /// Point d'entrée de l'application
    /// </summary>
    [Command]
    [Subcommand(typeof(Texte), typeof(Fichier))]
    partial class Program :SubCommand
    {
        public static void Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        /// <summary>
        /// Commandes de gestion d'intégration de fichier dans une image
        /// </summary>
        [Command("fichier", Description = "Intégrer un fichier dans une image")]
        [Subcommand(typeof(Lecture), typeof(Ecriture))]
        private partial class Fichier : SubCommand
        {
            /// <summary>
            /// Sous-commande de lecture de fichier dans une image.
            /// </summary>
            [Command("lecture", Description = "Lecture d'un fichier dans une image",
                    AllowArgumentSeparator = true,
                    UnrecognizedArgumentHandling = UnrecognizedArgumentHandling.StopParsingAndCollect)]
            partial class Lecture { }

            /// <summary>
            /// Sous-commande d'écriture de fichier dans une image.
            /// </summary>
            [Command("ecriture", Description = "Ecriture d'un fichier dans une image",
                    AllowArgumentSeparator = true,
                    UnrecognizedArgumentHandling = UnrecognizedArgumentHandling.StopParsingAndCollect)]
            partial class Ecriture { }
        }

        /// <summary>
        /// Commandes de gestion d'intégration de texte dans une image
        /// </summary>
        [Command("texte", Description = "Intégrer un texte dans une image")]
        [Subcommand(typeof(Lecture), typeof(Ecriture))]
        private partial class Texte : SubCommand
        {
            /// <summary>
            /// Sous-commande de lecture de texte dans une image.
            /// </summary>
            [Command("lecture", Description = "Lecture d'un message sur une image",
                    AllowArgumentSeparator = true,
                    UnrecognizedArgumentHandling = UnrecognizedArgumentHandling.StopParsingAndCollect)]
            partial class Lecture { }

            /// <summary>
            /// Sous-commande d'écriture de texte dans une image.
            /// </summary>
            [Command("ecriture", Description = "Ecriture d'un message sur une image",
                    AllowArgumentSeparator = true,
                    UnrecognizedArgumentHandling = UnrecognizedArgumentHandling.StopParsingAndCollect)]
            partial class Ecriture { }
        }
    }

    /// <summary>
    /// Classe parant servant à afficher l'aide
    /// </summary>
    public class SubCommand
    {
        public int OnExecute(CommandLineApplication app, IConsole console)
        {
            app.ShowHelp();
            return 1;
        }
    }
}
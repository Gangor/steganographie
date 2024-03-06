# Steganographie Tool
## Overview

This is a command-line tool for steganography, which allows you to hide and retrieve information within image files. The tool supports two main functionalities: embedding and extracting information. You can either hide text messages within images or embed entire files.
## Prerequisites
- .NET 8 SDK
## Installation 
1. Clone the repository:

```bash
git clone https://github.com/Gangor/steganographie
``` 
2. Navigate to the project directory:

```bash
cd steganographie
``` 
3. Build the project:

```bash
dotnet build
```
## Usage
### Text Embedding and Extraction
#### Embedding a Text Message:

```bash
.\Steganographie.exe texte ecriture --clef "Mon password" --image "C:\base_image.png" --output "C:\image_with_embed_text.png" --message "My secret message"
```

 
- `--clef`: Encryption password for securing the hidden message. 
- `--image`: Path to the input image. 
- `--output`: Path to save the output image. 
- `--message`: The text message to hide.
#### Extracting a Text Message:

```bash
.\Steganographie.exe texte lecture --clef "Mon password" --image "C:\image_with_embed_text.png"
```

 
- `--clef`: Encryption password for decoding the hidden message. 
- `--image`: Path to the input image.
### File Embedding and Extraction
#### Embedding a File:

```bash
.\Steganographie.exe fichier ecriture --clef "Mon password" --image "C:\base_image.png" --secret-file "C:\my_secret_image.png" --output "C:\image_with_embed_file.png"
```

 
- `--clef`: Encryption password for securing the hidden file. 
- `--image`: Path to the input image. 
- `--secret-file`: Path to the file to be hidden. 
- `--output`: Path to save the output image.
#### Extracting a File:

```bash
.\Steganographie.exe fichier lecture --clef "Mon password" --image "C:\image_with_embed_file.png" --output "D:\image_secret.png"
```

 
- `--clef`: Encryption password for decoding the hidden file. 
- `--image`: Path to the input image. 
- `--output`: Path to save the extracted file.
## License

This project is licensed under the MIT License - see the [LICENSE]()  file for details.

## Usage
### Text Embedding and Extraction
#### Embedding a Text Message:

```bash
.\hostinfo.exe texte ecriture `
--clef "Mon password" `
--image "C:\base_image.png" `
--output "C:\image_with_embed_text.png" `
--message "My secret message"
```

- `--clef`: Encryption password for securing the hidden message. 
- `--image`: Path to the input image. 
- `--output`: Path to save the output image. 
- `--message`: The text message to hide.

#### Extracting a Text Message:

```bash
.\hostinfo.exe texte lecture `
--clef "Mon password" `
--image "C:\image_with_embed_text.png"
```
 
- `--clef`: Encryption password for decoding the hidden message. 
- `--image`: Path to the input image.

### File Embedding and Extraction
#### Embedding a File:

```bash
.\hostinfo.exe fichier ecriture `
--clef "Mon password" `
--image "C:\base_image.png" `
--secret-file "C:\my_secret_image.png" `
--output "C:\image_with_embed_file.png"
```
 
- `--clef`: Encryption password for securing the hidden file. 
- `--image`: Path to the input image. 
- `--secret-file`: Path to the file to be hidden. 
- `--output`: Path to save the output image.

#### Extracting a File:

```bash
.\hostinfo.exe fichier lecture `
--clef "Mon password" `
--image "C:\image_with_embed_file.png" `
--output "D:\image_secret.png"
```

- `--clef`: Encryption password for decoding the hidden file. 
- `--image`: Path to the input image. 
- `--output`: Path to save the extracted file.
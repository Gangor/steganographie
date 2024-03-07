# Documentation

EPSI 2023-2024

Tristan MULLER, Louis CHAMBARD, Joel DE SAINT GILLES, Sofiane NASRI

## Abstract
As we work (on the subject) for an intelligence agency, the goal is to allow agents in the field to communicate safely between them. This application has been created for this purpose.

By using the principle of stenography (as asked in the subject), the software code can encode incoming and outcoming messages with a particular method.

This work will focus on studying steganography and its application for the needs intelligently.

## Use-case studying

Agents must be able to send :
* Text(s)
* Photo(s)

The messages are sent by email, but this software doesn't have to incorporate any sending method. Only the encoding/decoding process is really needed.

Its needing that an enemy cannot :
* Discover hidden message
* Know if any hidden message exist
* Prevent message to be sent
* Send false information

One of the most difficult features to develop, is to make the encoding undiscoverable by any steganalysis method. Hopefully, this feature has not been asked for this project. But it can be a future improvement.

## Existing algorithms
There are many existing methods to add a message into an image, or a text. But some of them are better depending on the context they are used in.

The first distinction we can make between all different kinds of algorithms is the type of media where the information will be stored during the process.


For text, there are many ways to hide a message in it. We can use the format, random letter sequences or grammars. It's less hidden than when we use images.

### Least Significant Bit
This method is the simplest method of digital image steganography. The method consists in replacing the least significant bits of the container image with the bits of the message. The logic of the method can be illustrated by the following picture :

![alt](https://planetcalc.com/users/2/1623914538.png)[^1]

The method has good capacity compared to other methods.


The disadvantages of the method include its weak robustness - the method is very sensitive to distortions of the steganography image.


This method requires many mathematical operations depending on which alternative has been chosen.

[^1]: https://planetcalc.com/9345/

### Redundant Pattern Encoding
There is not many articles about this technique, the only one I found explain this well like this :

> Redundant encoding is the use of more than one graphical or visual structure (e.g. color + position) to encode/represent one variable of data. For instance, in a 2D scatter plot, the x-axis values can be represented by both color+shape (e.g. red rectangles for series 1, blue circles for series 2 etc.). Actually, we can use a non-redundant code to represent these data (e.g. rectangles for series 1, circles for series 2). It is controversial whether redundant codes are effective or not. Some visualization theorists argue that certain redundant codes can increase accuracy and enhance perception.[^2]

It's very similar to the Least Significant Bit on the way that can use bits to encode some information.
However, this method has the advantage of using multiple techniques and that implies more complexity and so more security.

[^2]: https://infovis-wiki.net/wiki/Patterns:Redundant_Encoding

#### Masking and filtering
> They hide info in a way similar to watermarks on actual paper and are sometimes used as digital watermarks. Masking techniques embed information in significant areas so that the hidden message is more integral to the cover image than just hiding it in the "noise" level. Compared to LSB, masking is more robust and masked images pass cropping, compression and some image processing.[^3]

This technique seems to be easy to detect, but it can also be very little. It can be interesting to make a comparison between the memory amount used by each technique to know precisely in terms of used image type in the context.

[^3]: https://www.ukessays.com/essays/computer-science/the-types-and-techniques-of-steganography-computer-science-essay.php

## AES Algorithm
This algorithm has been created in 1998 and now it's known to be the more sure and performant encryption algorithm and hugely used for cryptography.[^4]
The algorithm consist of four steps :
* SubBytes : a non-linear substitution where each byte  is replaced with another according to a lookup table.
* ShiftRows : a transposition where the last three rows of the state are shifted cyclically a certain number of steps.
* MixColumns : a linear mixing operation which operates on the columns of the state, combining the four bytes in each column.
  

   * AddRoundKey : the subkey is combined with the state.

[^4]: https://en.wikipedia.org/wiki/Advanced_Encryption_Standard

## Chosen method

Based on the subject, the most relevant method is the Redundant Pattern encoding. Unfortunately, this method is quite difficult to implement in code. For example, we'll use the Less Significant Byte method instead.
The message in the image needs to be encrypted, we used the AES algorithm which uses the same key also to encode and decode the message.
After having encrypted the message, it is added to an image using the method specified previously. With this simple method, agents can send images with a hidden message.
However, the text of the email also needs to be "steganographied". We also used an AES algorithm to encrypt the text.

### Risk management
It was relevant to perform some little improvements to limit security risks. 
By example here are some details that must be took in count : 
 * The application name mustn't reveal the goal.
 * The best is to add an authentication system to prevent anybody to inspect or to send false informations.
 * Generated images must not reveal any differences with the source ones.
 * Obfuscate source code to prevent anybody from reading it freely.

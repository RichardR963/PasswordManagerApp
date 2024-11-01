
# Password Manager App

This is a simple Password Manager App I built because I wanted a way to locally store my passwords on my machine, 
in an encrypted state, and the app will decrypt when I want to see/edit/create/delete them, and re-encrypt them onto a file
when I'm done.


## Features

- Reads from a comma delimited text file, which has the already encrypted password and the associated site and username/email
- Decrypts the passwords when ran
- Displays the decrypted passwords, site and username/email in a simple gridview format.
- All fields are editable
- Can create/delete password entries
- Changes are written to the file immediately.


## 🛠 Built with
- ASP.NET Framework 4.8
- Web Forms application
- HTML, CSS


## Environment Variables

To run this project, you will need to add the following variables:

`FilePath` (this is the path to your text file)

`Key` (this is your desired key for both encryption and decryption)


## FAQ

#### Question 1: What kind of file is used to store the password entries?

Answer 1: A simple text file is used. Passwords are already encrypted

#### Question 2: Is there a specific format to store the password entries?

Answer 2: Yes. It must be in this format: site,username/email,encryptedPassword


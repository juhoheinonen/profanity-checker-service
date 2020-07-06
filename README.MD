# Profane language checker assignment

## Client application

I haven't developed for SalesForce earlier, and I'm not sure if the application is packed in correct way. However, I started a developer SalesForce account and managed to add Apex trigger and class which are used when an attachment is uploaded for a case. Probably also other parts of the application which use attachments fire the trigger, but I haven't tested.

### Application structure

The client application is in the repository under SalesForceClientApp folder. It consists of two files:

#### UploadAttachmentTrigger.tgr

A simple trigger that triggers when an attachment file is uploaded to SalesForce. I learned that if using Lightning apps, then you should use ContentDocumentLink to be able to detect
the attahcment upload event. For every ContentDocumentLink found, calling asynchronically AsyncProfanityChecker.

#### AsyncProfanityChecker.cls

An Apex class that reads the content of a single attachment file. Checks the file content and if a StringException is thrown, e.g. if file is an image file, considering file content empty.
If file content is not empty, trying to send it through a post request to back endservice. A return value of 'false' means that the tested text has been detected to contain profane language. In this case,
the ContentDocumentLink is deleted.

### Installation

1. Open your SalesForce account.
2. Go to Developer Console 
3. Create a new Apex trigger, setting as its name UploadAttachmentTrigger as its name and ContentDocumentLink as its SObject.
4. Copy file's contents from UploadAttachmentTrigger.tgr.
5. Create a new Apex class, setting as its name AsyncProfanityChecker.
6. Copy file's contents from AsyncProfanityChecker.cls
7. In your SalesForce main menu open Setup. 
8. In Remote Site Settings, add url for back end api site.

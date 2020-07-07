# Profane language checker assignment

The application with both client and backend source codes are found in GitHub repository 
https://github.com/juhoheinonen/profanity-checker-service. Most relevant code is found in master branch.

## Client application

Used technology: Apex trigger and Apex class. Using asynchronous processing.

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

### Deploying application 

1. Open your SalesForce account.
2. Go to Developer Console 
3. Create a new Apex trigger, setting as its name UploadAttachmentTrigger as its name and ContentDocumentLink as its SObject.
4. Copy file's contents from UploadAttachmentTrigger.tgr.
5. Create a new Apex class, setting as its name AsyncProfanityChecker.
6. Copy file's contents from AsyncProfanityChecker.cls
7. In your SalesForce main menu open Setup. 
8. In Remote Site Settings, add url for back end api site.

### Testing manually

1. Open Cases
2. Create a new Case or select existing one.
3. Try to add one or several attachment files with a profane word.
4. The file is deleted immediately, and won't show in Attachments list. 
5. Try to add one or several attachment files with no profane words.
6. The files should be normally handled and added to Attachments list.

Note. The toast message "1 file was added to Case." is shown even in case the file contains profanities and isn't really added to Attachments list.

## Backend application

Used technology: .NET Core Web Api, MsTest unit testing framework. .Net Core SDK version 3.1 needs to be installed on the machine the application is built and also where it is deployed.

Validation for input text is very simple. If it contains any word in the list of profane words (introduced later), then the text is considered invalid. Currently supports only English, i.e. only a list of English profane words is used. Also, only words, not phrases are checked. 

Currently no authentication is implemented.

### Application Structure

Application is a solutions consisting of two projects.

#### ProfanityCheckerService

ProfanityCheckController is a Rest api controller that has just one post method that receives the text to be checked for profane words. Returns an ObjectResult with value "true" = file is valid, or "false", file contains profanities. Project contains also two manager/service classes. InputTextPreparer goes through the input text and splits it by using whitespace as separator and remvoes non-alphanumeric characters. ProfanityCheckManager does the validating of the input text.

##### Important file: Content/list.txt

List of profane words used in the application. If any of these is found with case insensitive search in input text, then the text is considered invalid.

#### ProfanityCheckerService.UnitTests

Contains unit tests testing e.g. with valid and invalid files if expected validation result is returned.

### Testing

#### Unit tests

The solution contains some unit tests that can be run in Visual Studio. From Command line, they can be run by going to the folder where the .sln file file for the solution is and entering command line command "dotnet test." The 
tests can also be run using Visual Studio 2019, probably also older versions.

#### Testing locally

1. Build and run the project ProfanityCheckerService. The application opens a browser window with notice like "This page isn't working at the moment". This is ok.
2. Send a POST method, e.g. with Postman, to address https://localhost:44315/api/profanitycheck. The body of the message should be raw Json string, e.g. 
```
    {
        "content": "hello, this text does not contain profanities."
    }
```
The key content must be used, otherwise the rest api method won't be able to read content.

3. A return value of 'true' or 'false' is returned.

### Deploying application

Probably most straightforward way to install this application is to publish it using Visual Studio. Just open the solution in Visual Studio and right-click project ProfanityCheckerService and follow the steps to deploy the application to an Azure account. Of course, one needs an Azure account.

Just right click publish, and follow instructions to deploy it to Azure account. You should select AppService as application type.

After this, add site url as the remote site address in SalesForce (see Client app Deploying application instructions).

#### Extra assignments

##### How to notify the user about deletion of attachment file?

I studied this a bit and found out that it isn't possible to command directly a toast to show from Apex code. However, if a file is detected as invalid, probably a message of it could be sent to a queue. On the Salesforce client side code there could be a module that polls the queue and shows a toast message when a file was detected as inappropriate.

Also I noticed, that if ContentDocumentLink was deleted in Apex code in early enough phase, then a red background toast telling of failing of adding attachment was shown. I'm not sure if this is a feasible option to notify the user, but probably it could be used.

##### How to handle files with bigger size than 12MB?

I can think of such a possibility to read the file's content in Apex code and sending it in smaller chunks to backend service. Depending on file size, maybe they could be sent sequentially to the backend, and if just one chunk fails validation, then the process is stopped and the file is deleted.

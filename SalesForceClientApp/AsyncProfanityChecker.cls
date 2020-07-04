public class AsyncProfanityChecker implements Queueable, Database.AllowsCallouts {
    ContentDocumentLink cdl;

    public AsyncProfanityChecker(ContentDocumentLink cdl) {
        this.cdl = cdl;
    }

    public void execute(QueueableContext context) {
        String fileContent = getFileContent();

        if (fileContent.length() > 0)
        {
            HttpRequest req = new HttpRequest();
            String url = 'https://profanitycheckerservice.azurewebsites.net/api/profanitycheck';
            //String query = '?input=' + EncodingUtil.urlEncode(fileContent, 'UTF-8');
            req.setEndpoint(url);
            req.setMethod('POST');

            req.setHeader('Content-length', '0');
            req.setHeader('Content-type', 'application/json');

            req.setBody('{ "Content": "' + fileContent + '"}');

            Http http = new Http();

            try {

                HTTPResponse res = http.send(req);

                System.debug('here');

                Integer statusCode = res.getStatusCode();
                if (statusCode >= 200 && statusCode < 300) {
                    System.debug('here!!!!');
                    String documentIsValid = res.getBody();
                    if (documentIsValid == 'false') {
                        delete cdl;
                    }
                } else {
                    System.debug('Status code: ' + statusCode);
                    System.debug('Status text: ' + res.getStatus());
                }
            } catch (Exception ex) {
                System.debug('Exception: ' + ex.getMessage());
            }
        }
    }

    private String getFileContent() {
        String docId = cdl.ContentDocumentId;
        ContentVersion contentVersion = [SELECT VersionData FROM ContentVersion WHERE ContentDocumentId = :docId AND IsLatest = true];

        String fileContent;

        // add try catch because VersionData could be a blob, not text.
        try {
            fileContent = contentVersion.VersionData.toString();
            fileContent = EncodingUtil.urlEncode(fileContent, 'UTF-8');
        } catch (StringException ex) {
            fileContent = '';
        }

        return fileContent;
    }
}
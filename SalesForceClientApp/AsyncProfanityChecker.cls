public class AsyncProfanityChecker implements Queueable, Database.AllowsCallouts {
    ContentDocumentLink cdl;
    
    public AsyncProfanityChecker(ContentDocumentLink cdl) {
        this.cdl = cdl;        
    }
    
    public void execute(QueueableContext context) {
        String fileContent = getFileContent();
        
        HttpRequest req = new HttpRequest();        
        String url = 'https://profanitycheckerservice.azurewebsites.net/api/profanitycheck';
        String query = '?input=' + EncodingUtil.urlEncode(fileContent, 'UTF-8');        
        req.setEndpoint(url + query);
        req.setMethod('GET');              
        
        Http http = new Http();
        HTTPResponse res = http.send(req);
                
        String documentIsValid = res.getBody();
        if (documentIsValid == 'false') {
         	delete cdl;               
        }
    }
    
    private String getFileContent() {
        String docId = cdl.ContentDocumentId;                        
        ContentVersion contentVersion = [SELECT VersionData FROM ContentVersion WHERE ContentDocumentId = :docId AND IsLatest = true];
                        
        String fileContent = contentVersion.VersionData.toString();
        
        return fileContent;
    }
}
mergeInto(LibraryManager.library, {

  ShowFullscreenAd: function () {
    showFullscrenAd();
  },

  ReadDataFirebase: function(referencePath, objectName, successfulCallback, failedCallback){
    var reference = UTF8ToString(referencePath);
    var nameMethodInvoke = UTF8ToString(objectName);
    var successful = UTF8ToString(successfulCallback);
    var failed = UTF8ToString(failedCallback);

    try {
        firebase.database().ref(reference).once('value').then(function(snapshot) {
            unityInstance.Module.SendMessage(nameMethodInvoke, successful, JSON.stringify(snapshot.val()));
        });
    } catch (error) {
         unityInstance.Module.SendMessage(nameMethodInvoke, failed, "Error ReadDataFirebase: " + error.message);
    }
  },

  WriteDataFirebase: function(referencePath, value, objectName, callback, fallback) {
        var parsedPath = UTF8ToString(referencePath);
        var parsedValue = UTF8ToString(value);
        var parsedObjectName = UTF8ToString(objectName);
        var parsedCallback = UTF8ToString(callback);
        var parsedFallback = UTF8ToString(fallback);

        try {

            firebase.database().ref(parsedPath).set(JSON.parse(parsedValue)).then(function(unused) {
                unityInstance.Module.SendMessage(parsedObjectName, parsedCallback, "Success: " + parsedValue + " was posted to " + parsedPath);
            });

        } catch (error) {
            unityInstance.Module.SendMessage(parsedObjectName, parsedFallback, JSON.stringify(error, Object.getOwnPropertyNames(error)));
        }
    },

    GetTypePlatfromDevice: function(){
        getTypeDevice();
    }
  
});
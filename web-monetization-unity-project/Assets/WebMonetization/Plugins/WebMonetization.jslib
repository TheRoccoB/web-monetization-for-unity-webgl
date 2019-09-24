

mergeInto(LibraryManager.library, {
    
    InitializeMonetization: function (paymentPointer, verbose) {
        
        function log(){
            if (verbose){
                console.log.apply(this);
            }
        }
        
        var paymentPointerString = '$coil.xrptipbot.com/JABJLDXNSje7h_bY26_6wg';
            
        if (paymentPointer) {
            paymentPointerString = Pointer_stringify(paymentPointer);
        }
        else{
            console.log('It appears that your payment pointer is not configured properly.');
        }
        
        // remove any existing payment tags
        var existingMonetizationTags = document.querySelectorAll('meta[name=monetization]');
        existingMonetizationTags.forEach(function(el){
            console.log('removing existing monetization tag', el);
            el.parentNode.removeChild(el);
        });

        var meta = document.createElement('meta');
        meta.setAttribute('name', 'monetization');
        meta.setAttribute('content', paymentPointerString);
        document.head.append(meta);

        //monetization start event.
        
        if (document.monetization){
            
            function sendMessageToWebMonetizationBroadcaster(fnName, detail){
                unityInstance.SendMessage('WebMonetizationBroadcaster', fnName, JSON.stringify(detail));
            }
            
            document.monetization.addEventListener('monetizationstart', function(event){
                sendMessageToWebMonetizationBroadcaster('monetizationstart', event.detail);
            });
            
            document.monetization.addEventListener('monetizationprogress', function(event){
                sendMessageToWebMonetizationBroadcaster('monetizationprogress', event.detail);
            });
        }
        else{
            console.log('no monetization found');
        }
        
    }

});
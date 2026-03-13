mergeInto(LibraryManager.library, {

  IsMobileBrowser: function () {
    var userAgent = navigator.userAgent || navigator.vendor || window.opera;
    
    if (/android|iphone|ipad|ipod|blackberry|iemobile|opera mini/i.test(userAgent.toLowerCase())) {
      return true;
    }
    return false;
  },

});
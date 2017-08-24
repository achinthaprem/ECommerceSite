﻿volume.toolkit.browserDetector = (function()
{
	// From http://msdn.microsoft.com/en-us/library/ms537509(v=vs.85).aspx
	var getInternetExplorerVersion = function()
		// Returns the version of Internet Explorer or a -1
		// (indicating the use of another browser).
	{
		var rv = -1; // Return value assumes failure.
		if (navigator.appName == 'Microsoft Internet Explorer') {
			var ua = navigator.userAgent;
			var re = new RegExp("MSIE ([0-9]{1,}[\.0-9]{0,})");
			if (re.exec(ua) != null)
				rv = parseFloat(RegExp.$1);
		}
		return rv;
	};

	return {
		getInternetExplorerVersion: getInternetExplorerVersion
	};
})();
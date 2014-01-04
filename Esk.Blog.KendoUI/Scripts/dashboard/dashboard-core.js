// Reference: http://elegantcode.com/2011/01/26/basic-javascript-part-8-namespaces/
// Reference: http://stackoverflow.com/questions/2504568/javascript-namespace-declaration
if (typeof ESK === "undefined" || !ESK) {
	var ESK = {};
}

ESK.namespace = function namespace(namespaceName) {
	var parts = namespaceName.split("."),
		parent = window,
		currentPart = '';

	for (var i = 0, length = parts.length; i < length; i++) {
		currentPart = parts[i];
		parent[currentPart] = parent[currentPart] || {};
		parent = parent[currentPart];
	}

	return parent;
}

ESK.namespace("ESK.Blog.Site");
// Set the global root url for other BTMU modules.
ESK.Blog.Site = {
	ContentUrl: '',
	ImageUrl: ''
};

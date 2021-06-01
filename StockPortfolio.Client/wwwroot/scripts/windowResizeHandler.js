function raiseResizeEvent() {
    var namespace = 'StockPortfolio.Client';
    var method = 'RaiseWindowResizeEvent'; //the name of the method in our "service"
    DotNet.invokeMethodAsync(namespace, method);
}

var timeout = false;
window.addEventListener("resize", function () {
    if (timeout !== false)
        clearTimeout(timeout);
    timeout = setTimeout(raiseResizeEvent, 200);
});
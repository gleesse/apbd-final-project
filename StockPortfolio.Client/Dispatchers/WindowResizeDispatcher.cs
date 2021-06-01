using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace StockPortfolio.Dispatchers
{
    public class WindowResizeDispatcher
    {
        private readonly IJSRuntime _jsRuntime;
        public WindowResizeDispatcher(IJSRuntime jsRuntime){
            _jsRuntime = jsRuntime;
        }
        public static event Func<Task> WindowResize;
        [JSInvokable]
        public static async Task RaiseWindowResizeEvent()
        {
            try{
                await WindowResize?.Invoke();
            }
            catch{}
        }

        public async Task<BrowserDimensions> GetBrowserWidth(){
            try{
                return await _jsRuntime.InvokeAsync<BrowserDimensions>("getDimensions");
            }catch{}
            return null;
        }
    }

    public class BrowserDimensions
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }
}

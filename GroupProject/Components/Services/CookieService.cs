using Microsoft.JSInterop;

namespace GroupProject.Components.Services
{
    public interface ICookieService
    {
        Task SetCookieAsync(string name, string value, int days);
        Task<string> GetCookieAsync(string name);
        Task DeleteCookieAsync(string name);
    }

    public class CookieService : ICookieService
    {
        private readonly IJSRuntime _jsRuntime;

        public CookieService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task SetCookieAsync(string name, string value, int days)
        {
            var expires = DateTime.Now.AddDays(days).ToUniversalTime().ToString("R");
            await _jsRuntime.InvokeVoidAsync("eval", 
                $"document.cookie = `{name}={value};expires={expires};path=/`");
        }

        public async Task<string> GetCookieAsync(string name)
        {
            var cookieValue = await _jsRuntime.InvokeAsync<string>("eval", @"
                function getCookie(name) {
                    const value = `; ${document.cookie}`;
                    const parts = value.split(`; ${name}=`);
                    if (parts.length === 2) return parts.pop().split(';').shift();
                    return '';
                }
                getCookie('" + name + "')");
            return cookieValue;
        }

        public async Task DeleteCookieAsync(string name)
        {
            await _jsRuntime.InvokeVoidAsync("eval", 
                $"document.cookie = `{name}=;expires=Thu, 01 Jan 1970 00:00:00 UTC;path=/`");
        }
    }
} 
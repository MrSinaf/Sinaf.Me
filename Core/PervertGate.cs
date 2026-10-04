using Microsoft.JSInterop;

namespace Sinaf.Me;

public static class PervertGate
{
	private const string KEY_ARGS = "pervert-confirmed";
	
	public static async Task<bool> GetConfirmed(IJSRuntime js)
	{
		var value = await js.InvokeAsync<string?>("localStorage.getItem", KEY_ARGS);
		return value == "true";
	}
	
	public static async Task SetConfirmed(IJSRuntime js, bool value)
		=> await js.InvokeVoidAsync("localStorage.setItem", KEY_ARGS, value ? "true" : "false");
}
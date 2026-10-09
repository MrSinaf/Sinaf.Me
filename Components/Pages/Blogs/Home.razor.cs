using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using Sinaf.Me.Data;
using Sinaf.Me.Data.Web;

namespace Sinaf.Me.Components.Pages.Blogs;

public partial class Home
{
	[Inject] private IJSRuntime JS { get; set; } = null!;
	[Inject] private AuthenticationStateProvider ASP { get; set; } = null!;
	[Inject] private NavigationManager Nav { get; set; } = null!;
	[Inject] private IWebHostEnvironment Env { get; set; } = null!;
	
	private Blog[]? blogs;
	private bool isPervert;
	private string errorMessage = string.Empty;
	
	private readonly Blog blog = new () {Title = string.Empty};
	private string thumbnailPreviewUrl = "/r34/placeholder_blog.png";
	private IBrowserFile? selectedThumbnail;
	
	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (!firstRender)
			return;
		
		var isConnected = (await ASP.GetAuthenticationStateAsync()).User.Identity?.IsAuthenticated
						  ?? false;
		await using var context = new WebDbContext();
		blogs = await context.Blogs
							 .Select(x => new Blog
							 {
								 Id = x.Id,
								 Title = x.Title,
								 Content = (x.Content.Length < 128
										 ? x.Content
										 : x.Content.Substring(0, 128) + "...").Replace("\n", ""),
								 PublishAt = x.PublishAt,
								 Published = x.Published
							 })
							 .Where(x => isConnected || x.Published)
							 .OrderByDescending(x => x.PublishAt)
							 .ToArrayAsync();
		foreach (var blog in blogs)
			blog.Content = blog.Content.Split('#')[0];
		
		isPervert = await PervertGate.GetConfirmed(JS);
		StateHasChanged();
	}
	
	private async Task OnThumbnailSelected(InputFileChangeEventArgs e)
	{
		var file = e.File;
		
		if (file.ContentType != "image/png")
			return;
		
		
		await using var stream = file.OpenReadStream();
		using var memoryStream = new MemoryStream();
		await stream.CopyToAsync(memoryStream);
		var bytes = memoryStream.ToArray();
		
		if (!Utility.GetPNGSize(bytes, out var width, out var height) 
			|| width != 128 || height != 128)
		{
			errorMessage = "Le thumbnail doit être une image PNG de 128x128 pixels (。_。).";
			StateHasChanged();
			return;
		}
		thumbnailPreviewUrl = $"data:image/png;base64,{Convert.ToBase64String(bytes)}";
		selectedThumbnail = file;
	}
	
	private async Task OnCreateClick()
	{
		await using var context = new WebDbContext();
		do
		{
			blog.Id = (uint)Random.Shared.Next(100_000_000, 999_999_999);
		}
		while (await context.Blogs.AnyAsync(x => x.Id == blog.Id));
		blog.Content = "Mon nouveau blog! `(*>﹏<*)′ *retire sa culotte*";
		
		if (blog.Title.Length < 5)
		{
			errorMessage = "Le titre doit contenir au moins 5 caractères (。_。).";
			StateHasChanged();
			return;
		}
		if (selectedThumbnail == null)
		{
			errorMessage = "Un thumbnail doit être sélectionné (。_。).";
			StateHasChanged();
			return;
		}
		
		var blogDirectory = Path.Combine(Env.WebRootPath, "blogs", blog.Id.ToString());
		Directory.CreateDirectory(blogDirectory);
		
		var thumbnailPath = Path.Combine(blogDirectory, "thumbnail.png");
		await using var sourceStream = selectedThumbnail.OpenReadStream();
		await using var destinationStream = File.Create(thumbnailPath);
		await sourceStream.CopyToAsync(destinationStream);
		
		context.Blogs.Add(blog);
		await context.SaveChangesAsync();
		Nav.NavigateTo($"/blogs/{blog.Id}/edit");
	}
}
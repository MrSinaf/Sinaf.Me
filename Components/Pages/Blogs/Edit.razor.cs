using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Sinaf.Me.Data;
using Sinaf.Me.Data.Web;

namespace Sinaf.Me.Components.Pages.Blogs;

public partial class Edit : ComponentBase
{
	[Parameter] public long Id { get; set; }
	
	private Blog? blog;
	private bool notFound;
	private bool isModified;
	private CancellationTokenSource? contentDebounce;
	
	protected override async Task OnParametersSetAsync()
	{
		await using var context = new WebDbContext();
		blog = await context.Blogs.FirstOrDefaultAsync(x => x.Id == Id);
		notFound = blog == null;
	}
	
	private async Task OnSaveClick()
	{
		await using var context = new WebDbContext();
		context.Update(blog!);
		await context.SaveChangesAsync();
		isModified = false;
		StateHasChanged();
	}
	
	private async Task OnPublishClick()
	{
		await using var context = new WebDbContext();
		blog!.Published = !blog.Published;
		blog!.PublishAt ??= DateTime.Now;
		context.Attach(blog);
		context.Entry(blog).Property(x => x.Published).IsModified = true;
		context.Entry(blog).Property(x => x.PublishAt).IsModified = true;
		await context.SaveChangesAsync();
	}
	
	private async Task OnContentInput(ChangeEventArgs args)
	{
		isModified = true;
		
		if (contentDebounce is not null)
		{
			await contentDebounce.CancelAsync();
			contentDebounce.Dispose();
		}
		contentDebounce = new CancellationTokenSource();
		
		try
		{
			await Task.Delay(500, contentDebounce.Token);
			blog!.Content = args.Value?.ToString() ?? string.Empty;
			StateHasChanged();
		}
		catch (TaskCanceledException) { }
	}
}
using Microsoft.AspNetCore.Components.Forms;

namespace HandmadeShop.Web.Extensions;

public static class BrowseFileExtensions
{
    public static async Task<string> GetDataUrl(this IBrowserFile file)
    {
        var imageStream = file.OpenReadStream();
        var imageBytes = new byte[imageStream.Length];
        var readAsync = await imageStream.ReadAsync(imageBytes, 0, (int)imageStream.Length);
        return $"data:{file.ContentType};base64,{Convert.ToBase64String(imageBytes)}"; 
    }
}
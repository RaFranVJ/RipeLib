using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// Allows Sending Request to a Server by using a URL or Download Files from it

public static partial class UrlFetcher
{
// Http Client used for Responses

private static readonly HttpClient client = new();

// Get Response from Server

public static async Task<string> GetResponseAsync(string link)
{
string responseBody;

try
{
var response = await client.GetAsync(link);
response.EnsureSuccessStatusCode();

responseBody = await response.Content.ReadAsStringAsync();
}

catch(Exception error)
{
responseBody = $"Failed to Get Response from: {link}\nReason: <{error.Message}>";
}

return responseBody;      
}

// Get Response Str

public static string GetResponse(string link)
{
return GetResponseAsync(link).ConfigureAwait(false).GetAwaiter().GetResult();
}

// Get Response from Server

public static async Task<Stream> GetResponseStreamAsync(string link)
{
var response = await client.GetAsync(link);
response.EnsureSuccessStatusCode();

return await response.Content.ReadAsStreamAsync();
}

// Get Response Str

public static Stream GetResponseStream(string link)
{
return GetResponseStreamAsync(link).ConfigureAwait(false).GetAwaiter().GetResult();
}

// Download File Async

public static async Task DownloadFileAsync(string link, string filePath)
{
string responseBody = await GetResponseAsync(link);

await File.WriteAllTextAsync(filePath, responseBody);
}

// Download File

public static void DownloadFile(string link, string filePath)
{
string responseBody = GetResponse(link);

File.WriteAllText(filePath, responseBody);
}

// Get Hosted Files

public static async Task<List<string>> GetFilesListAsync(string link)
{
List<string> files = new();
        
try
{
var response = await client.GetAsync(link);
response.EnsureSuccessStatusCode();

string htmlContent = await response.Content.ReadAsStringAsync(); // Assuming response its HTML
MatchCollection matches = HrefRegex().Matches(htmlContent);

foreach(Match fileMatch in matches)
{
string filePath = fileMatch.Groups[1].Value;

if(!Uri.IsWellFormedUriString(filePath, UriKind.Absolute) )
filePath = new Uri(new Uri(link), filePath).ToString();
                    
files.Add(filePath);
}

}

catch(Exception)
{
}

return files;
}

// Get Files Hosted

public static List<string> GetFilesList(string link)
{
return GetFilesListAsync(link).ConfigureAwait(false).GetAwaiter().GetResult();
}

[GeneratedRegex(@"href\s*=\s*""([^""]+)""", RegexOptions.IgnoreCase) ]
private static partial Regex HrefRegex();
}
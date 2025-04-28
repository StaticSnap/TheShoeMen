using System.Diagnostics;
using GroupProject.Components;
using GroupProject;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient();

// Run the Python 3 server
RunPythonServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

void RunPythonServer()
{
    try
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = "python3", // Ensure 'python3' is in your PATH or provide the full path to the Python 3 executable
            Arguments = "server.py",
            WorkingDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Back End", "Databases"), // Adjust the path as needed
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        var process = new Process
        {
            StartInfo = startInfo
        };

        process.OutputDataReceived += (sender, args) => Console.WriteLine(args.Data);
        process.ErrorDataReceived += (sender, args) => Console.WriteLine($"Error: {args.Data}");

        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
    }
    catch (Exception ex)
    {
        try
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "python", // Ensure 'python3' is in your PATH or provide the full path to the Python 3 executable
                Arguments = "server.py",
                WorkingDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Back End", "Databases"), // Adjust the path as needed
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var process = new Process
            {
                StartInfo = startInfo
            };

            process.OutputDataReceived += (sender, args) => Console.WriteLine(args.Data);
            process.ErrorDataReceived += (sender, args) => Console.WriteLine($"Error: {args.Data}");

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
        }
        catch
        {
            Console.WriteLine($"Failed to start Python server: {ex.Message}");
        }
    }
}
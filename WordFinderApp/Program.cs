//Ask for matrix text file
using BenchmarkDotNet.Running;
using Microsoft.Extensions.Configuration;
using WordFinderApp;

var builder = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Construir la configuración
IConfiguration configuration = builder.Build();

// Leer configuraciones específicas
bool commandLineRequest = configuration.GetValue<bool>("commandLineRequest");
try
{
    
    IEnumerable<string> matrix;
    IEnumerable<string> wordStream;
    if (commandLineRequest)
    {
        Console.WriteLine("Please, provide a txt file path with the matrix: ");
        Console.WriteLine("Example: \nabcdc\nfgwio\nchill\npqnsd\nuvdxy ");
        matrix = Utils.GetFileFromConsole();
        Console.WriteLine("Please, provide a txt file path whit the stream of words, one per line: ");
        Console.WriteLine("Example: \nchill\nwind\ncold");
        wordStream = Utils.GetFileFromConsole();
    }
    else
    {
        matrix = Utils.GetFileFromConfig(configuration, "matrixFilePath");
        wordStream = Utils.GetFileFromConfig(configuration, "wordStreamFilePath");
    }

    WordFinder wordFinder = new WordFinder(matrix);
    IEnumerable<string> result = wordFinder.Find(wordStream);

    //var summary = BenchmarkRunner.Run<BenchMarkApp>();

    Console.WriteLine(string.Join(Environment.NewLine, result));
}
catch (Exception ex)
{
    Console.WriteLine($"Error initializing the WordFinder: {ex.Message}");
}



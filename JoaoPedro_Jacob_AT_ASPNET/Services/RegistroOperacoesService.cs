namespace JoaoPedro_Jacob_AT_ASPNET.Services;

public class RegistroOperacoesService
{
    private readonly IWebHostEnvironment _env;
    private readonly List<string> _memoria = new();

    public RegistroOperacoesService(IWebHostEnvironment env) => _env = env;

    public IReadOnlyList<string> Memoria => _memoria;

    public void Registrar(string mensagem)
    {
        Action<string> logger = LogToConsole;
        logger += LogToFile;
        logger += LogToMemory;

        logger(mensagem);
    }

    private void LogToConsole(string mensagem)
    {
        Console.WriteLine($"[CONSOLE] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {mensagem}");
    }

    private void LogToFile(string mensagem)
    {
        var pasta = Path.Combine(_env.WebRootPath, "files");
        Directory.CreateDirectory(pasta);
        var arquivo = Path.Combine(pasta, "log.txt");
        var linha = $"[ARQUIVO] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {mensagem}{Environment.NewLine}";
        File.AppendAllText(arquivo, linha);
    }

    private void LogToMemory(string mensagem)
    {
        _memoria.Add($"[MEMÓRIA] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {mensagem}");
    }
}

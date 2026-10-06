Console.OutputEncoding = System.Text.Encoding.UTF8;

var gerenciador = new GerenciadorTarefas();

gerenciador.Adicionar("Estudar C#", "Terminar o curso da Microsoft");
gerenciador.Adicionar("Treinar LINQ");

var executando = true;

while (executando)
{
    MostrarMenu();
    Console.Write("Escolha uma opção: ");


    string? opcao = Console.ReadLine();
    Console.WriteLine();

    switch (opcao)
    {
        case "1": AdicionarTarefa(gerenciador); break;
        case "2": ListarTarefas(gerenciador.ObterTodas()); break;
        case "3": ConcluirTarefa(gerenciador); break;
        case "4": RemoverTarefa(gerenciador); break;
        case "5": BuscarTarefas(gerenciador); break;
        case "6": MostrarEstatisticas(gerenciador); break;
        case "0":
            executando = false;
            Console.WriteLine("Até logo!");
            break;
        default:
            Console.WriteLine("Opção inválida. Tente de novo.");
            break;
    }

    Console.WriteLine();
}

static void MostrarMenu()
{
    Console.WriteLine("========== TAREFAS ==========");
    Console.WriteLine("1 - Adicionar tarefa");
    Console.WriteLine("2 - Listar tarefas");
    Console.WriteLine("3 - Concluir tarefa");
    Console.WriteLine("4 - Remover tarefa");
    Console.WriteLine("5 - Buscar por título");
    Console.WriteLine("6 - Estatísticas");
    Console.WriteLine("0 - Sair");
    Console.WriteLine("=============================");
}

static void AdicionarTarefa(GerenciadorTarefas gerenciador)
{
    Console.Write("Título: ");
    string? titulo = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(titulo))
    {
        Console.WriteLine("O título é obrigatório.");
        return;
    }

    Console.Write("Descrição (opcional, Enter para pular): ");
    string? descricao = Console.ReadLine();

    var tarefa = gerenciador.Adicionar(
        titulo,
        string.IsNullOrWhiteSpace(descricao) ? null : descricao);

    Console.WriteLine($"Tarefa criada com o Id {tarefa.Id}.");
}

static void ListarTarefas(IEnumerable<Tarefa> tarefas)
{
    var lista = tarefas.ToList();

    if (lista.Count == 0)
    {
        Console.WriteLine("Nenhuma tarefa encontrada.");
        return;
    }

    foreach (var tarefa in lista)
    {
        Console.WriteLine(tarefa);
    }
}

static void ConcluirTarefa(GerenciadorTarefas gerenciador)
{
    int? id = LerInteiro("Id da tarefa a concluir: ");

    if (id is null)
    {
        Console.WriteLine("Id inválido.");
        return;
    }

    Console.WriteLine(gerenciador.Concluir(id.Value)
        ? "Tarefa concluída!"
        : "Tarefa não encontrada.");
}

static void RemoverTarefa(GerenciadorTarefas gerenciador)
{
    int? id = LerInteiro("Id da tarefa a remover: ");
    if (id is null)
    {
        Console.WriteLine("Id inválido.");
        return;
    }

    Console.WriteLine(gerenciador.Remover(id.Value)
        ? "Tarefa removida."
        : "Tarefa não encontrada.");
}

static void BuscarTarefas(GerenciadorTarefas gerenciador)
{
    Console.Write("Buscar por (parte do título): ");
    string? texto = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(texto))
    {
        Console.WriteLine("Digite algum texto para buscar.");
        return;
    }

    ListarTarefas(gerenciador.Buscar(texto));
}

static void MostrarEstatisticas(GerenciadorTarefas gerenciador)
{
    var todas = gerenciador.ObterTodas();

    int total = todas.Count;
    int concluidas = todas.Count(t => t.Concluida);
    int pendentes = total - concluidas;

    double percentual = total == 0 ? 0 : concluidas * 100.0 / total;

    Console.WriteLine($"Total: {total}");
    Console.WriteLine($"Concluídas: {concluidas}");
    Console.WriteLine($"Pendentes: {pendentes}");

    Console.WriteLine($"Progresso: {percentual:F1}%");

    var maisAntiga = todas
        .Where(t => !t.Concluida)
        .OrderBy(t => t.CriadaEm)
        .FirstOrDefault();

    if (maisAntiga is not null)
        Console.WriteLine($"Pendente mais antiga: {maisAntiga.Titulo}");
}


static int? LerInteiro(string mensagem)
{
    Console.Write(mensagem);
    string? texto = Console.ReadLine();

    if (int.TryParse(texto, out int numero))
        return numero;

    return null;
}

class Tarefa
{
    public int Id { get; init; }

    public string Titulo { get; set; } = string.Empty;

    public string? Descricao { get; set; }

    public bool Concluida { get; set; }

    public DateTime CriadaEm { get; } = DateTime.Now;

    public override string ToString()
    {
        var marca = Concluida ? "x" : " ";
        var descricao = string.IsNullOrWhiteSpace(Descricao) ? "" : $" - {Descricao}";
        return $"[{marca}] #{Id} {Titulo}{descricao}";
    }
}

class GerenciadorTarefas
{
    private readonly List<Tarefa> _tarefas = new();

    private int _proximoId = 1;

    public Tarefa Adicionar(string titulo, string? descricao = null)
    {
        var tarefa = new Tarefa
        {
            Id = _proximoId++,
            Titulo = titulo.Trim(),
            Descricao = descricao
        };

        _tarefas.Add(tarefa);
        return tarefa;
    }

    public IReadOnlyList<Tarefa> ObterTodas() =>
        _tarefas.OrderBy(t => t.Id).ToList();

    public IEnumerable<Tarefa> Buscar(string texto) =>
        _tarefas.Where(t => t.Titulo.Contains(texto, StringComparison.OrdinalIgnoreCase));

    public bool Concluir(int id)
    {
        var tarefa = _tarefas.FirstOrDefault(t => t.Id == id);
        if (tarefa is null)
            return false;

        tarefa.Concluida = true;
        return true;
    }

    public bool Remover(int id) =>
        _tarefas.RemoveAll(t => t.Id == id) > 0;
}
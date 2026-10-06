# Gerenciador de Tarefas (C# / .NET)

Meu primeiro projeto em C#: um gerenciador de tarefas em **aplicação de console**, feito para aprender C# e .NET.

## O que o programa faz

Menu no terminal com as opções:

- adicionar, listar, concluir e remover tarefas
- buscar tarefas por título
- ver estatísticas (total, concluídas, pendentes e progresso)

Os dados ficam em memória, então somem ao fechar o programa.

## Tecnologias

- **C#**: a linguagem
- **.NET 10**: a plataforma onde o código roda (SDK, runtime e bibliotecas)

> C# é a linguagem e .NET é a plataforma. Este projeto usa os dois.

## Como rodar

```bash
dotnet run
```

Requer o [.NET SDK](https://dotnet.microsoft.com/download) instalado.

## O que aprendi

- Estrutura de um projeto: `.csproj`, `Program.cs` e *top-level statements* (sem `Main`)
- Variáveis, `if`, `switch`, `while`, `foreach` e métodos
- Classes, **propriedades** (`get`, `set`, `init`) e `override ToString()`
- `List<T>` e **LINQ** (`Where`, `OrderBy`, `Count`, `FirstOrDefault`)
- Nulos com `string?`, `is null` e `int.TryParse`
- Interpolação de strings: `$"Olá {nome}"`



# Agência de Turismo — ASP.NET Core

Aplicação web de uma **agência de turismo** desenvolvida em **ASP.NET Core Razor Pages (.NET 10)**.
O sistema permite gerenciar clientes, destinos, pacotes turísticos e reservas, com autenticação de
usuários e demonstração prática de conceitos de C# como *delegates*, *eventos* e *multicast delegates*.

> Projeto acadêmico (Avaliação Técnica) com foco em CRUD completo, persistência com Entity Framework Core
> e aplicação de delegates/eventos em cenários reais.

---

## ✨ Funcionalidades

- **Autenticação e identidade** — cadastro, login e logout de usuários via ASP.NET Core Identity.
- **CRUD de Clientes** — cadastro, listagem, edição e exclusão.
- **CRUD de Destinos** — gestão dos destinos disponíveis.
- **CRUD de Pacotes Turísticos** — com preço e **capacidade máxima** de lugares.
- **CRUD de Reservas** — vincula um cliente a um pacote, controlando a quantidade de lugares.
- **Simulação de Reserva** — cálculo do valor de uma reserva antes de confirmá-la.
- **Desconto de Pacote** — aplicação de descontos usando *delegate* de cálculo.
- **Notas / cálculos** — página demonstrando uso de delegates sobre valores.
- **Registro de Operações** — log das ações realizadas (console, arquivo e memória) via *multicast delegate*.
- **Evento de capacidade esgotada** — quando um pacote atinge a capacidade máxima, um **evento**
  (`CapacityReached`) é disparado e novas reservas são bloqueadas.
- **Seed automático** — banco e usuário padrão criados na inicialização.

---

## 🧩 Conceitos de C# demonstrados

| Conceito | Onde é aplicado |
|----------|-----------------|
| **Delegate** (`CalculateDelegate`) | Cálculo de descontos e simulações (`Delegates/AgenciaDelegate.cs`) |
| **Multicast delegate** (`Action<string>`) | Registro de operações em console + arquivo + memória (`RegistroOperacoesService`) |
| **Eventos** (`event EventHandler<>`) | Notificação de capacidade máxima atingida (`Models/Reserva.cs` + `Program.cs`) |
| **EventArgs customizado** | `CapacityReachedEventArgs` carregando dados do pacote |

---

## 🛠️ Tecnologias utilizadas

- **.NET 10** / **C#**
- **ASP.NET Core Razor Pages**
- **Entity Framework Core 10** (Code First + Migrations)
- **SQLite** como banco de dados
- **ASP.NET Core Identity** (autenticação e gestão de usuários)
- Injeção de dependência nativa do ASP.NET Core
- HTML / CSS / Bootstrap (frontend padrão do Razor Pages)

---

## 🏗️ Arquitetura e práticas

- **Separação por responsabilidade**
  - `Models/` — entidades de domínio (Cliente, Destino, PacoteTuristico, Reserva)
  - `Data/` — `DbContext`, configurações de mapeamento (`Configurations/`) e seed (`DbInitializer`)
  - `Services/` — regras de negócio (`ReservaService`, `RegistroOperacoesService`)
  - `Delegates/` — definição de delegates de cálculo
  - `Pages/` — páginas Razor organizadas por funcionalidade
  - `Areas/Identity/` — telas de autenticação
- **Injeção de dependência** com tempos de vida adequados (`Scoped` para serviços de dados, `Singleton` para o registro de operações).
- **Padrão Code First** com Migrations versionadas em `Migrations/`.
- **Fluent API** para configuração do mapeamento das entidades (`Data/Configurations/`).
- **Nullable reference types** e **implicit usings** habilitados.
- **Programação orientada a eventos** para regras de negócio (bloqueio por capacidade).

---

## 🚀 Como executar

Pré-requisitos: **.NET 10 SDK** instalado.

```bash
# clonar o repositório
git clone <url-do-repositorio>
cd JoaoPedro_Jacob_AT_ASPNET/JoaoPedro_Jacob_AT_ASPNET

# restaurar dependências e rodar
dotnet restore
dotnet run
```

A aplicação cria o banco SQLite (`agencia.db`), aplica as migrations e popula os dados iniciais
automaticamente na primeira execução. Em seguida, acesse a URL exibida no console
(ex.: `https://localhost:5001`).

---

## 📁 Estrutura do projeto

```
JoaoPedro_Jacob_AT_ASPNET/
├── Areas/Identity/      # autenticação (login, registro, logout)
├── Data/                # DbContext, configurations e seed
├── Delegates/           # delegates de cálculo
├── Migrations/          # migrations do EF Core
├── Models/              # entidades de domínio + eventos
├── Pages/               # páginas Razor por funcionalidade
├── Services/            # regras de negócio e registro de operações
├── wwwroot/             # arquivos estáticos
└── Program.cs           # configuração e bootstrap da aplicação
```

---

## 📸 Prints

As capturas de tela do sistema em funcionamento estão disponíveis em
[`prints.md`](./JoaoPedro_Jacob_AT_ASPNET/prints.md).

---

## 👤 Autor

**João Pedro Jacob** — projeto desenvolvido para a disciplina de programação com ASP.NET Core.

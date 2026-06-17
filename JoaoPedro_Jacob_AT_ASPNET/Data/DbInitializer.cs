using JoaoPedro_Jacob_AT_ASPNET.Models;
using Microsoft.AspNetCore.Identity;

namespace JoaoPedro_Jacob_AT_ASPNET.Data;

public static class DbInitializer
{
    public static async Task SeedUsuarioPadraoAsync(UserManager<IdentityUser> userManager)
    {
        const string email = "admin@flyair.com";
        const string senha = "Admin@123";

        if (await userManager.FindByEmailAsync(email) != null)
            return;

        var usuario = new IdentityUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true
        };
        await userManager.CreateAsync(usuario, senha);
    }

    public static void Seed(AgenciaContext context)
    {
        if (context.PacotesTuristicos.Any())
            return;

        var destinos = new List<Destino>
        {
            new() { Nome = "Paris", Pais = "França",
                Descricao = "A Cidade Luz encanta com a Torre Eiffel, o Louvre e a gastronomia refinada. Perfeita para quem busca arte, romance e cultura." },
            new() { Nome = "Roma", Pais = "Itália",
                Descricao = "A Cidade Eterna reúne o Coliseu, o Vaticano e a Fontana di Trevi. Uma viagem por milênios de história e culinária incomparável." },
            new() { Nome = "Nova York", Pais = "EUA",
                Descricao = "A metrópole que nunca dorme: Times Square, Central Park, a Estátua da Liberdade e uma infinidade de atrações culturais e gastronômicas." },
            new() { Nome = "Lisboa", Pais = "Portugal",
                Descricao = "Cidade de sete colinas com charme único: pastéis de nata, o Fado, o Castelo de São Jorge e vistas deslumbrantes sobre o Tejo." },
            new() { Nome = "Tóquio", Pais = "Japão",
                Descricao = "Uma fusão fascinante de tradição e modernidade: templos milenares, culinária premiada, tecnologia de ponta e uma cultura única no mundo." },
            new() { Nome = "Cancún", Pais = "México",
                Descricao = "Praias de areia branca e águas turquesas do Caribe, ruínas maias nas proximidades e uma animada vida noturna. Paraíso tropical completo." },
            new() { Nome = "Cairo", Pais = "Egito",
                Descricao = "Lar das Pirâmides de Gizé e da Esfinge, o Cairo mergulha o viajante em uma civilização com mais de 5 mil anos de história." },
            new() { Nome = "Sydney", Pais = "Austrália",
                Descricao = "A Opera House, a Harbour Bridge e praias icônicas como Bondi compõem uma cidade vibrante em um dos países mais fascinantes do mundo." },
        };
        context.Destinos.AddRange(destinos);

        var clientes = new List<Cliente>
        {
            new() { Nome = "João Silva", Email = "joao@email.com", Telefone = "(11) 99999-1234" },
            new() { Nome = "Maria Santos", Email = "maria@email.com", Telefone = "(21) 98888-5678" },
            new() { Nome = "Pedro Oliveira", Email = "pedro@email.com", Telefone = "(31) 97777-9012" },
        };
        context.Clientes.AddRange(clientes);

        var pacotes = new List<PacoteTuristico>
        {
            new()
            {
                Titulo = "Europa Clássica",
                Descricao = "Explore os grandes ícones europeus em uma jornada inesquecível. Paris e Roma em um único roteiro com guia especializado, hotéis 4 estrelas e transfers incluídos.",
                DataInicio = DateTime.Today.AddMonths(2),
                CapacidadeMaxima = 20,
                Preco = 8500.00m,
                Destinos = new List<Destino> { destinos[0], destinos[1] }
            },
            new()
            {
                Titulo = "Nova York Especial",
                Descricao = "Uma semana na cidade mais famosa do mundo com hospedagem em Manhattan, city tour completo, ingresso para a Estátua da Liberdade e show na Broadway.",
                DataInicio = DateTime.Today.AddMonths(3),
                CapacidadeMaxima = 15,
                Preco = 12000.00m,
                Destinos = new List<Destino> { destinos[2] }
            },
            new()
            {
                Titulo = "Oriente Encantador",
                Descricao = "Descubra o Japão moderno e ancestral: templos em Kyoto, gastronomia premiada em Tóquio, cerimônia do chá e muito mais neste roteiro exclusivo de 10 dias.",
                DataInicio = DateTime.Today.AddMonths(4),
                CapacidadeMaxima = 12,
                Preco = 15800.00m,
                Destinos = new List<Destino> { destinos[4] }
            },
            new()
            {
                Titulo = "Caribe & México",
                Descricao = "Sol, mar e cultura maia: 8 dias em Cancún com all-inclusive, passeio às ruínas de Chichén Itzá, mergulho no cenote Ik Kil e noites animadas na zona hoteleira.",
                DataInicio = DateTime.Today.AddMonths(1),
                CapacidadeMaxima = 25,
                Preco = 6200.00m,
                Destinos = new List<Destino> { destinos[5] }
            },
            new()
            {
                Titulo = "Mistérios do Egito",
                Descricao = "Uma expedição única pelas maravilhas do mundo antigo: Pirâmides de Gizé, Vale dos Reis, cruzeiro pelo Nilo e visita ao museu egípcio com guia arqueólogo.",
                DataInicio = DateTime.Today.AddMonths(5),
                CapacidadeMaxima = 18,
                Preco = 9900.00m,
                Destinos = new List<Destino> { destinos[6] }
            },
            new()
            {
                Titulo = "Ibéria & Oceânia",
                Descricao = "Da melancolia do Fado lisboeta às ondas de Bondi Beach: dois destinos únicos em uma viagem de contrastes, com 5 dias em Lisboa e 7 dias em Sydney.",
                DataInicio = DateTime.Today.AddMonths(6),
                CapacidadeMaxima = 16,
                Preco = 18500.00m,
                Destinos = new List<Destino> { destinos[3], destinos[7] }
            },
        };
        context.PacotesTuristicos.AddRange(pacotes);

        context.SaveChanges();

        context.Reservas.AddRange(
            new Reserva
            {
                ClienteId = clientes[0].Id,
                PacoteTuristicoId = pacotes[0].Id,
                DataReserva = DateTime.Today,
                Quantidade = 2
            },
            new Reserva
            {
                ClienteId = clientes[1].Id,
                PacoteTuristicoId = pacotes[3].Id,
                DataReserva = DateTime.Today.AddDays(-3),
                Quantidade = 1
            });

        context.SaveChanges();
    }
}

namespace ConexaoSolidaria.Web.Modelos;

// Mesmos valores que a campanhas-api envia como texto no JSON (JsonStringEnumConverter em Api.Json).
public enum StatusCampanha { Ativa, Concluida, Cancelada }

public enum StatusDoacao { Pendente, Processada, Rejeitada }

// Perfis da claim "role" do JWT, para [Authorize(Roles = ...)] e para a Sessao.
public static class Perfis
{
    public const string GestorONG = "GestorONG";
    public const string Doador = "Doador";
}

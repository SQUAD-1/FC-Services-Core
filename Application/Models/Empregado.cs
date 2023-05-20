namespace Models;

public class Empregado
{
    public int Matricula { get; set; }
    public string Nome { get; set; }
    public string Funcao { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public int Resolutor { get; set; }
    public int Setor_idSetor { get; set; }
}
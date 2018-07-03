using System;


namespace Model
{
    public class Paciente : Base
    {
        public string   Nome            { get; set; }
        public string   Responsavel     { get; set; }
        public DateTime DataNascimento  { get; set; }
        public int      Sexo            { get; set; }
        public string   CartaoSus       { get; set; }
        public string[] Endereco        { get; set; }
        public string   Telefone        { get; set; }
    }
}
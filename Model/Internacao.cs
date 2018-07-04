using System;


namespace Model
{
    public class Internacao : Base
    {
        public int      Paciente    { get; set; }
        public string   Causa       { get; set; }
        public int      Leito       { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime DataSaida   { get; set; }
    }
}
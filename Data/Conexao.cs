using MySql.Data.MySqlClient;


namespace Data
{
    public abstract class Conexao
    {
        private static string SERVER   = "localhost";
        private static string USER_ID  = "root";
        private static string PASSWORD = "bUsufeC_U7eg";
        private static string DATABASE = "hospital_infantil";



        protected string ConnectionString
        {
            get { return "server=" + SERVER + ";user id=" + USER_ID + ";password=" + PASSWORD + ";database=" + DATABASE + ""; }
        }



        protected MySqlConnection   MyConnection    { get; set; }
        protected MySqlCommand      MyCommand       { get; set; }
        protected MySqlDataReader   MyReader        { get; set; }



        protected string RawSelect(string tabela, string[] colunas, string[] valores)
        {
            string query = "SELECT * FROM " + tabela + " ";

            if (colunas != null && valores != null)
            {
                if (colunas.Length == valores.Length)
                {
                    query += "WHERE ";

                    for (int i = 0; i < colunas.Length; i++)
                    {
                        if (colunas[i] != null && valores[i] != null)
                        {
                            query += (i < (colunas.Length - 1)) ? colunas[i] + "='" + valores[i] + "' AND " : colunas[i] + "='" + valores[i] + "'";
                        }
                    }
                }
            }

            return query;
        }
    }
}

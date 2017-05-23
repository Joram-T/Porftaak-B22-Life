namespace Proftaak_B22__Life
{
     public class Account
    {
        //AutoProperty generates private field for us
        public int ID { get; }
        public string Email { get; set; }
        public string Wachtwoord { get; set; }

        public Account(string email, string wachtwoord)
        {
            this.Email = email;
            this.Wachtwoord = wachtwoord;
        }

        public Account(int id, string email, string wachtwoord)
        {
            this.ID = id;
            this.Email = email;
            this.Wachtwoord = wachtwoord;
        }
    }
}
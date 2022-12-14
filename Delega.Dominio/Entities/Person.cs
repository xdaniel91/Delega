namespace Delega.Dominio.Entities
{
    public sealed class Person : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cpf { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age {
            get {
                var age = DateTime.Today.Year - BirthDate.Year;
                if (BirthDate < DateTime.Today)
                    age--;

                return age;
            }
        }
        public Address Address { get; set; }
        public long AddressId { get; set; }


        public Person()
        {
            //empty constructor for entityframework.
        }

        public Person(string firstname, string lastname, string cpf, DateTime birthdate)
        {
            FirstName = firstname;
            LastName = lastname;
            Cpf = cpf;
            BirthDate = birthdate;
        }
    }
}

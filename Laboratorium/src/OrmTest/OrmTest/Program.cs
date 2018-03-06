using OrmTest.Model;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;

namespace OrmTest
{
    class Program
    {
        public static string ConnectionString { get; set; } = "C:/Temp/sqlite.db";

        static void Main(string[] args)
        {
            /*
             * Tworzymy obiekt pomocniczy do otwierania połączeń do BD
             */
            var dbFactory = new OrmLiteConnectionFactory(ConnectionString, SqliteDialect.Provider);

            /*
             * Otwieramy połączenie do bazy danych - najbardziej istotny fragment kodu
             */
            using (var db = dbFactory.Open())
            {
                /*
                 * Tworzymy tabele, jeśli nie istnieją.
                 * Istotne jest to gdzie ten fragment znajduje się w produkcyjnym kodzie
                 */
                db.CreateTableIfNotExists<Address>();
                db.CreateTableIfNotExists<Person>();

                /*
                 * Instancjonujemy obiekty dla adresu oraz osób
                 */
                Address addressOne = new Address() { Text = "Słoneczny Rzeszów" };
                Address addressTwo = new Address() { Text = "Boguchwała" };

                Person personOne = new Person() { Name = "Maciej", Surname = "Penar", Age = 17, Address = addressOne };
                Person personTwo = new Person() { Name = "Wanda", Surname = "Kowalska", Age = 20, Address = addressOne };
                Person personThree = new Person() { Name = "Róża", Surname = "Wesoła", Age = 21, Address = addressTwo };

                /*
                 * Wykonamy insert,
                 * Zwrócić uwagę na powtarzający się kod
                 */
                db.SaveAllReferences(personOne);
                db.Save(personOne); // db.Insert(personOne); też istnieje, ale nie aktualizuje w programie Id nadanego przez Bazę Danych

                db.SaveAllReferences(personTwo);
                db.Save(personTwo);

                db.SaveAllReferences(personThree);
                db.Save(personThree); 
                

                /*
                 * Wykonamy update
                 */
                addressOne.Text = "Jednak nie słoneczny rzeszów";
                db.Update(addressOne);

                /*
                 * Usuwamy dane
                 */
                db.Delete(personTwo);

                /*
                 * Rożne formy odczytu
                 */
                Person resultOne = db.SingleById<Person>(1); // Punktowy
                List<Person> resultTwo = db.Select<Person>("SELECT Id, Name FROM Persons WHERE Age > 20"); // Przez SQL
                SqlExpression<Person> resultThree = db.From<Person>().Where("Age > 20").Select(); // Mapowanie zapytań

                /*
                 * Wydrukowanie zapytania na konsole
                 */
                Console.WriteLine(resultThree.ToSelectStatement().ToString());

            }
        }
    }
}

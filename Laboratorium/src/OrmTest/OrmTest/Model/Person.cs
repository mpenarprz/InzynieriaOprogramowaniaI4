using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmTest.Model
{
    /*
     * Możemy nadać nazwę tabeli za pomocą atrybutu [Alias]
     * Jak zabraknie tego atrybutu to zostanie użyta nazwa klasy
     */
    [Alias("Persons")] 
    public class Person
    {
        /*
         * Konwencja w ORMLite jest to że pola o nazwie Id są kluczami głównymi
         * Wykorzytujemy atrybut [AutoIncrement] żeby baza danych sama nadawała identyfikatory kolejnymi liczbami
         */
        [AutoIncrement]
        public int Id { get; set; }

        /*
         * Atrybut [Required] powoduje że niedopuszalna jest wartość pusta
         */
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        /*
         * Atrybut [Default] powoduje że można nadać wartość domyślną w przypadku użycia (omyłkowo) pustej wartości
         * Atrybut [CheckConstraint] umożliwia zdefiniowania dodatkowych ograniczeń (więzów typu CHECK)
         */
        [Default(-1)]
        [CheckConstraint("Age >= -1")]
        public int Age { get; set; }

        /*
         * Atrybut [Reference] umożliwia wskazanie klucza obcego na inny byt
         */
        [Reference]
        public Address Address { get; set; }
        public int AddressId { get; set; }

    }
}

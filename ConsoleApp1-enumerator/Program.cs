//https://msdn.microsoft.com/en-us/library/system.collections.ienumerator(v=vs.110).aspx

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1_enumerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Person[] peopleArray = new Person[3]
            {
            new Person("John", "Smith"),
            new Person("Jim", "Johnson"),
            new Person("Sue", "Rabon"),
            };

            //COMMENT byDR: ITERATING THROUGH INTERNAL ARRAY / COLLECTION OF THE CLASS
            Console.WriteLine("DISPLAYING INTERNAL COLLECTION OF A CLASS, USING Enumerator");
            People peopleList = new People(peopleArray);
            foreach (Person p in peopleList)
                Console.WriteLine(p.firstName + " " + p.lastName);

            Console.WriteLine("DISPLAYING ARRAY ELEMENTS, USING ARRAY (DEFAULT) ITERATOR");
            //modified / added byDR, ITERATING THROUGH ARRAY OUTSIDE CLASS
            foreach (Person p in peopleArray)
                Console.WriteLine(p.firstName + " " + p.lastName);

        }
    }
    // Simple business object.
    public class Person
    {
        public Person(string fName, string lName)
        {
            this.firstName = fName;
            this.lastName = lName;
        }

        public string firstName;
        public string lastName;
    }
    // Collection of Person objects. This class
    // implements IEnumerable so that it can be used
    // with ForEach syntax.
    public class People //: IEnumerable                     //modified byDR, NOT NEEDED
    {
        private Person[] _people;
        public People(Person[] pArray)
        {
            _people = new Person[pArray.Length];

            for (int i = 0; i < pArray.Length; i++)
            {
                _people[i] = pArray[i];
            }
        }
        // Implementation for the GetEnumerator method.     //modified byDR, NOT NEEDED
        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return (IEnumerator)GetEnumerator();
        //}

        public PeopleEnum GetEnumerator()
        {
            return new PeopleEnum(_people);
        }
    }
    // When you implement IEnumerable, you must also implement IEnumerator.
    public class PeopleEnum : IEnumerator
    {
        public Person[] _people;

        // Enumerators are positioned before the first element
        // until the first MoveNext() call.
        int position = -1;

        public PeopleEnum(Person[] list)
        {
            _people = list;
        }
        public void Reset()
        {
            position = -1;
            //throw new NotSupportedException("Reset function not realized.");
            ////throw new ArgumentNullException("Parameter 'id' cannot be empty.");
        }
        public bool MoveNext()
        {
            position++;
            return (position < _people.Length);
        }
        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }
        public Person Current
        {
            get
            {
                try
                {
                    return _people[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
//John Smith
//Jim Johnson
//Sue Rabon
//Press any key to continue . . .
/* This code produces output similar to the following:
 *
 * John Smith
 * Jim Johnson
 * Sue Rabon
 *
 */

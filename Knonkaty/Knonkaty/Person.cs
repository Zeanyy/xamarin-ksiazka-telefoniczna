using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Knonkaty
{
    [Table("Person")]
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public string state { get; set; }
        public int number { get; set; }
    }
}

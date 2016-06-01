using System.Collections.Generic;

namespace LuceneNetSample
{
    public class MyModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        internal static IList<MyModel> Load()
        {
            return new List<MyModel>
            {
                new MyModel { Id = 0, Age = 33, Name = "Felipe Dutra Tine e Silva"},
                new MyModel { Id = 1, Age = 34, Name = "Logan"},
                new MyModel { Id = 2, Age = 35, Name = "Peter Parker"},
                new MyModel { Id = 3, Age = 36, Name = "En Sabah Nur"},
                new MyModel { Id = 4, Age = 37, Name = "Eric Magnus"},
                new MyModel { Id = 5, Age = 38, Name = "Dr Von Doom"},
                new MyModel { Id = 6, Age = 39, Name = "Stephen Strange"},
                new MyModel { Id = 7, Age = 40, Name = "Jean Grey"},
                new MyModel { Id = 8, Age = 41, Name = "Malicia"},
                new MyModel { Id = 9, Age = 42, Name = "Norrin Radd"},
            };
        }

        public class Mapping
        {
            public const string Id = "Id";
            public const string Name = "Name";
            public const string Age = "Age";

        }

    }
}

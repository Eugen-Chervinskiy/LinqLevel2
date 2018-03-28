using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqLevel2
{
   class Program
   {
      static void Main(string[] args)
      {
         var data = new List<object>() {
                  "Hello",
                  new Book() { Author = "Terry Pratchett", Name = "Guards! Guards!", Pages = 810 },

                  new List<int>() {4, 6, 8, 2},

                  new Book() { Author = "Chak Palanik", Name="Fight Club", Pages = 300},

                  new string[] {"Hello inside array"},

                  new Film() { Author = "Martin Scorsese", Name= "The Departed", Actors = new List<Actor>() {
                     new Actor() { Name = "Jack Nickolson", Birthdate = new DateTime(1937, 4, 22)},
                     new Actor() { Name = "Leonardo DiCaprio", Birthdate = new DateTime(1974, 11, 11)},
                     new Actor() { Name = "Matt Damon", Birthdate = new DateTime(1970, 8, 10)}
                  }},

                  new Book() { Author = "Clive Barker", Name="Books of Blood", Pages = 500},

                  new Film() { Author = "Gus Van Sant", Name = "Good Will Hunting", Actors = new List<Actor>() {
                     new Actor() { Name = "Matt Damon", Birthdate = new DateTime(1970, 8, 10)},
                     new Actor() { Name = "Robin Williams", Birthdate = new DateTime(1951, 8, 11)},
                  }},
                  new Book() { Author = "Chak Palanik", Name="Fight", Pages = 350},
                  new Book() { Author = "Stephen King", Name="Finders Keepers", Pages = 200},
                  "Leonardo DiCaprio"
                  };





         //1.Output all elements excepting ArtObjects
         var query1 = data
                     .Where(i => i is ArtObject)
                     .ToList();


         //2.Output all actors names
         var films = data
                     .Where(i => i is Film)
                     .SelectMany(n => (n as Film).Actors)
                     .Select(i => i.Name)
                     .Distinct()
                     .ToList();



         //3. Output number of actors born in august
         var born = data
                    .Where(i => i is Film)
                    .SelectMany(n => (n as Film).Actors)
                    .Where(i => i.Birthdate.Month == 8)
                    .Select(i => i.Name)
                    .Distinct()
                    .Count();



         //4. Output two oldest actors names
         var oldest = data
                     .Where(i => i is Film)
                     .SelectMany(n => (n as Film).Actors)
                     .OrderBy(i => i.Birthdate)
                     .Take(2)
                     .Select(i => i.Name)
                     .ToList();



         //5.Output number of books per authors
         var book = data
            .Where(i => i is Book)
            .GroupBy(i => (i as Book).Author)
            .Select(i => new { Author = i.Key, Quantity = i.Count() })
            .ToList();


         //6.Output number of books per authors and films per director
         var bookAndFilms = data
                           .Where(i => i is ArtObject)
                           .GroupBy(i => (i as ArtObject).Author)
                           .Select(i => new { Author = i.Key, Quantity = i.Count() })
                           .ToList();


         //7.Output how many different letters used in all actors names
         var differ = data
                     .Where(i => i is Film)
                     .SelectMany(n => (n as Film).Actors)
                     .SelectMany(i => i.Name)
                     .Distinct()
                     .Where(i => !i.Equals(' '))
                     .Count();


         //8.Output names of all books ordered by names of their authors and number of pages
         var names = data
                     .Where(i => i is Book)
                     .OrderBy(i => (i as Book).Author)
                     .ThenBy(i => (i as Book).Pages)
                     .Select(i => $"Author: {(i as Book).Author}, " +
                                  $"Pages: {(i as Book).Pages}, " +
                                  $"Books Name: {(i as Book).Name}")
                     .ToList();


         //11.Get the dictionary with the key - book author, value - list of author's books
         var dictionaryBooks = data.Where(_ => _ is Book)
                                                  .Select(_ => (_ as Book))
                                                  .GroupBy(_ => _.Author)
                                                  .Select(_ => new
                                                  {
                                                     Author = _.Key,
                                                     Count = _.Count(),
                                                     Books = _.Select(x => x.Name).ToList()
                                                  })
                                                  .Select(_ => $"Author: {_.Author}, Books: {string.Join(",", _.Books)}")
                                                  .ToList();




      }
   }
}

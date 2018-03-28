using System.Collections.Generic;

namespace LinqLevel2
{
   public class Film : ArtObject
   {
      public int Length { get; set; }
      public IEnumerable<Actor> Actors { get; set; }
   }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models.EF;

namespace ConsoleShowCase
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new xLingua_StagingEntities())
            {
                var basewords = from b in context.Basewords1
                                where b.WordtypeId == 1 && b.LanguageId == 1
                                select b;

                foreach (var baseword in basewords)
                {
                    Console.Out.WriteLine(baseword.Text);
                }

                Console.ReadKey();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANIFESTA
{
    internal class EntryPoint
    {
        public static async Task Main(string[] args)
        {
            var program = new Program();
            await program.Run(args);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBuilder
{
    public interface IBuildService
    {
        bool Build(string solutionDirectory, string outputPath);
    }
}

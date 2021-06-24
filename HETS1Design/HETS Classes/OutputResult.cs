using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HETS1Design
{
    public class OutputResult
    {
        public string GetResultOutput { get; private set; }
        public bool DidItMatch { get; private set; }

        public OutputResult(string resultOutput)
        {
            this.GetResultOutput = resultOutput;
            DidItMatch = false; //We didn't choose whether it matches or not yet.
        }

        public void Match()
        {            
                this.DidItMatch = true;            
        }

        public void Mismatch()
        {
                this.DidItMatch = false;
        }

    }
}

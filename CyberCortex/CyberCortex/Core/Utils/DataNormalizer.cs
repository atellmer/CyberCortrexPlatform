using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CyberCortex.Core.AI;

namespace CyberCortex.Core.Utils
{
    public static class DataNormalizer
    {
        public static Sample[] NormalizeSamples(Sample[] samples)
        {
            Sample[] samplesNormalized = new Sample[samples.Length];

            for (int i = 0; i < samples.Length; i++)
            {
                samplesNormalized[i] = Sample.Normalize(samples[i]);
            }

            return samplesNormalized;
        }
    }
}

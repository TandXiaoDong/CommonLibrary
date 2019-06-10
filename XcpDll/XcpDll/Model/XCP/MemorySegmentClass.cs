using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisAgreeMent.Model.XCP
{
    public class MemorySegmentClass
    {
        public UInt32 startAddress;
        public UInt32 offset;
        public byte segmentNumber;
        public byte pageCount;
        public byte readPageNumber;
        public byte writePageNumber;
    }
}

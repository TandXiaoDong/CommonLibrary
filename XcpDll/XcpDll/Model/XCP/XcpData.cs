using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisAgreeMent.Model.XCP
{
    /// <summary>
    /// 存储解析数据
    /// </summary>
    public class XcpData
    {
        public List<MeasureMent> MeasureData { get; set; }

        public List<Characteristic> CharacterData { get; set; }

        public List<CompuMethod> MetholdData { get; set; }

        public List<TableClass> TableData { get; set; }

        public List<RecordLayoutClass> RecordData { get; set; }

        public List<MemorySegmentClass> MemorySedData { get; set; }

        public List<PropertyClass> PropertyData { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalysisAgreeMent.Model;

namespace AnalysisAgreeMent.Analysis
{
    public class TypeConvert
    {
        public static SaveDataTypeEnum AnalysisTypeToSaveType(DataTypeEnum type)
        {
            switch (type)
            {
                case DataTypeEnum.UBYTE:
                    return SaveDataTypeEnum.V_UNIT;
                case DataTypeEnum.SBYTE:
                    return SaveDataTypeEnum.V_INT;
                case DataTypeEnum.UWORD:
                    return SaveDataTypeEnum.V_UNIT;
                case DataTypeEnum.SWORD:
                    return SaveDataTypeEnum.V_INT;
                case DataTypeEnum.ULONG:
                    return SaveDataTypeEnum.V_UNIT;
                case DataTypeEnum.SLONG:
                    return SaveDataTypeEnum.V_INT;
                case DataTypeEnum.FLOAT32_IEEE:
                    return SaveDataTypeEnum.V_FL4;
                default:
                    return SaveDataTypeEnum.V_NULL;
            }
        }

        public static int AnalysisTypeToLength(DataTypeEnum type)
        {
            switch (type)
            {
                case DataTypeEnum.UBYTE:
                    return 1;
                case DataTypeEnum.SBYTE:
                    return 1;
                case DataTypeEnum.UWORD:
                    return 2;
                case DataTypeEnum.SWORD:
                    return 2;
                case DataTypeEnum.ULONG:
                    return 4;
                case DataTypeEnum.SLONG:
                    return 4;
                case DataTypeEnum.FLOAT32_IEEE:
                    return 4;
                default:
                    return 0;
            }
        }
    }
}

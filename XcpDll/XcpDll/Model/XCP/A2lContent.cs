using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisAgreeMent.Model.XCP
{
    public static class A2lContent
    {
        public static class BeginmodCommon
        {
            public const string BEGINMOD_COMMON = "/begin MOD_COMMON";
            public const string END_MOD_COMMON = "/end MOD_COMMON";
            public const string BYTE_ORDER = "byte_order";
            public const string MSB_LAST = "msb_last";
            public const string MSB_FIRST = "msb_first";
        }
        public static class BeginXcpOnCan
        {
            public const string BEGINXCP_ON_CAN = "/begin xcp_on_can";
            public const string ENDXCP_ON_CAN = "/endx cp_on_can";
            public const string CAN_ID_MASTEROX = "can_id_master0x";
            public const string CAN_ID_SLAVEOX = "can_id_slave0x";
            public const string BAUDRATE = "baudrate";
            public const string BAUDRATE_HZ = "/*baudrate[hz]*/";
            public const string SEG_SYNC = "seg_sync";
            public const string EVENT_CHANNEL_SHORT_NAME = "event_channel_short_name";
            public const string EVENT_CHANNEL_NUMBER = "/*event_channel_number*/";
            public const string MSECOND_10 = "10_ms";
            public const string TRANSPORT_LAYER_INSTANCE = "transport_layer_instance";
            public const string CALIBRATIONCAN_APP1 = "calibrationcan(appl)";
            public const string CALIBRATIONCAN_LE = "calibrationcan(le)";
            public const string CALIBRATIONCAN_APP1_ED_RAM = "calibrationcan(appl)(ed-ram)";
            public const string VEHICECAN_PT = "vehiclecan(pt)";
            public const string VEHICLECAN_APP1 = "vehiclecan(appl)";
            public const string VEHICLECAN_PT_ED_RAM = "vehiclecan(pt)(ed-ram)";
        }

        public static class BeginMeasurement
        {
            public const string BEGIN_MEASUREMENT = "/begin measurement";
            public const string END_MEASUREMENT = "/end measurement";
            public const string UBYTE = "ubyte";
            public const string SBYTE = "sbyte";
            public const string UWORD = "uword";
            public const string SWORD = "sword";
            public const string ULONG = "ulong";
            public const string SLONG = "slong";
            public const string FLOAT32_IEEE = "float32_ieee";
            public const string BIT_MASKOX = "bit_mask";
            public const string ECU_ADDRESSOX = "ecu_address0x";

        }

        public static class BegincompuMedthod
        {
            public const string BEGINCOMPU_MEDTHOD = "/begin compu_method";
            public const string ENDCOMPU_MEDTHOD = "/end compu_method";
            public const string TAB_VERB = "tab_verb";
            public const string RAT_FUNC = "rat_func";
            public const string COEFFS = "coeffs";
        }

        public static class BeginCharacteristic
        {
            public const string BEGIN_CHARACTERISTIC = "/begin characteristic";
            public const string END_CHARACTERISTIC = "/end characteristic";
            public const string VALUE = "value";
            public const string VAL_BLK = "val_blk";
            public const string ASCII = "ascii";
            public const string CURVE = "curve";
            public const string MAP = "map";
            public const string NUMBER = "number";
            public const string BEGINAXIX_DESCR = "/beginaxis_descr";
            public const string ENDAXIS_DESCR = "/endaxis_descr";
            public const string _AXIS = "_axis";
        }

        public static class BegincompuVtab
        {
            public const string BEGIN_COMPU_VTAB = "/begin compu_vtab";
            public const string END_COMPU_VTAB = "/end compu_vtab";
            public const string TAB_VERB = "tab_verb";
        }
    }
}

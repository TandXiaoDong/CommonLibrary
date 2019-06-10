using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using AnalysisAgreeMent.Model;
using CommonUtils.Logger;
using CommonUtils.ByteHelper;
using System.Diagnostics;
using AnalysisAgreeMent.Model.XCP;
using AnalysisAgreeMent.Analysis;

namespace AnalysisAgreeMent
{
    /// <summary>
    /// XCP协议解析，支持CCP协议
    /// </summary>
    public class XcpHelper
    {
        #region 私有成员变量
        /// <summary>
        /// 存储所有解析数据对象
        /// </summary>
        private XcpData xcpData;

        private static FileStream A2lFile;
        private static StreamReader A2lReader;
        private static string readLineResult;
        private static bool isVaild = false;
        private static int index = 0;
        private static int dimensionIndex = 0;
        private static int XY_index = 0;
        private static MeasureMent measureMent;
        private static CompuMethod compuMethod;
        private static Characteristic characterInformation;
        private static TableClass TableInformation;
        private static RecordLayoutClass RecordInformation;
        private static MemorySegmentClass MemoryInformation;
        private static PropertyClass propertyInformation;

        private static List<MeasureMent> measureList = new List<MeasureMent>();
        private static List<Characteristic> characterList = new List<Characteristic>();
        private static List<CompuMethod> MetholdList = new List<CompuMethod>();
        private static List<TableClass> TableList = new List<TableClass>();
        private static List<RecordLayoutClass> RecordList = new List<RecordLayoutClass>();
        private static List<MemorySegmentClass> MemoryList = new List<MemorySegmentClass>();
        private static List<PropertyClass> propertyList = new List<PropertyClass>();
        #endregion

        #region 公有成员变量
        
        #endregion

        public XcpHelper(XcpData data)
        {
            this.xcpData = data;
        }

        #region 读取文件，解析a2l文件
        /// <summary>
        /// 解析a2l文件，解析成功返回值1
        /// </summary>
        /// <param name="A2lPath">a2l绝对路径</param>
        /// <param name="Protocol">协议类型：1/2=calibrationcan；3/4=vehiclecan</param>
        /// <returns></returns>
        public CodeCommand AnalyzeXcpFile(string A2lPath, int Protocol)
        {
            try
            {
                Init();
                if (!File.Exists(A2lPath))
                {
                    LogHelper.Log.Error("文件不存在！"+A2lPath);
                    return CodeCommand.NON_EXIST_FILE;
                }
                A2lFile = new FileStream(A2lPath, FileMode.Open);
                A2lReader = new StreamReader(A2lFile);

                #region read file
                while (!A2lReader.EndOfStream)
                {
                    readLineResult = A2lReader.ReadLine().Trim();
                    string signTemp = readLineResult.ToLower();
                    if (signTemp.Contains(A2lContent.BeginmodCommon.BEGINMOD_COMMON.ToLower()))
                    {
                        signTemp = A2lContent.BeginmodCommon.BEGINMOD_COMMON;
                    }
                    switch (signTemp)
                    {
                        case A2lContent.BeginmodCommon.BEGINMOD_COMMON:
                            //解析字节顺序
                            AnalysisModCommon();
                            break;
                        case A2lContent.BeginXcpOnCan.BEGINXCP_ON_CAN:
                            //AnalysisXcpOnCan(Protocol);
                            break;
                        case A2lContent.BeginMeasurement.BEGIN_MEASUREMENT:
                            //测量值
                            AnalysisMeasureMent();
                            break;

                        case A2lContent.BeginCharacteristic.BEGIN_CHARACTERISTIC:
                            //AnalysisCharactoritics();
                            break;

                        case A2lContent.BegincompuMedthod.BEGINCOMPU_MEDTHOD:
                            //计算方法
                            AnalysisCompu_Methold();
                            break;
                        case A2lContent.BegincompuVtab.BEGIN_COMPU_VTAB:
                            //AnalysisCompu_Vtab();
                            break;

                        default:
                            //AnalysisOtherType();
                            break;
                    }
                }
                xcpData.PropertyData = propertyList;
                xcpData.MeasureData = measureList;
                xcpData.MetholdData = MetholdList;
                LogHelper.Log.Info("解析完成："+propertyList.Count+"  "+measureList.Count+"  "+MetholdList.Count);
                #endregion

                CheckAgreementType();
                //ReadListMethod();
                //WriteListMethod();
                FileClose();
                return CodeCommand.RESULT;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex.Message+"\r\n"+ex.StackTrace);
                return 0;
            }
        }
        #endregion

        private void AnalysisModCommon()
        {
            index = 0;
            propertyInformation = new PropertyClass();
            while (readLineResult.ToLower() != A2lContent.BeginmodCommon.END_MOD_COMMON.ToLower())
            {
                readLineResult = A2lReader.ReadLine().Trim();
                switch (index)
                {
                    case (int)XcpFormat.XcpMod_Common.BYTE_ORDER:
                        if (readLineResult.ToLower().Contains(A2lContent.BeginmodCommon.MSB_LAST))
                        {
                            propertyInformation.byteOrder = ByteOrder.BYTE_ORDER_MSB_LAST;
                        }
                        else if (readLineResult.ToLower().Contains(A2lContent.BeginmodCommon.MSB_FIRST))
                        {
                            propertyInformation.byteOrder = ByteOrder.BYTE_ORDER_MSB_FIRST;
                        }
                        break;
                    case (int)XcpFormat.XcpMod_Common.ALIGNMENT_BYTE:
                        break;
                    case (int)XcpFormat.XcpMod_Common.ALIGNMENT_WORD:
                        break;
                    case (int)XcpFormat.XcpMod_Common.ALIGNMENT_LONG:
                        break;
                    case (int)XcpFormat.XcpMod_Common.ALIGNMENT_FLOAT32_IEEE:
                        break;
                }
                propertyList.Add(propertyInformation);
                index++;
            }
        }

        private CodeCommand AnalysisXcpOnCan(int Protocol)
        {
            string SendID = "";
            string ReceiveID = "";
            string Baudrate = "";
            string EventChannel_sync = "";
            string EventChannel_10 = "";
            string EventChannel_100 = "";
            if (isVaild == false)
            {
                while (readLineResult.ToLower() != A2lContent.BeginXcpOnCan.ENDXCP_ON_CAN)
                {
                    readLineResult = A2lReader.ReadLine();
                    if (readLineResult.ToLower().Contains(A2lContent.BeginXcpOnCan.CAN_ID_MASTEROX))
                    {
                        readLineResult = readLineResult.Replace(A2lContent.BeginXcpOnCan.CAN_ID_MASTEROX, "");
                        int position = readLineResult.IndexOf("/");
                        SendID = readLineResult.Substring(0, position);
                    }
                    else if (readLineResult.Contains(A2lContent.BeginXcpOnCan.CAN_ID_SLAVEOX))
                    {
                        readLineResult = readLineResult.Replace(A2lContent.BeginXcpOnCan.CAN_ID_SLAVEOX, "");
                        int position = readLineResult.IndexOf("/");
                        ReceiveID = readLineResult.Substring(0, position);
                    }
                    else if (readLineResult.Contains(A2lContent.BeginXcpOnCan.BAUDRATE) && (readLineResult.Contains(A2lContent.BeginXcpOnCan.BAUDRATE_HZ)) && (!readLineResult.Contains("_")))
                    {
                        readLineResult = readLineResult.Replace(A2lContent.BeginXcpOnCan.BAUDRATE, "");
                        int position = readLineResult.IndexOf("/");
                        Baudrate = readLineResult.Substring(0, position);
                    }
                    else if (readLineResult.Contains(A2lContent.BeginXcpOnCan.SEG_SYNC) && readLineResult.Contains(A2lContent.BeginXcpOnCan.EVENT_CHANNEL_SHORT_NAME))
                    {
                        readLineResult = A2lReader.ReadLine();
                        readLineResult = readLineResult.ToLower().Replace(" ", "");
                        if (readLineResult.Contains(A2lContent.BeginXcpOnCan.EVENT_CHANNEL_NUMBER))
                        {
                            EventChannel_sync = readLineResult.Substring(0, 1);
                        }

                    }
                    else if (readLineResult.Contains(A2lContent.BeginXcpOnCan.MSECOND_10) && readLineResult.Contains(A2lContent.BeginXcpOnCan.EVENT_CHANNEL_SHORT_NAME))
                    {
                        readLineResult = A2lReader.ReadLine();
                        readLineResult = readLineResult.ToLower().Replace(" ", "");
                        if (readLineResult.Contains(A2lContent.BeginXcpOnCan.EVENT_CHANNEL_NUMBER))
                        {
                            EventChannel_10 = readLineResult.Substring(0, 1);
                        }
                    }
                    else if (readLineResult.Contains(A2lContent.BeginXcpOnCan.MSECOND_10) && readLineResult.Contains(A2lContent.BeginXcpOnCan.EVENT_CHANNEL_SHORT_NAME))
                    {
                        readLineResult = A2lReader.ReadLine();
                        readLineResult = readLineResult.ToLower().Replace(" ", "");
                        if (readLineResult.Contains(A2lContent.BeginXcpOnCan.EVENT_CHANNEL_NUMBER))
                        {
                            EventChannel_100 = readLineResult.Substring(0, 1);
                        }

                    }
                    else if (readLineResult.Contains(A2lContent.BeginXcpOnCan.TRANSPORT_LAYER_INSTANCE))
                    {
                        if (Protocol == 1)
                        {
                            if ((readLineResult.Contains(A2lContent.BeginXcpOnCan.CALIBRATIONCAN_APP1)) || (readLineResult.Contains(A2lContent.BeginXcpOnCan.CALIBRATIONCAN_LE)))
                            {
                                isVaild = true;
                            }
                        }
                        else if (Protocol == 2)
                        {
                            if ((readLineResult.Contains(A2lContent.BeginXcpOnCan.CALIBRATIONCAN_APP1_ED_RAM)))
                            {
                                isVaild = true;
                            }
                        }
                        else if (Protocol == 3)
                        {
                            if ((readLineResult.Contains(A2lContent.BeginXcpOnCan.VEHICECAN_PT)) || (readLineResult.Contains(A2lContent.BeginXcpOnCan.VEHICLECAN_APP1)))
                            {
                                isVaild = true;
                            }
                        }
                        else if (Protocol == 4)
                        {
                            if ((readLineResult.Contains(A2lContent.BeginXcpOnCan.VEHICLECAN_PT_ED_RAM)))
                            {
                                isVaild = true;
                            }
                        }
                        else
                        {
                            FileClose();
                            return CodeCommand.TRANSPORT_LAYER_INSTANCE;
                        }
                    }
                }
                if ((isVaild == true) && (SendID != "") && (ReceiveID != "") && (Baudrate != "") && (EventChannel_sync != "") && (EventChannel_10 != "") && (EventChannel_100 != ""))
                {
                    if (!UInt32.TryParse(Baudrate, out propertyInformation.baudrate))
                    {
                        FileClose();
                        return CodeCommand.XCP_ON_CAN_BAUDRATE;
                    }
                    if (!UInt16.TryParse(EventChannel_sync, out propertyInformation.eventChannel_sync))
                    {
                        FileClose();
                        return CodeCommand.XCP_ON_CAN_SYNC;
                    }
                    if (!UInt16.TryParse(EventChannel_10, out propertyInformation.eventChannel_10))
                    {
                        FileClose();
                        return CodeCommand.XCP_ON_CAN_10;
                    }
                    if (!UInt16.TryParse(EventChannel_100, out propertyInformation.eventChannel_100))
                    {
                        FileClose();
                        return CodeCommand.XCP_ON_CAN_100;
                    }
                    try
                    {
                        propertyInformation.MasterCANID = Convert.ToUInt32(SendID, 16);
                        propertyInformation.SlaverCANID = Convert.ToUInt32(ReceiveID, 16);
                    }
                    catch
                    {
                        FileClose();
                        return CodeCommand.XCP_ON_CAN_SEND_REV_ID;
                    }
                }
            }
            return CodeCommand.XCP_ON_CAN_SUCCESS;
        }

        private void AnalysisMeasureMent()
        {
            index = 0;
            measureMent = new MeasureMent();
            while (readLineResult.ToLower() != A2lContent.BeginMeasurement.END_MEASUREMENT)
            {
                readLineResult = A2lReader.ReadLine().Trim();
                switch (index)
                {
                    case (int)XcpFormat.XcpMeasureFormat.NAME:
                        measureMent.Name = readLineResult;
                        break;
                    case (int)XcpFormat.XcpMeasureFormat.DESCRIBLE:
                        measureMent.Describle = readLineResult;
                        break;
                    case (int)XcpFormat.XcpMeasureFormat.TYPE:
                        measureMent.Type = readLineResult.ToLower();
                        break;
                    case (int)XcpFormat.XcpMeasureFormat.REFERENCE_METHOD:
                        measureMent.ReferenceMethod = readLineResult;
                        break;
                    case (int)XcpFormat.XcpMeasureFormat.RESOLUTION_IN_BITS:
                        break;
                    case (int)XcpFormat.XcpMeasureFormat.ACCURACY:
                        break;
                    case (int)XcpFormat.XcpMeasureFormat.LOWER_LIMIT:
                        break;
                    case (int)XcpFormat.XcpMeasureFormat.UPPER_LIMIT:
                        break;
                    case (int)XcpFormat.XcpMeasureFormat.BIT_MASK:
                        break;
                    case (int)XcpFormat.XcpMeasureFormat.FORMAT_11:
                        break;
                    case (int)XcpFormat.XcpMeasureFormat.ECU_ADDRESS:
                        measureMent.EcuAddress = int.Parse(ConvertString.ConvertToDec(readLineResult.ToLower().Replace("ecu_address", "")));
                        break;
                }
                measureList.Add(measureMent);
                index++;
            }
        }

        private CodeCommand AnalysisCharactoritics()
        {
            index = 0;
            dimensionIndex = 0;
            XY_index = 0;
            characterInformation = new Characteristic();
            while (readLineResult.ToLower() != A2lContent.BeginCharacteristic.END_CHARACTERISTIC)
            {
                readLineResult = A2lReader.ReadLine();
                if ((readLineResult != "") && (index == 0))
                {
                    characterInformation.name = readLineResult;
                    index++;
                }
                else if ((readLineResult.ToLower() == A2lContent.BeginCharacteristic.VALUE))
                {
                    characterInformation.dimension = 1;
                    characterInformation.dimensiontype = A2lContent.BeginCharacteristic.VALUE;
                    index++;
                }
                else if ((readLineResult.ToLower() == A2lContent.BeginCharacteristic.VAL_BLK))
                {
                    characterInformation.dimension = 1;
                    characterInformation.dimensiontype = A2lContent.BeginCharacteristic.VAL_BLK;
                    index++;
                }
                else if ((readLineResult.ToLower() == A2lContent.BeginCharacteristic.ASCII))
                {
                    characterInformation.dimension = 1;
                    characterInformation.dimensiontype = A2lContent.BeginCharacteristic.ASCII;
                    index++;
                }
                else if (readLineResult.ToLower() == A2lContent.BeginCharacteristic.CURVE)
                {
                    characterInformation.dimension = 2;
                    characterInformation.dimensiontype = A2lContent.BeginCharacteristic.CURVE;
                    index++;
                }
                else if (readLineResult.ToLower() == A2lContent.BeginCharacteristic.MAP)
                {
                    characterInformation.dimension = 3;
                    characterInformation.dimensiontype = A2lContent.BeginCharacteristic.MAP;
                    index++;
                }
                else if ((readLineResult != "") && (index == 2))
                {
                    if (readLineResult.ToLower().Contains("0x"))
                    {
                        readLineResult = readLineResult.ToLower().Replace("0x", "");
                        try
                        {
                            characterInformation.address = Convert.ToUInt32(readLineResult, 16);
                            index++;
                        }
                        catch
                        {
                            FileClose();
                            return CodeCommand.CHARACTERISTIC_ADDRESS;
                        }
                    }
                    else
                    {
                        FileClose();
                        return CodeCommand.CHARACTERISTIC_OX;
                    }
                }
                else if ((readLineResult != "") && (index == 3))
                {
                    characterInformation.recordLayout = readLineResult.ToLower();
                    index++;
                }
                else if ((readLineResult != "") && (index == 4))
                {
                    if (!float.TryParse(readLineResult, out characterInformation.V_range))
                    {
                        FileClose();
                        return CodeCommand.CHARACTERISTIC_V_RANGE;
                    }
                    index++;
                }
                else if ((readLineResult != "") && (index == 5))
                {
                    characterInformation.V_Expression = readLineResult.ToLower();
                    index++;
                }
                else if ((readLineResult != "") && (index == 6))
                {
                    if (!float.TryParse(readLineResult, out characterInformation.V_minValue))
                    {
                        FileClose();
                        return CodeCommand.CHARACTERISTIC_V_MIN_VALUE;
                    }
                    index++;
                }
                else if ((readLineResult != "") && (index == 7))
                {
                    if (!float.TryParse(readLineResult, out characterInformation.V_maxValue))
                    {
                        FileClose();
                        return CodeCommand.CHARACTERISTIC_V_MAX_VALUE;
                    }
                    index++;
                }
                else if (readLineResult.ToLower().Contains(A2lContent.BeginCharacteristic.NUMBER) && (index == 8))
                {
                    readLineResult = readLineResult.ToLower().Replace(A2lContent.BeginCharacteristic.NUMBER, "");
                    if (!UInt32.TryParse(readLineResult, out characterInformation.V_count))
                    {
                        FileClose();
                        return CodeCommand.CHARACTERISTIC_V_COUNT;
                    }
                    index++;
                }
                else if (readLineResult.ToLower() == A2lContent.BeginCharacteristic.BEGINAXIX_DESCR)
                {
                    XY_index = 0;
                    while (readLineResult.ToLower() != A2lContent.BeginCharacteristic.ENDAXIS_DESCR)
                    {
                        readLineResult = A2lReader.ReadLine();
                        readLineResult = readLineResult.Replace(" ", "");
                        if (dimensionIndex == 0)
                        {
                            if ((readLineResult != "") && (!readLineResult.ToLower().Contains(A2lContent.BeginCharacteristic._AXIS)) && (XY_index == 0))
                            {
                                characterInformation.X_name = readLineResult.ToLower();
                                XY_index++;
                            }
                            else if ((readLineResult != "") && (XY_index == 1))
                            {
                                characterInformation.X_Expression = readLineResult.ToLower();
                                XY_index++;
                            }
                            else if ((readLineResult != "") && (XY_index == 2))
                            {
                                if (!UInt32.TryParse(readLineResult, out characterInformation.X_count))
                                {
                                    FileClose();
                                    return CodeCommand.CHARACTERISTIC_X_COUNT;
                                }
                                XY_index++;
                            }
                            else if ((readLineResult != "") && (XY_index == 3))
                            {
                                if (!float.TryParse(readLineResult, out characterInformation.X_minValue))
                                {
                                    FileClose();
                                    return CodeCommand.CHARACTERISTIC_X_MIN_VALUE;
                                }
                                XY_index++;
                            }
                            else if ((readLineResult != "") && (XY_index == 4))
                            {
                                if (!float.TryParse(readLineResult, out characterInformation.X_maxValue))
                                {
                                    FileClose();
                                    return CodeCommand.CHARACTERISTIC_X_MAX_VALUE;
                                }
                                XY_index++;
                            }
                        }
                        else if (dimensionIndex == 1)
                        {
                            if ((readLineResult != "") && (!readLineResult.ToLower().Contains("_axis")) && (XY_index == 0))
                            {
                                characterInformation.Y_name = readLineResult.ToLower();
                                XY_index++;
                            }
                            else if ((readLineResult != "") && (XY_index == 1))
                            {
                                characterInformation.Y_Expression = readLineResult.ToLower();
                                XY_index++;
                            }
                            else if ((readLineResult != "") && (XY_index == 2))
                            {
                                if (!UInt32.TryParse(readLineResult, out characterInformation.Y_count))
                                {
                                    FileClose();
                                    return CodeCommand.CHARACTERISTIC_Y_COUNT;
                                }
                                XY_index++;
                            }
                            else if ((readLineResult != "") && (XY_index == 3))
                            {
                                if (!float.TryParse(readLineResult, out characterInformation.Y_minValue))
                                {
                                    FileClose();
                                    return CodeCommand.CHARACTERISTIC_Y_MIN_VALUE;
                                }
                                XY_index++;
                            }
                            else if ((readLineResult != "") && (XY_index == 4))
                            {
                                if (!float.TryParse(readLineResult, out characterInformation.Y_maxValue))
                                {
                                    FileClose();
                                    return CodeCommand.CHARACTERISTIC_Y_MAX_VALUE;
                                }
                                XY_index++;
                            }
                        }
                    }
                    dimensionIndex++;
                }
                else
                {
                    return CodeCommand.CHARACTERISTIC_FAIL;
                }
            }

            characterList.Add(characterInformation);
            xcpData.CharacterData = characterList;
            return CodeCommand.CHARACTERISTIC_SUCCESS;
        }

        private void AnalysisCompu_Methold()
        {
            index = 0;
            compuMethod = new CompuMethod();
            while (readLineResult.ToLower() != A2lContent.BegincompuMedthod.ENDCOMPU_MEDTHOD)
            {
                readLineResult = A2lReader.ReadLine().Trim();
                switch (index)
                {
                    case (int)XcpFormat.XcpCompu_Method.REFERENCE_METHOD:
                        compuMethod.name = readLineResult;
                        break;
                    case (int)XcpFormat.XcpCompu_Method.DESCRIBLE:
                        compuMethod.describle = readLineResult;
                        break;
                    case (int)XcpFormat.XcpCompu_Method.FUN_TYLE:
                        compuMethod.funType = readLineResult;
                        break;
                    case (int)XcpFormat.XcpCompu_Method.FORMAT_STRING:
                        compuMethod.format = readLineResult;
                        break;
                    case (int)XcpFormat.XcpCompu_Method.UNIT:
                        compuMethod.unit = readLineResult;
                        break;
                    case (int)XcpFormat.XcpCompu_Method.COEFFS:
                        if (compuMethod.funType == A2lContent.BegincompuMedthod.RAT_FUNC)
                        {
                            ///函数类型为rat_func时，显示值v1= b/f,v2 = c/f;
                            ///
                            compuMethod.coeffsValue = readLineResult.Replace(A2lContent.BegincompuMedthod.COEFFS, "");
                            string[] res = readLineResult.Replace(A2lContent.BegincompuMedthod.COEFFS, "").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            double v1 = double.Parse(res[1]) / double.Parse(res[5]);
                            double v2 = double.Parse(res[2]) / double.Parse(res[5]);
                            compuMethod.Factor = v1;
                            compuMethod.OffSet = v2;
                        }
                        else
                        {
                            compuMethod.Factor = 1;
                            compuMethod.OffSet = 0;
                        }
                        break;
                }
                MetholdList.Add(compuMethod);
                index++;
            }
        }

        private void AnalysisCompu_Vtab()
        {
            index = 0;
            TableInformation = new TableClass();
            while (readLineResult != A2lContent.BegincompuVtab.END_COMPU_VTAB)
            {
                readLineResult = A2lReader.ReadLine();
                if ((readLineResult != "") && (index == 0))
                {
                    TableInformation.name = readLineResult;
                    index++;
                }
                else if (readLineResult == A2lContent.BegincompuVtab.TAB_VERB)
                {
                    readLineResult = A2lReader.ReadLine();
                    int count = Convert.ToInt32(readLineResult, 10);
                    for (int index = 0; index < count; index++)
                    {
                        readLineResult = A2lReader.ReadLine();
                        TableInformation.table = string.Concat(TableInformation.table, readLineResult, "^&*");
                    }
                }
            }
            TableList.Add(TableInformation);
            xcpData.TableData = TableList;
        }

        private CodeCommand AnalysisOtherType()
        {
            if (readLineResult.Contains("/beginrecord_layout"))
            {
                index = 0;
                RecordInformation = new RecordLayoutClass();
                RecordInformation.name = readLineResult.Replace("/beginrecord_layout", "");
                while (readLineResult != "/endrecord_layout")
                {
                    readLineResult = A2lReader.ReadLine();
                    if (readLineResult.Contains("no_axis_pts_x"))
                    {
                        if (readLineResult.Contains("sbyte") || (readLineResult.Contains("ubyte")))
                        {
                            RecordInformation.X_countLength = 1;
                        }
                        else if (readLineResult.Contains("sword") || (readLineResult.Contains("uword")))
                        {
                            RecordInformation.X_countLength = 2;
                        }
                        else if (readLineResult.Contains("sword") || (readLineResult.Contains("uword")))
                        {
                            RecordInformation.X_countLength = 4;
                        }
                    }
                    else if (readLineResult.Contains("no_axis_pts_y"))
                    {
                        if (readLineResult.Contains("sbyte") || (readLineResult.Contains("ubyte")))
                        {
                            RecordInformation.Y_countLength = 1;
                        }
                        else if (readLineResult.Contains("sword") || (readLineResult.Contains("uword")))
                        {
                            RecordInformation.Y_countLength = 2;
                        }
                        else if (readLineResult.Contains("sword") || (readLineResult.Contains("uword")))
                        {
                            RecordInformation.Y_countLength = 4;
                        }
                    }
                    else if (readLineResult.Contains("axis_pts_x"))
                    {
                        if (readLineResult.Contains("sbyte"))
                        {
                            RecordInformation.X_elementLength = 1;
                            RecordInformation.X_elementType = "sbyte";
                        }
                        else if (readLineResult.Contains("ubyte"))
                        {
                            RecordInformation.X_elementLength = 1;
                            RecordInformation.X_elementType = "ubyte";
                        }
                        else if (readLineResult.Contains("sword"))
                        {
                            RecordInformation.X_elementLength = 2;
                            RecordInformation.X_elementType = "sword";
                        }

                        else if (readLineResult.Contains("uword"))
                        {
                            RecordInformation.X_elementLength = 2;
                            RecordInformation.X_elementType = "uword";
                        }

                        else if (readLineResult.Contains("slong"))
                        {
                            RecordInformation.X_elementLength = 4;
                            RecordInformation.X_elementType = "slong";
                        }

                        else if (readLineResult.Contains("ulong"))
                        {
                            RecordInformation.X_elementLength = 4;
                            RecordInformation.X_elementType = "ulong";
                        }
                    }
                    else if (readLineResult.Contains("axis_pts_y"))
                    {
                        if (readLineResult.Contains("sbyte"))
                        {
                            RecordInformation.Y_elementLength = 1;
                            RecordInformation.Y_elementType = "sbyte";
                        }
                        else if (readLineResult.Contains("ubyte"))
                        {
                            RecordInformation.Y_elementLength = 1;
                            RecordInformation.Y_elementType = "ubyte";
                        }
                        else if (readLineResult.Contains("sword"))
                        {
                            RecordInformation.Y_elementLength = 2;
                            RecordInformation.Y_elementType = "sword";
                        }

                        else if (readLineResult.Contains("uword"))
                        {
                            RecordInformation.Y_elementLength = 2;
                            RecordInformation.Y_elementType = "uword";
                        }

                        else if (readLineResult.Contains("slong"))
                        {
                            RecordInformation.Y_elementLength = 4;
                            RecordInformation.Y_elementType = "slong";
                        }

                        else if (readLineResult.Contains("ulong"))
                        {
                            RecordInformation.Y_elementLength = 4;
                            RecordInformation.Y_elementType = "ulong";
                        }
                    }
                    else if (readLineResult.Contains("fnc_values"))
                    {
                        if (readLineResult.Contains("sbyte"))
                        {
                            RecordInformation.V_elementLength = 1;
                            RecordInformation.V_elementType = "sbyte";
                        }
                        else if (readLineResult.Contains("ubyte"))
                        {
                            RecordInformation.V_elementLength = 1;
                            RecordInformation.V_elementType = "ubyte";
                        }
                        else if (readLineResult.Contains("sword"))
                        {
                            RecordInformation.V_elementLength = 2;
                            RecordInformation.V_elementType = "sword";
                        }

                        else if (readLineResult.Contains("uword"))
                        {
                            RecordInformation.V_elementLength = 2;
                            RecordInformation.V_elementType = "uword";
                        }

                        else if (readLineResult.Contains("slong"))
                        {
                            RecordInformation.V_elementLength = 4;
                            RecordInformation.V_elementType = "slong";
                        }

                        else if (readLineResult.Contains("ulong"))
                        {
                            RecordInformation.V_elementLength = 4;
                            RecordInformation.V_elementType = "ulong";
                        }
                    }
                }
                RecordList.Add(RecordInformation);
            }
            else if (readLineResult.Contains("/beginmemory_segmentpst") || readLineResult.Contains("/beginmemory_segmentdst") || readLineResult.Contains("/beginmemory_segmentram"))
            {
                index = 0;
                //XY_index = 0;
                MemoryInformation = new MemorySegmentClass();
                string[] strList = readLineResult.ToLower().Split(new char[1] { ' ' });
                foreach (string str in strList)
                {
                    if (str.Contains("0x") && (index == 0))
                    {
                        index++;
                        try
                        {
                            MemoryInformation.startAddress = Convert.ToUInt32(str.Replace("0x", ""), 16);
                        }
                        catch
                        {
                            FileClose();
                            return CodeCommand.DEAFAULT_START_ADDRES;
                        }
                    }
                    else if (str.Contains("0x") && (index == 1))
                    {
                        index++;
                        try
                        {
                            MemoryInformation.offset = Convert.ToUInt32(str.Replace("0x", ""), 16);
                        }
                        catch
                        {
                            FileClose();
                            return CodeCommand.DEAFAULT_OFFSET;
                        }
                        break;
                    }
                }
                while (readLineResult != "/endmemory_segment")
                {
                    readLineResult = A2lReader.ReadLine();
                    if (readLineResult.Contains("/*segmentlogicalnumber*/"))
                    {
                        if (!byte.TryParse(readLineResult.Replace("/*segmentlogicalnumber*/", ""), out MemoryInformation.segmentNumber))
                        {
                            FileClose();
                            return CodeCommand.DEAFAULT_SEGMENT_NUMBER;
                        }
                    }
                    else if (readLineResult.Contains("/*numberofpages*/"))
                    {
                        readLineResult = readLineResult.Replace("/*numberofpages*/", "").Replace("0x", "");
                        try
                        {
                            MemoryInformation.pageCount = Convert.ToByte(readLineResult, 16);
                        }
                        catch
                        {
                            FileClose();
                            return CodeCommand.DEAFAULT_PAGE_COUNT;
                        }
                    }
                    else if (readLineResult.Contains("/beginpage"))
                    {
                        byte tempPageNumber = new byte();
                        while (readLineResult != "/endpage")
                        {
                            readLineResult = A2lReader.ReadLine();
                            if (readLineResult.Contains("/*pagenumber*/"))
                            {
                                readLineResult = readLineResult.Replace("/*pagenumber*/", "").Replace("0x", "");
                                try
                                {
                                    tempPageNumber = Convert.ToByte(readLineResult, 16);
                                }
                                catch
                                {
                                    FileClose();
                                    return CodeCommand.DEAFAULT_PAGE_NUMBER;
                                }
                            }
                            else if (readLineResult.Contains("xcp_write_access_not_allowed"))
                            {
                                MemoryInformation.readPageNumber = tempPageNumber;
                            }
                            else if (readLineResult.Contains("xcp_write_access_with_ecu_only"))
                            {
                                MemoryInformation.writePageNumber = tempPageNumber;
                            }
                        }
                    }

                }
                MemoryList.Add(MemoryInformation);
                xcpData.MemorySedData = MemoryList;
            }
            return CodeCommand.DEAFAULT_SUCCESS;
        }

        #region 检查A2l是否是XCP类型，如果不是，则返回
        private static AgreementType CheckAgreementType()
        {
            if (isVaild == false)
            {
                FileClose();
                return AgreementType.other;
            }
            return AgreementType.XCP;
        }
        #endregion

        #region ReadList
        //private static CodeCommand ReadListMethod()
        //{
        //    #region ReadList
        //    for (int index = 0; index < measureList.Count(); index++)
        //    {
        //        MeasureMent tempMethold = MetholdList.Find(temp => temp.name == measureList[index].expression);
        //        if (tempMethold == null)
        //        {
        //            FileClose();
        //            return CodeCommand.METHOD_FUN_RESULT;
        //        }
        //        if (tempMethold.funType == "rat_func")
        //        {
        //            if (tempMethold.express != null)
        //            {
        //                //ReadList[index].expression = tempMethold.express;
        //                measureList[index].metholdType = "rat_func";
        //                if (!caculateReadParameter(tempMethold.express, index))
        //                {
        //                    FileClose();
        //                    return CodeCommand.METHOD_RAT_EXPRESS_CAL;
        //                }
        //            }
        //            else
        //            {
        //                FileClose();
        //                return CodeCommand.METHOD_RAT_EXPRESS;
        //            }
        //        }
        //        else if (tempMethold.funType == "tab_verb")
        //        {
        //            TableClass tempTable = TableList.Find(table => table.name == measureList[index].expression);
        //            if (tempTable == null)
        //            {
        //                FileClose();
        //                return CodeCommand.METHOD_VERT_EXPRESS_RESULT;
        //            }
        //            if (tempTable.table != null)
        //            {
        //                //ReadList[index].expression = tempTable.table;
        //                measureList[index].metholdType = "tab_verb";
        //                if (!caculateReadTableDictionary(tempTable.table, index))
        //                {
        //                    FileClose();
        //                    return CodeCommand.METHOD_VERT_EXPRESS_CAL;
        //                }
        //            }
        //            else
        //            {
        //                FileClose();
        //                return CodeCommand.METHOD_VERT_EXPRESS;
        //            }
        //        }
        //        else
        //        {
        //            FileClose();
        //            return CodeCommand.METHOD_RAT_FUN_NONE;
        //        }
        //    }
        //    return CodeCommand.READ_LIST_FAIL;
        //    #endregion
        //}
        #endregion

        #region WriteList
        //private static CodeCommand WriteListMethod()
        //{
        //    #region WriteList
        //    for (int index = 0; index < characterList.Count(); index++)
        //    {
        //        MetholdClass tempVMethold = MetholdList.Find(temp => temp.name == characterList[index].V_Expression);
        //        if (tempVMethold == null)
        //        {
        //            FileClose();
        //            return CodeCommand.METHOD_WRITE_1;
        //        }
        //        if (tempVMethold.funType == "rat_func")
        //        {
        //            if (tempVMethold.express != null)
        //            {
        //                //WriteList[index].V_Expression  = tempVMethold.express;
        //                characterList[index].V_ExpressType = "rat_func";
        //                if (!caculateWriteVParameter(tempVMethold.express, index))
        //                {
        //                    FileClose();
        //                    return CodeCommand.METHOD_WRITE_2;
        //                }
        //            }
        //            else
        //            {
        //                FileClose();
        //                return CodeCommand.METHOD_WRITE_3;
        //            }
        //        }
        //        else if (tempVMethold.funType == "tab_verb")
        //        {
        //            TableClass tempVTable = TableList.Find(table => table.name == characterList[index].V_Expression);
        //            if (tempVTable == null)
        //            {
        //                FileClose();
        //                return CodeCommand.METHOD_WRITE_4;
        //            }
        //            if (tempVTable.table != null)
        //            {
        //                //WriteList[index].V_Expression = tempVTable.table;
        //                characterList[index].V_ExpressType = "tab_verb";
        //                if (!caculateWriteVTableDictionary(tempVTable.table, index))
        //                {
        //                    FileClose();
        //                    return CodeCommand.METHOD_WRITE_5;
        //                }
        //            }
        //            else
        //            {
        //                FileClose();
        //                return CodeCommand.METHOD_WRITE_6;
        //            }
        //        }
        //        else
        //        {
        //            FileClose();
        //            return CodeCommand.METHOD_WRITE_7;
        //        }
        //        if (characterList[index].dimension >= 2)
        //        {
        //            MetholdClass tempXMethold = MetholdList.Find(temp => temp.name == characterList[index].X_Expression);
        //            if (tempXMethold == null)
        //            {
        //                FileClose();
        //                return CodeCommand.METHOD_WRITE_7;
        //            }
        //            if (tempXMethold.funType == "rat_func")
        //            {
        //                if (tempXMethold.express != null)
        //                {
        //                    //WriteList[index].V_Expression  = tempVMethold.express;
        //                    characterList[index].X_ExpressType = "rat_func";
        //                    if (!caculateWriteXParameter(tempXMethold.express, index))
        //                    {
        //                        FileClose();
        //                        return CodeCommand.METHOD_WRITE_8;
        //                    }
        //                }
        //                else
        //                {
        //                    FileClose();
        //                    return CodeCommand.METHOD_WRITE_9;
        //                }
        //            }
        //            else if (tempXMethold.funType == "tab_verb")
        //            {
        //                TableClass tempXTable = TableList.Find(table => table.name == characterList[index].X_Expression);
        //                if (tempXTable == null)
        //                {
        //                    FileClose();
        //                    return CodeCommand.METHOD_WRITE_10;
        //                }
        //                if (tempXTable.table != null)
        //                {
        //                    //WriteList[index].V_Expression = tempVTable.table;
        //                    characterList[index].X_ExpressType = "tab_verb";
        //                    if (!caculateWriteXTableDictionary(tempXTable.table, index))
        //                    {
        //                        FileClose();
        //                        return CodeCommand.METHOD_WRITE_11;
        //                    }

        //                }
        //                else
        //                {
        //                    FileClose();
        //                    return CodeCommand.METHOD_WRITE_12;
        //                }
        //            }
        //            else
        //            {
        //                FileClose();
        //                return CodeCommand.METHOD_WRITE_13;
        //            }
        //        }

        //        if (characterList[index].dimension == 3)
        //        {
        //            MetholdClass tempYMethold = MetholdList.Find(temp => temp.name == characterList[index].Y_Expression);
        //            if (tempYMethold == null)
        //            {
        //                FileClose();
        //                return CodeCommand.METHOD_WRITE_14;
        //            }
        //            if (tempYMethold.funType == "rat_func")
        //            {
        //                if (tempYMethold.express != null)
        //                {
        //                    //WriteList[index].V_Expression  = tempVMethold.express;
        //                    characterList[index].Y_ExpressType = "rat_func";
        //                    if (!caculateWriteYParameter(tempYMethold.express, index))
        //                    {
        //                        FileClose();
        //                        return CodeCommand.METHOD_WRITE_15;
        //                    }
        //                }
        //                else
        //                {
        //                    FileClose();
        //                    return CodeCommand.METHOD_WRITE_16;
        //                }
        //            }
        //            else if (tempYMethold.funType == "tab_verb")
        //            {
        //                TableClass tempYTable = TableList.Find(table => table.name == characterList[index].Y_Expression);
        //                if (tempYTable == null)
        //                {
        //                    FileClose();
        //                    return CodeCommand.METHOD_WRITE_17;
        //                }
        //                if (tempYTable.table != null)
        //                {
        //                    //WriteList[index].V_Expression = tempVTable.table;
        //                    characterList[index].Y_ExpressType = "tab_verb";
        //                    if (!caculateWriteYTableDictionary(tempYTable.table, index))
        //                    {
        //                        FileClose();
        //                        return CodeCommand.METHOD_WRITE_18;
        //                    }

        //                }
        //                else
        //                {
        //                    FileClose();
        //                    return CodeCommand.METHOD_WRITE_19;
        //                }
        //            }
        //            else
        //            {
        //                FileClose();
        //                return CodeCommand.METHOD_WRITE_20;
        //            }

        //        }

        //        RecordLayoutClass tempRecord = RecordList.Find(temp => temp.name == characterList[index].recordLayout);
        //        if (tempRecord == null)
        //        {
        //            FileClose();
        //            return CodeCommand.METHOD_WRITE_21;
        //        }
        //        characterList[index].V_elementLength = tempRecord.V_elementLength;
        //        characterList[index].V_elementType = tempRecord.V_elementType;
        //        if (characterList[index].dimension >= 2)
        //        {
        //            characterList[index].X_elementLength = tempRecord.X_elementLength;
        //            characterList[index].X_elementType = tempRecord.X_elementType;
        //        }

        //        if (characterList[index].dimension == 3)
        //        {
        //            characterList[index].Y_elementLength = tempRecord.Y_elementLength;
        //            characterList[index].Y_elementType = tempRecord.Y_elementType;
        //        }

        //        MemorySegmentClass tempMemory = MemoryList.Find(temp => ((characterList[index].address >= temp.startAddress) && (characterList[index].address <= (temp.startAddress + temp.offset))));
        //        if (tempMemory == null)
        //        {
        //            FileClose();
        //            return CodeCommand.METHOD_WRITE_22;
        //        }
        //        if (tempMemory.pageCount < 2)
        //        {
        //            FileClose();
        //            return CodeCommand.METHOD_WRITE_23;
        //        }
        //        characterList[index].pageCount = tempMemory.pageCount;
        //        characterList[index].readPageNumber = tempMemory.readPageNumber;
        //        characterList[index].writePageNumber = tempMemory.writePageNumber;
        //        characterList[index].segmentNumber = tempMemory.segmentNumber;
        //    }
        //    return CodeCommand.WRITE_LIST_FAIL;
        //    #endregion
        //}
        #endregion

        #region init 
        private static void Init()
        {
            measureList.Clear();
            characterList.Clear();
            RecordList.Clear();
            MetholdList.Clear();
            TableList.Clear();
            MemoryList.Clear();
            //propertyInformation.baudrate = 0;
            ////PropertyInformation.byteOrder = "";
            //propertyInformation.eventChannel_10 = 0;
            //propertyInformation.eventChannel_100 = 0;
            //propertyInformation.eventChannel_sync = 0;
            //propertyInformation.MasterCANID = 0;
            //propertyInformation.SlaverCANID = 0;
            isVaild = false;
            index = 0;
            dimensionIndex = 0;
            XY_index = 0;
        }
        #endregion

        #region 计算物理量与报文之间的转换
        //注意物理场景，不可能2个不同的物理值对应同一个CAN总现值的值，
        //因此不可能是一元二次方程，求解及防错可以简化
        //因为该函数在计算解析时使用量巨大，因此尽可能简化，所有防错做在其他函数中
        public static float caculatePhyValue(List<float> tempLsit, float canValue)
        {
            float result = 0f;
            if ((canValue * tempLsit[4] - tempLsit[1]) != 0f)
            {
                result = (tempLsit[2] - (canValue * tempLsit[5])) / (canValue * tempLsit[4] - tempLsit[1]);
            }
            return result;
        }
        //注意物理场景，不可能2个不同的物理值对应同一个CAN总现值的值，因此不可能是一元二次方程，求解及防错可以简化
        //因为该函数在计算解析时使用量巨大，因此尽可能简化，所有防错做在其他函数中
        public static float caculateCANValue(List<float> tempLsit, float phyValue)
        {
            float result = 0f;
            result = ((tempLsit[0] * phyValue * phyValue) + (tempLsit[1] * phyValue) + tempLsit[2]) / ((tempLsit[3] * phyValue * phyValue) + (tempLsit[4] * phyValue) + tempLsit[5]);
            return result;
        }
        #endregion

        #region 计算表格数据
        private static bool caculateReadTableDictionary(string table, int index)
        {
            measureList[index].tableDictionary = new Dictionary<UInt32, string>();
               string[]  strList =  table.Split(new string[1] {"^&*"}, StringSplitOptions.None);
            UInt32 keyValue = new UInt32();
            string tableContent = new string(new char[] { });
            foreach (string str in strList)
            {
                   string[] tempTable = str.Split(new string[1] {"\""}, StringSplitOptions.None);
                bool isKeyValue = false;
                foreach (string strEumn in tempTable)
                {
                    if ((strEumn.Replace(" ", "") != "") && (isKeyValue == false))
                    {
                        isKeyValue = true;
                        string Eumn = strEumn.Replace(" ", "");
                        if (!UInt32.TryParse(Eumn, out keyValue))
                        {
                            return false;
                        }
                    }
                    else if ((strEumn.Replace(" ", "") != "") && (isKeyValue == true))
                    {
                        tableContent = strEumn;
                        measureList[index].tableDictionary.Add(keyValue, tableContent);
                        break;
                    }
                }
                //ReadList[index].tableDictionary.Add(keyValue, tableContent);
            }
            return true;

        }
        #endregion

        #region 计算写入X表格数据
        private static bool caculateWriteXTableDictionary(string table, int index)
        {
            characterList[index].X_tableDictionary = new Dictionary<UInt32, string>();
            string[] strList = table.Split(new string[1] { "^&*" }, StringSplitOptions.None);
            UInt32 keyValue = new UInt32();
            string tableContent = new string(new char[] { });
            foreach (string str in strList)
            {
                string[] tempTable = str.Split(new string[1] { "\"" }, StringSplitOptions.None);
                bool isKeyValue = false;
                foreach (string strEumn in tempTable)
                {
                    if ((strEumn.Replace(" ", "") != "") && (isKeyValue == false))
                    {
                        isKeyValue = true;
                        string Eumn = strEumn.Replace(" ", "");
                        if (!UInt32.TryParse(Eumn, out keyValue))
                        {
                            return false;
                        }
                    }
                    else if ((strEumn.Replace(" ", "") != "") && (isKeyValue == true))
                    {
                        tableContent = strEumn;
                        characterList[index].X_tableDictionary.Add(keyValue, tableContent);
                        break;
                    }
                }
                //ReadList[index].tableDictionary.Add(keyValue, tableContent);
            }
            return true;

        }
        #endregion

        #region 计算写入Y表格数据
        private static bool caculateWriteYTableDictionary(string table, int index)
        {
            characterList[index].Y_tableDictionary = new Dictionary<UInt32, string>();
            string[] strList = table.Split(new string[1] { "^&*" }, StringSplitOptions.None);
            UInt32 keyValue = new UInt32();
            string tableContent = new string(new char[] { });
            foreach (string str in strList)
            {
                string[] tempTable = str.Split(new string[1] { "\"" }, StringSplitOptions.None);
                bool isKeyValue = false;
                foreach (string strEumn in tempTable)
                {
                    if ((strEumn.Replace(" ", "") != "") && (isKeyValue == false))
                    {
                        isKeyValue = true;
                        string Eumn = strEumn.Replace(" ", "");
                        if (!UInt32.TryParse(Eumn, out keyValue))
                        {
                            return false;
                        }
                    }
                    else if ((strEumn.Replace(" ", "") != "") && (isKeyValue == true))
                    {
                        tableContent = strEumn;
                        characterList[index].Y_tableDictionary.Add(keyValue, tableContent);
                        break;
                    }
                }
                //ReadList[index].tableDictionary.Add(keyValue, tableContent);
            }
            return true;

        }
        #endregion

        #region 计算写入V表格数据
        private static bool caculateWriteVTableDictionary(string table, int index)
        {
            characterList[index].V_tableDictionary = new Dictionary<UInt32, string>();
            string[] strList = table.Split(new string[1] { "^&*" }, StringSplitOptions.None);
            UInt32 keyValue = new UInt32();
            string tableContent = new string(new char[] { });
            foreach (string str in strList)
            {
                string[] tempTable = str.Split(new string[1] { "\"" }, StringSplitOptions.None);
                bool isKeyValue = false;
                foreach (string strEumn in tempTable)
                {
                    if ((strEumn.Replace(" ", "") != "") && (isKeyValue == false))
                    {
                        isKeyValue = true;
                        string Eumn = strEumn.Replace(" ", "");
                        if (!UInt32.TryParse(Eumn, out keyValue))
                        {
                            return false;
                        }
                    }
                    else if ((strEumn.Replace(" ", "") != "") && (isKeyValue == true))
                    {
                        tableContent = strEumn;
                        characterList[index].V_tableDictionary.Add(keyValue, tableContent);
                        break;
                    }
                }
                //ReadList[index].tableDictionary.Add(keyValue, tableContent);
            }
            return true;

        }
        #endregion

        #region 计算读取参数(coeffs表达式值)
        private static bool caculateReadParameter(string Parameter, int index)
        {
            measureList[index].parameter = new List<float>();
            string[] parameterList = Parameter.Split(new char[1] { ' ' });
            string temp;
            float result;
            foreach (string tempParamenter in parameterList)
            {
                temp = tempParamenter.Replace(" ", "");
                if (temp != "")
                {
                    if (float.TryParse(temp, out result))
                    {
                        measureList[index].parameter.Add(result);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            //注意物理场景，不可能2个不同的物理值对应同一个CAN总现值的值，因此不可能是一元二次方程，求解及防错可以简化
            if ((measureList[index].parameter.Count != 6) || (measureList[index].parameter[0] != 0f) || (measureList[index].parameter[3] != 0f))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region 计算写入X参数
        private static bool caculateWriteXParameter(string Parameter, int index)
        {
            characterList[index].X_parameter = new List<float>();
            string[] parameterList = Parameter.Split(new char[1] { ' ' });
            string temp;
            float result;
            foreach (string tempParamenter in parameterList)
            {
                temp = tempParamenter.Replace(" ", "");
                if (temp != "")
                {
                    if (float.TryParse(temp, out result))
                    {
                        characterList[index].X_parameter.Add(result);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            //注意物理场景，不可能2个不同的物理值对应同一个CAN总现值的值，因此不可能是一元二次方程，求解及防错可以简化
            if ((characterList[index].X_parameter.Count != 6) || (characterList[index].X_parameter[0] != 0f) || (characterList[index].X_parameter[3] != 0f))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region 计算写入Y参数
        private static bool caculateWriteYParameter(string Parameter, int index)
        {
            characterList[index].Y_parameter = new List<float>();
            string[] parameterList = Parameter.Split(new char[1] { ' ' });
            string temp;
            float result;
            foreach (string tempParamenter in parameterList)
            {
                temp = tempParamenter.Replace(" ", "");
                if (temp != "")
                {
                    if (float.TryParse(temp, out result))
                    {
                        characterList[index].Y_parameter.Add(result);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            //注意物理场景，不可能2个不同的物理值对应同一个CAN总现值的值，因此不可能是一元二次方程，求解及防错可以简化
            if ((characterList[index].Y_parameter.Count != 6) || (characterList[index].Y_parameter[0] != 0f) || (characterList[index].Y_parameter[3] != 0f))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region 计算写入V参数
        private static bool caculateWriteVParameter(string Parameter, int index)
        {
            characterList[index].V_parameter = new List<float>();
            string[] parameterList = Parameter.Split(new char[1] { ' ' });
            string temp;
            float result;
            foreach (string tempParamenter in parameterList)
            {
                temp = tempParamenter.Replace(" ", "");
                if (temp != "")
                {
                    if (float.TryParse(temp, out result))
                    {
                        characterList[index].V_parameter.Add(result);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            //注意物理场景，不可能2个不同的物理值对应同一个CAN总现值的值，因此不可能是一元二次方程，求解及防错可以简化
            if ((characterList[index].V_parameter.Count != 6) || (characterList[index].V_parameter[0] != 0f) || (characterList[index].V_parameter[3] != 0f))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region 关闭文件
        private static void FileClose()
        {
            A2lFile.Close();
            A2lReader.Close();
        }
        #endregion
    }
}

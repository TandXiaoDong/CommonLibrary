using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalysisAgreeMent.Model.DBC;
using AnalysisAgreeMent.Model.XCP;
using AnalysisAgreeMent.Model;
using CommonUtils.Logger;

namespace AnalysisAgreeMent.Analysis
{
    public class AnalysisDataSet
    {
        /// <summary>
        /// 合并a2l 与dbc数据，统一格式
        /// </summary>
        public static AnalysisData UnionXcpDbc(FileType fileType, XcpData xcpData, DBCData dbcData)
        {
            ////name + describle+unit+dataType+dataLen+IsMotorola+startIndex+dataBitLen+dataAddress+factor+offset
            AnalysisData analysisData = new AnalysisData();
            try
            {
                switch (fileType)
                {
                    case FileType.A2L:
                        var measureList = xcpData.MeasureData;
                        if (measureList == null)
                        {
                            LogHelper.Log.Info("XCP_DATA_MeasureData为空！");
                            return null;
                        }
                        var metholdList = xcpData.MetholdData;
                        if (metholdList == null)
                        {
                            LogHelper.Log.Info("XCP_DATA_METHOLD_LIST为空！");
                            return null;
                        }
                        var propertyList = xcpData.PropertyData;
                        if (propertyList == null)
                        {
                            LogHelper.Log.Info("XCP_DATA_PROPERTY_LIST为空！");
                            return null;
                        }
                        analysisData.AnalysisiXcpDataList = new List<AnalysisSignal>();
                        LogHelper.Log.Info(" start :"+measureList.Count+"  "+metholdList.Count);
                        for (int i = 0; i < measureList.Count; i++)
                        {
                            AnalysisSignal analysisSignal = new AnalysisSignal();
                            analysisSignal.OrderId = i + 1;
                            analysisSignal.Name = measureList[i].Name;
                            analysisSignal.Describle = measureList[i].Describle;

                            ///查询函数值
                            var mdList = metholdList.Find(tm => tm.name == measureList[i].ReferenceMethod);

                            if (mdList == null)
                            {
                                //LogHelper.Log.Info("查询metholdList失败，查询结果为空！空ID号为"+i+" 方法名为"+measureList[i].ReferenceMethod);
                                //B_TRUE 查询不到
                                continue;
                            }
                            analysisSignal.Unit = mdList.unit;

                            DataTypeEnum dataTypeEnum = (DataTypeEnum)Enum.Parse(typeof(DataTypeEnum), measureList[i].Type.ToUpper());
                            SaveDataTypeEnum svType = TypeConvert.AnalysisTypeToSaveType(dataTypeEnum);
                            analysisSignal.SaveDataType = svType;

                            analysisSignal.SaveDataLen = TypeConvert.AnalysisTypeToLength(dataTypeEnum);
                            analysisSignal.IsMotorola = (int)propertyList[0].byteOrder;
                            analysisSignal.StartIndex = 0;
                            analysisSignal.DataBitLen = 0;
                            analysisSignal.DataAddress = measureList[i].EcuAddress;

                            analysisSignal.Factor = mdList.Factor;
                            analysisSignal.OffSet = mdList.OffSet;

                            analysisData.AnalysisiXcpDataList.Add(analysisSignal);
                        }
                        break;
                    case FileType.DBC:
                        var dbcmsgList = dbcData.DBCMessageList;
                        var dbcsigList = dbcData.DBCSignalList;
                        analysisData.AnalysisDbcDataList = new List<AnalysisSignal>();
                        int count = 0;
                        for (int i = 0; i < dbcmsgList.Count; i++)
                        {
                            var dbcList = dbcsigList.FindAll(msg => msg.FrameID == dbcmsgList[i].FrameID);
                            for (int j = 0; j < dbcList.Count; j++)
                            {
                                AnalysisSignal signal = new AnalysisSignal();
                                signal.OrderId = count + 1;
                                signal.Name = dbcList[j].SignalName;
                                signal.Describle = dbcmsgList[i].FrameName;
                                signal.Unit = dbcList[j].Unit;
                                signal.SaveDataType = SaveDataTypeEnum.V_INT;
                                signal.SaveDataLen = dbcmsgList[i].MessageLen;
                                signal.IsMotorola = (int)dbcList[j].ByteOrder;
                                signal.StartIndex = dbcList[j].StartBitIndex;
                                signal.DataBitLen = dbcList[j].BitLength;
                                signal.DataAddress = dbcmsgList[i].FrameID;
                                signal.Factor = dbcList[j].Factor;
                                signal.OffSet = dbcList[j].Offset;
                                analysisData.AnalysisDbcDataList.Add(signal);
                                count++;
                            }
                        }
                        break;
                }

            }
            catch (Exception e)
            {
                LogHelper.Log.Error("a2l dbc数据整合失败！失败原因：" + e.Message + e.StackTrace);
            }
            return analysisData;
        }
    }
}

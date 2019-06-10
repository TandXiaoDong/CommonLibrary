using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Configuration;
using CommonUtils.Logger;

namespace CommonUtils.FileHelper
{
    /// <summary>
    /// 处理string字符串，显示与保存
    /// </summary>
    class ReadString
    {
        
        /// <summary>
        /// 原始数据长度
        /// </summary>
        public static int lenFrequency;

        private static object obj = new object();

        /// <summary>
        /// 保存原始数据用于计算算法
        /// </summary>
        public static bool bFlagAlgrithmStart;
        /// <summary>
        /// 保存原始数据用于显示
        /// </summary>
        public static bool bFlagOriginData;
        private static DateTime timerBegin;
        private static DateTime timerEnd;
        private static DateTime triggerBegin;
        private static DateTime triggerEnd;

        /// <summary>
        /// 本地文件type = string
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="fname">文件名</param>
        //public static DataTable ReadFileDT(string path)
        //{
        //    try
        //    {
        //        DataTable dt_analysFile = new DataTable();
        //        dt_analysFile.Columns.Add(DataTableColumnName.SIG_TIME);
        //        dt_analysFile.Columns.Add(DataTableColumnName.SIG_X);
        //        dt_analysFile.Columns.Add(DataTableColumnName.SIG_Y);
        //        dt_analysFile.Columns.Add(DataTableColumnName.SIG_Z);

        //        if (File.Exists(path))
        //        {
        //            string str = string.Empty;
        //            FileStream fs = new FileStream(path, FileMode.Open);
        //            StreamReader sr = new StreamReader(fs, Encoding.Default);

        //            while (true)
        //            {
        //                str = sr.ReadLine();
        //                if (!string.IsNullOrEmpty(str))
        //                {
        //                    string[] strRes = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        //                    DataRow dr = dt_analysFile.NewRow();
        //                    dr[DataTableColumnName.SIG_TIME] = strRes[0];
        //                    dr[DataTableColumnName.SIG_X] = strRes[1];
        //                    dr[DataTableColumnName.SIG_Y] = strRes[2];
        //                    dr[DataTableColumnName.SIG_Z] = strRes[3];
        //                    dt_analysFile.Rows.Add(dr);
        //                }
        //                else
        //                {
        //                    sr.Close();
        //                    fs.Close();
        //                    break;
        //                }
        //            }

        //            return dt_analysFile;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        /// <summary>
        /// 解析接收到的string格式数据,以及本地保存的数据格式包含设备参数
        /// 保存参数放在数据末尾，以|隔开格式：采样频率+量程+传感器类型+灵敏度三个+启动采样时间+连续采样开始时间+
        /// +连续采样结束时间+超前点数+存储深度+连续触发采集开始时间+连续触发采集结束时间+
        /// +XYZ各十个阈值；
        /// </summary>
        /// <param name="revMsg"></param>
        /// <param name="revMsgs"></param>
        //public static void ReceiveString(string revMsg, string[] revMsgs,string sampleDataSave,DataTableM.AluData dt,List<string> listAlg,EquipStateData devData)
        //{
        //    string isTrigger = "0";
        //    string saveLocalData = revMsg;
        //    string[] saveLocalArray = revMsgs;
        //    string saveLocalParams = "";
        //    string charactorx = "";
        //    string charactory = "";
        //    string charactorz = "";
        //    string devId = "";
        //    int len = 0;
        //    string dataPath = "";
        //    DataRow drw = dt.dtOrignalMonitor.NewRow();
        //    string saveFlg = ConfigurationManager.AppSettings["saveFlg"].ToString();
        //    //Log.Write(revMsg+"\r\n");
        //    try
        //    {
        //        //命令+DEVID+长度+30*特征值+{x y z}
        //        devId = revMsgs[1];
        //        if (string.IsNullOrEmpty(sampleDataSave))
        //        {
        //            sampleDataSave = @"C:\SIGNALDATA";
        //        }
        //        dataPath = sampleDataSave+"\\"+ devId + "-" + DateTime.Now.ToString("yyyyMMddHH") + ".txt";

        //        revMsg = revMsg.Substring(revMsgs[0].Length + 1 + revMsgs[1].Length + 1 + revMsgs[2].Length + 1);
        //        len = int.Parse(revMsgs[2]); //总长度
        //        int chactorLen = 0;
        //        isTrigger = revMsgs[3];
        //        chactorLen += revMsgs[3].Length + 1;
        //        for (int m = 4; m < 34; m++)
        //        {
        //            chactorLen += revMsgs[m].Length + 1;
        //            if (m < 14 && m >= 4)
        //                charactorx += double.Parse(revMsgs[m]) + DevComand.SplitChar;
        //            if (m < 24 && m >= 14)
        //                charactory += double.Parse(revMsgs[m]) + DevComand.SplitChar;
        //            if (m < 34 && m >= 24)
        //                charactorz += double.Parse(revMsgs[m]) + DevComand.SplitChar;
        //        }
        //        revMsg = revMsg.Substring(chactorLen);
        //        len = len - 30-1-1; ///命令号+编号+三十个特征值

        //        drw[DataTableColumnName.SIG_X_CHARACTOR] = charactorx;
        //        drw[DataTableColumnName.SIG_Y_CHARACTOR] = charactory;
        //        drw[DataTableColumnName.SIG_Z_CHARACTOR] = charactorz;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Error(ex.Message + ex.StackTrace);
        //    }
        //    if (!string.IsNullOrEmpty(revMsg))
        //    {
        //        //长度+t x y z
        //        revMsgs = revMsg.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        //        AlgrithCal.IsHaveData = true;
        //        int i = 1;
        //        int j = 0;
        //        try
        //        {
        //            string time = "";
        //            float x = 0;
        //            float y = 0;
        //            float z = 0;
        //            ///第一位为触发标志位
        //            ///该频率数据长度
        //            lenFrequency = (revMsgs.Length) / 4;
        //            if (len != revMsgs.Length / 4)
        //            {
        //                LogHelper.Error("数据长度不正确");
        //            }
        //            if (lenFrequency % 1000 != 0)
        //            {
        //                LogHelper.Error(" 接收数据长度异常 " + lenFrequency + "    ");
        //                charactorx = "";
        //                charactory = "";
        //                charactorz = "";
        //                return;
        //            }
        //            //Log.Write(" -----------------+lenFrequency " + lenFrequency +" len:"+len+ "   time:" + revMsgs[0] + "   " + TimeStamp.ConvertStringToDateTime(revMsgs[0])+ " isTrigger:" + isTrigger+ "  listAlgCount:"+ listAlg.Count+" originCount:"+ dt.dtOrignalMonitor.Rows.Count);

        //            for (i = 1; i <= (revMsgs.Length) / 4; i++)
        //            {
        //                time = revMsgs[j];
        //                if (!float.TryParse(revMsgs[j + 1], out x))
        //                    LogHelper.Error("格式转换错误");
        //                if (!float.TryParse(revMsgs[j + 2], out y))
        //                    LogHelper.Error("格式转换错误");
        //                if (!float.TryParse(revMsgs[j + 3], out z))
        //                    LogHelper.Error("格式转换错误");
        //                j += 4;

        //                //当前数据中的数据点的时间戳 比上一组数据中最后一个点的时间戳还小，则不匹配，跳过该数据点，计算下一个数据点
        //                //if (long.Parse(time) < lngTimeOfLastBatch)
        //                //{
        //                //    //Log.Write("时间延时---------- time "+time+"  lngTimeOfLastBatch "+lngTimeOfLastBatch);
        //                //    //continue;
        //                //}
                        
        //                try
        //                {
        //                    if (drw == null)
        //                    {
        //                        drw = dt.dtOrignalMonitor.NewRow();
        //                    }
        //                    string tp = TimeStamp.ConvertStringToDateTime(time).ToString("yyyy/MM/dd HH:mm:ss.fff");
        //                    drw[DataTableColumnName.SIG_TIME] = tp;
        //                    drw[DataTableColumnName.SIG_X] = x;
        //                    drw[DataTableColumnName.SIG_Y] = y;
        //                    drw[DataTableColumnName.SIG_Z] = z;
        //                    dt.dtOrignalMonitor.Rows.Add(drw);
        //                    drw = null;
        //                    ///算法数据,用原始数据计算
        //                    if (false)
        //                    {
        //                        DataRow dra = dt.dataTableOrignIn.NewRow();
        //                        //Log.Write("   " + TimeStamp.ConvertStringToDateTime(time).ToString("yyyy/MM/dd HH:mm:ss.fff"));
        //                        dra[DataTableColumnName.TRIGGER_FLG] = isTrigger;
        //                        dra[DataTableColumnName.SIG_TIME] = time;
        //                        dra[DataTableColumnName.SIG_X] = x / 1000;
        //                        dra[DataTableColumnName.SIG_Y] = y / 1000;
        //                        dra[DataTableColumnName.SIG_Z] = z / 1000;

        //                        dt.dataTableOrignIn.Rows.Add(dra);
        //                        listAlg.Add(time);
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    LogHelper.Error(ex.Message+ex.StackTrace);
        //                }
        //            }
        //            //this.lngTimeOfLastBatch = long.Parse(time);
        //            if (Device.IsLocalSaveData)
        //            {
        //                StringToBytesSaveLocal(saveLocalData, saveLocalArray, devData, dataPath);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            LogHelper.Error(ex.Message + ex.StackTrace + "-------------- \r\n" + " lenfrequency " + lenFrequency + "  " );
        //        }
        //        //clear charactorxyz
        //        charactorx = "";
        //        charactory = "";
        //        charactorz = "";
        //    }
        //    else
        //    {
        //        ///没有数据，只有特征值数据
        //        AlgrithCal.IsHaveData = false;
        //    }
        //}

        //private static string AddDevParamsToString(EquipStateData devData)
        //{
        //    string devStr = devData.SampleFreq + DevComand.SplitChar;
        //    devStr += devData.SampleRangeX + DevComand.SplitChar;
        //    devStr += devData.SensorInput + DevComand.SplitChar;
        //    devStr += devData.ISensitiveX + DevComand.SplitChar;
        //    devStr += devData.ISensitiveY + DevComand.SplitChar;
        //    devStr += devData.ISensitiveZ + DevComand.SplitChar;
        //    devStr += devData.LClctStartTime + DevComand.SplitChar;
        //    devStr += devData.TimerBeginTime + DevComand.SplitChar;
        //    devStr += devData.TimerEndTime + DevComand.SplitChar;
        //    devStr += devData.BeforePoint + DevComand.SplitChar;
        //    devStr += devData.StorageDepth + DevComand.SplitChar;
        //    devStr += devData.TriggerStartTime + DevComand.SplitChar;
        //    devStr += devData.TriggerEndTime + DevComand.SplitChar;
        //    devStr += devData.ThresholdX1 + DevComand.SplitChar;
        //    devStr += devData.ThresholdX2 + DevComand.SplitChar;
        //    devStr += devData.ThresholdX3 + DevComand.SplitChar;
        //    devStr += devData.ThresholdX4 + DevComand.SplitChar;
        //    devStr += devData.ThresholdX5 + DevComand.SplitChar;
        //    devStr += devData.ThresholdX6 + DevComand.SplitChar;
        //    devStr += devData.ThresholdX7 + DevComand.SplitChar;
        //    devStr += devData.ThresholdX8 + DevComand.SplitChar;
        //    devStr += devData.ThresholdX9 + DevComand.SplitChar;
        //    devStr += devData.ThresholdX10 + DevComand.SplitChar;
        //    devStr += devData.ThresholdY1 + DevComand.SplitChar;
        //    devStr += devData.ThresholdY2 + DevComand.SplitChar;
        //    devStr += devData.ThresholdY3 + DevComand.SplitChar;
        //    devStr += devData.ThresholdY4 + DevComand.SplitChar;
        //    devStr += devData.ThresholdY5 + DevComand.SplitChar;
        //    devStr += devData.ThresholdY6 + DevComand.SplitChar;
        //    devStr += devData.ThresholdY7 + DevComand.SplitChar;
        //    devStr += devData.ThresholdY8 + DevComand.SplitChar;
        //    devStr += devData.ThresholdY9 + DevComand.SplitChar;
        //    devStr += devData.ThresholdY10 + DevComand.SplitChar;
        //    devStr += devData.ThresholdZ1 + DevComand.SplitChar;
        //    devStr += devData.ThresholdZ2 + DevComand.SplitChar;
        //    devStr += devData.ThresholdZ3 + DevComand.SplitChar;
        //    devStr += devData.ThresholdZ4 + DevComand.SplitChar;
        //    devStr += devData.ThresholdZ5 + DevComand.SplitChar;
        //    devStr += devData.ThresholdZ6 + DevComand.SplitChar;
        //    devStr += devData.ThresholdZ7 + DevComand.SplitChar;
        //    devStr += devData.ThresholdZ8 + DevComand.SplitChar;
        //    devStr += devData.ThresholdZ9 + DevComand.SplitChar;
        //    devStr += devData.ThresholdZ10 + DevComand.SplitChar;
        //    return devStr;
        //}

        //private static void RefreshDevStatus(EquipStateData devData,string[] data)
        //{
        //    devData.SampleFreq = data[0];
        //    devData.SampleRangeX = data[1];
        //    devData.SensorInput = data[2];
        //    devData.ISensitiveX = data[3];
        //    devData.ISensitiveY = data[4];
        //    devData.ISensitiveZ = data[5];
        //    devData.LClctStartTime = data[6];
        //    devData.TimerBeginTime = data[7];
        //    devData.TimerEndTime = data[8];
        //    devData.BeforePoint = data[9];
        //    devData.StorageDepth = data[10];
        //    devData.TriggerStartTime = data[11];
        //    devData.TriggerEndTime = data[12];
        //    devData.ThresholdX1 = data[13];
        //    devData.ThresholdX2 = data[14];
        //    devData.ThresholdX3 = data[15];
        //    devData.ThresholdX3 = data[16];
        //    devData.ThresholdX4 = data[17];
        //    devData.ThresholdX5 = data[18];
        //    devData.ThresholdX6 = data[19];
        //    devData.ThresholdX7 = data[20];
        //    devData.ThresholdX8 = data[21];
        //    devData.ThresholdX9 = data[22];
        //    devData.ThresholdX10 = data[23];
        //    devData.ThresholdY1 = data[24];
        //    devData.ThresholdY2 = data[25];
        //    devData.ThresholdY3 = data[26];
        //    devData.ThresholdY4 = data[27];
        //    devData.ThresholdY5 = data[28];
        //    devData.ThresholdY6 = data[29];
        //    devData.ThresholdY7 = data[30];
        //    devData.ThresholdY8 = data[31];
        //    devData.ThresholdY9 = data[32];
        //    devData.ThresholdY10 = data[33];
        //    devData.ThresholdZ1 = data[34];
        //    devData.ThresholdZ2 = data[35];
        //    devData.ThresholdZ3 = data[36];
        //    devData.ThresholdZ4 = data[37];
        //    devData.ThresholdZ5 = data[38];
        //    devData.ThresholdZ6 = data[39];
        //    devData.ThresholdZ7 = data[40];
        //    devData.ThresholdZ8 = data[41];
        //    devData.ThresholdZ9 = data[42];
        //    devData.ThresholdZ10 = data[43];
        //}

        ///// <summary>
        ///// 数据起始位置
        ///// </summary>
        //private static int indexStartData = 133;//128+4+1;
        ///// <summary>
        ///// 参数起始位置
        ///// </summary>
        //private static int indexStartParams;
        ///// <summary>
        ///// 量程长度
        ///// </summary>
        //private static int rangeLen;
        ///// <summary>
        ///// 阈值长度
        ///// </summary>
        //private static int thresholdLen;

        //private static void StringToBytesSaveLocal(string revData,string[] revArray, EquipStateData devData,string dataPath)
        //{
        //    ///解析云端保存的数据格式和回收保存的数据格式:时间+三十个特征值+三个通道数据
        //    /// +采样频率+量程+采集模式+定时采集开始+结束时间+触发开始时间+触发结束时间（时间戳）+阈值长度+XYZ阈值三十个（字符串）
        //    ///string:三十个特征值+通道数+数据长度+(t +x +y+z)*len+devParams(//频率4+量程长度+3个量程+采集模式4+采集时间8*4+触发值长度4+触发值信息string)
        //    ///len: 4*30+(8+4*3)*dataLen+4+4*3+20+4+4*3+8*5+4+4+4*30
        //    ///调整：8+30*4+（4*3）*dataLen+
        //    int dataNum;///xyz数据长度
        //    revData = revData.Substring(revArray[0].Length + 1 + revArray[1].Length + 1 + revArray[2].Length + 1);
        //    int len = int.Parse(revArray[2]); //总长度
        //    dataNum = len - 30 - 1 - 1;
        //    ///时间+特征值30+通道+数据长度+（xyz数据）*len+频率+量程长度+量程信息+采集模式+时间4+触发值长度+触发值信息
        //    ///
        //    if (string.IsNullOrEmpty(devData.SampleRangeX))
        //    {
        //        devData.SampleRangeX = "0";
        //    }
        //    if (string.IsNullOrEmpty(devData.SampleRangeY))
        //    {
        //        devData.SampleRangeY = "0";
        //    }
        //    if (string.IsNullOrEmpty(devData.SampleRangeZ))
        //    {
        //        devData.SampleRangeZ = "0";
        //    }
        //    rangeLen = devData.SampleRangeX.Length + devData.SampleRangeY.Length + devData.SampleRangeZ.Length+2;
        //    thresholdLen = ThresholdLen(devData);
        //    byte[] databyte = new byte[8+4 * 30 + 5 + 4 * 3 * dataNum + 4 + 4+ rangeLen + 4 + 8 * 4 + 4 + thresholdLen];

        //    int chactorLen = 0;
        //    chactorLen = revArray[3].Length + 1;
        //    DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));

        //    string timesig = TimeStamp.ConvertStringToDateTime(revArray[34]).ToString();

        //    DoubleToBytes(databyte, (double)((Convert.ToDateTime(timesig) - startTime).TotalMilliseconds), 0);

        //    ///三十个特征值
        //    for (int m = 4; m < 34; m++)
        //    {
        //        chactorLen += revArray[m].Length + 1;
        //        if (m < 14 && m >= 4)
        //        {
        //            FloatToBytes(databyte, float.Parse(revArray[m]), 8 + 4 * (m - 4)); //endIndex=4*(13-4)=36+4=40
        //        }
        //        else if (m < 24 && m >= 14)
        //        {
        //            FloatToBytes(databyte, float.Parse(revArray[m]), 8 + 4 * (m - 4)); //endIndex=4*(23-4)
        //        }
        //        else if (m < 34 && m >= 24)
        //        {
        //            FloatToBytes(databyte, float.Parse(revArray[m]), 8 + 4 * (m - 4)); //endIndex=4*(33-4)=116+4
        //        }
        //    }

        //    revData = revData.Substring(chactorLen);
        //    ///通道数+数据长度
        //    ///
        //    ///通道数占一位
        //    ///
        //    byte[] channel = BitConverter.GetBytes(3);
        //    databyte[128] = channel[0];
        //    //IntToBytes(databyte, revArray.Length / 4, 129);

        //    if (!string.IsNullOrEmpty(revData))
        //    {
        //        string time = "";
        //        float x = 0;
        //        float y = 0;
        //        float z = 0;
        //        int i = 0;
        //        int j = 0;
        //        revArray = revData.Split(new char[] { '|' });
        //        IntToBytes(databyte, revArray.Length / 4, 129);

        //        for (i = 0; i < (revArray.Length) / 4; i++)
        //        {
        //            time = revArray[j];
        //            //DoubleToBytes(dataFloat, (double)((Convert.ToDateTime(time) - startTime).TotalSeconds), indexStartData+(i-1)*20);

        //            if (float.TryParse(revArray[j + 1], out x))
        //            {
        //                FloatToBytes(databyte, float.Parse(revArray[j + 1]), indexStartData + i * 12); //0   12  24
        //            }
        //            if (float.TryParse(revArray[j + 2], out y))
        //            {
        //                FloatToBytes(databyte, float.Parse(revArray[j + 2]), indexStartData + i * 12 + 4);
        //            }
        //            if (float.TryParse(revArray[j + 3], out z))
        //            {
        //                FloatToBytes(databyte, float.Parse(revArray[j + 3]), indexStartData + i * 12 + 8);
        //            }
        //            j += 4;
        //        }
        //        indexStartParams = (3 * 4) * (revArray.Length / 4) + indexStartData; //12000+133=12133
        //        ///string:三十个特征值+(t +x +y+z)*len+devParams(频率+量程*3+传感器类型+灵敏度*3+采集模式+采集时间*5+超前点数+存储深度+阈值*30)
        //        ///len: 4*30+(8+4*3)*dataLen+4+4*3+20+4*3+4+8*5+4+4+4*30
        //        ///保存设备参数
        //        ///(频率4+量程长度+3个量程+采集模式4+采集时间8*4+触发值长度4+触发值信息string)
        //        IntToBytes(databyte, int.Parse(devData.SampleFreq), indexStartParams);
        //        IntToBytes(databyte, rangeLen, indexStartParams + 4);
        //        StrToBytes(databyte, devData.SampleRangeX+",", indexStartParams + 8, devData.SampleRangeX.Length+1);
        //        StrToBytes(databyte, devData.SampleRangeY+",", indexStartParams + 8 + devData.SampleRangeX.Length+1, devData.SampleRangeY.Length+1);
        //        StrToBytes(databyte, devData.SampleRangeZ, indexStartParams + 8 + devData.SampleRangeY.Length+devData.SampleRangeX.Length+2, devData.SampleRangeZ.Length);
        //        IntToBytes(databyte, int.Parse(devData.SampleModel), indexStartParams + 8 + rangeLen);
        //        DateTimeIsNull(devData);
        //        DoubleToBytes(databyte, (timerBegin - startTime).TotalSeconds, indexStartParams + rangeLen + 12);
        //        DoubleToBytes(databyte, (timerEnd - startTime).TotalSeconds, indexStartParams + rangeLen + 20);
        //        DoubleToBytes(databyte, (triggerBegin - startTime).TotalSeconds, indexStartParams + rangeLen + 28);
        //        DoubleToBytes(databyte, (triggerEnd - startTime).TotalSeconds, indexStartParams + rangeLen + 36);
        //        IntToBytes(databyte, thresholdLen, indexStartParams + rangeLen + 44);
        //        StrToBytes(databyte, ThresholdMsg(devData), indexStartParams + rangeLen + 48, thresholdLen);
        //    }
        //    lock (obj)
        //    {
        //        SaveBinary(databyte, dataPath);
        //    }
        //}

        //private static void DateTimeIsNull(EquipStateData devStatusData)
        //{
        //    if ("0".Equals(devStatusData.TimerBeginTime) || "-62135625600".Equals(devStatusData.TimerBeginTime) || "".Equals(devStatusData.TimerBeginTime))
        //    {
        //        timerBegin = DateTime.Now;
        //    }
        //    else
        //    {
        //        timerBegin = TimeStamp.ConvertStringToDateTime(devStatusData.TimerBeginTime);
        //    }
        //    if ("0".Equals(devStatusData.TimerEndTime) || "-62135625600".Equals(devStatusData.TimerEndTime) || "".Equals(devStatusData.TimerEndTime))
        //    {
        //        timerEnd = DateTime.Now;
        //    }
        //    else
        //    {
        //        timerEnd = TimeStamp.ConvertStringToDateTime(devStatusData.TimerEndTime);
        //    }
        //    if ("0".Equals(devStatusData.TriggerStartTime) || "-62135625600".Equals(devStatusData.TriggerStartTime) || "".Equals(devStatusData.TriggerStartTime))
        //    {
        //        triggerBegin = DateTime.Now;
        //    }
        //    else
        //    {
        //        triggerBegin = TimeStamp.ConvertStringToDateTime(devStatusData.TriggerStartTime);
        //    }
        //    if ("0".Equals(devStatusData.TriggerEndTime) || "-62135625600".Equals(devStatusData.TriggerEndTime) || "".Equals(devStatusData.TriggerEndTime))
        //    {
        //        triggerEnd = DateTime.Now;
        //    }
        //    else
        //    {
        //        triggerEnd = TimeStamp.ConvertStringToDateTime(devStatusData.TriggerEndTime);
        //    }
        //}

        //private static void FloatToBytes(byte[] floatData, float fVal, int startIndex)
        //{
        //    int i = 0;
        //    byte[] tempV = BitConverter.GetBytes(fVal);

        //    for (i = 0; i < 4; i++)
        //    {
        //        floatData[startIndex + i] = tempV[i];
        //    }
        //}

        //private static void IntToBytes(byte[] abNeedInt, int iVal, int indexStart)
        //{
        //    int i = 0;
        //    byte[] abITmp = BitConverter.GetBytes(iVal);

        //    for (i = 0; i < 4; i++)
        //    {
        //        abNeedInt[indexStart + i] = abITmp[i];
        //    }
        //}

        //private static void DoubleToBytes(byte[] doubleData, double tval, int indexStart)
        //{
        //    int i = 0;
        //    byte[] temp = BitConverter.GetBytes(tval);

        //    for (i = 0; i < 8; i++)
        //    {
        //        doubleData[indexStart + i] = temp[i];
        //    }
        //}

        //private static void StrToBytes(byte[] abNeedInt, string strVal, int indexStart, int iLength)
        //{
        //    int i = 0;
        //    byte[] abStrTmp = Encoding.Default.GetBytes(strVal);

        //    for (i = 0; i < iLength; i++)
        //    {
        //        if (abStrTmp.Length == i)
        //        {
        //            break;
        //        }
        //        abNeedInt[indexStart + i] = abStrTmp[i];
        //    }
        //}

        //private static int ThresholdLen(EquipStateData data)
        //{
        //    int xLen = data.ThresholdX1.Length + data.ThresholdX2.Length + data.ThresholdX3.Length + data.ThresholdX4.Length + data.ThresholdX5.Length + 
        //        data.ThresholdX6.Length + data.ThresholdX7.Length + data.ThresholdX8.Length + data.ThresholdX9.Length + data.ThresholdX10.Length;
        //    int yLen = data.ThresholdY1.Length + data.ThresholdY2.Length + data.ThresholdY3.Length + data.ThresholdY4.Length + data.ThresholdY5.Length +
        //        data.ThresholdY6.Length + data.ThresholdY7.Length + data.ThresholdY8.Length + data.ThresholdY9.Length + data.ThresholdY10.Length;
        //    int zLen = data.ThresholdZ1.Length + data.ThresholdZ2.Length + data.ThresholdZ3.Length + data.ThresholdZ4.Length + data.ThresholdZ5.Length +
        //        data.ThresholdZ6.Length + data.ThresholdZ7.Length + data.ThresholdZ8.Length + data.ThresholdZ9.Length + data.ThresholdZ10.Length;
        //    return xLen + yLen + zLen + 30;
        //}

        //private static string ThresholdMsg(EquipStateData data)
        //{
        //    string xMsg = data.ThresholdX1 + DevComand.SplitChar + data.ThresholdX2 + DevComand.SplitChar + data.ThresholdX3 + DevComand.SplitChar +
        //        data.ThresholdX4 + DevComand.SplitChar + data.ThresholdX5 + DevComand.SplitChar + data.ThresholdX6 + DevComand.SplitChar+ 
        //        data.ThresholdX7 + DevComand.SplitChar+ data.ThresholdX8 + DevComand.SplitChar+ data.ThresholdX9 + DevComand.SplitChar+ data.ThresholdX10 + DevComand.SplitChar;

        //    string yMsg = data.ThresholdY1 + DevComand.SplitChar + data.ThresholdY2 + DevComand.SplitChar + data.ThresholdY3 + DevComand.SplitChar +
        //        data.ThresholdY4 + DevComand.SplitChar + data.ThresholdY5 + DevComand.SplitChar + data.ThresholdY6 + DevComand.SplitChar +
        //        data.ThresholdY7 + DevComand.SplitChar + data.ThresholdY8 + DevComand.SplitChar + data.ThresholdY9 + DevComand.SplitChar + data.ThresholdY10 + DevComand.SplitChar;

        //    string zMsg = data.ThresholdZ1 + DevComand.SplitChar + data.ThresholdZ2 + DevComand.SplitChar + data.ThresholdZ3 + DevComand.SplitChar +
        //        data.ThresholdZ4 + DevComand.SplitChar + data.ThresholdZ5 + DevComand.SplitChar + data.ThresholdZ6 + DevComand.SplitChar +
        //        data.ThresholdZ7 + DevComand.SplitChar + data.ThresholdZ8 + DevComand.SplitChar + data.ThresholdZ9 + DevComand.SplitChar + data.ThresholdZ10 + DevComand.SplitChar;
        //    return xMsg + yMsg + zMsg;
        //}
        public static void SaveBinary(byte[] bytarrDatToSav, string strFileCompleteName)
        {

            FileStream flstrmToSave = null;
            BinaryWriter bnrywrtrToSave = null;
            try
            {
                flstrmToSave = new FileStream(strFileCompleteName, File.Exists(strFileCompleteName) ? FileMode.Append : FileMode.Create);
                bnrywrtrToSave = new BinaryWriter(flstrmToSave);

                bnrywrtrToSave.Write(bytarrDatToSav);
                bnrywrtrToSave.Flush();
                bnrywrtrToSave.Close();
                flstrmToSave.Close();
            }
            catch (Exception ex)
            {
                LogHelper.Log.Debug("Failed to write data. " + ex.Message);
                if (bnrywrtrToSave != null)
                {
                    bnrywrtrToSave.Flush();
                    bnrywrtrToSave.Close();
                }
                if (flstrmToSave != null)
                {
                    bnrywrtrToSave.Close();
                }
            }
        }
    }
}

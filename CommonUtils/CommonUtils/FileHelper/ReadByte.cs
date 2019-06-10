using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ComLibrary;

namespace EquipmentClient.IO
{
    
    enum CmdType
    {
        SIG_DATA = 14,
        REBACK_DATA = 18
    }

    public class ReadByte
    {
        //private static int iSigTimestOffset = 12;
        //private static int iSigCharactValOffset = iSigTimestOffset +8;//20
        //private static int iSigChlCountOffset = iSigCharactValOffset + 120;// 140;
        //private static int iSigDataLengthOffset = iSigChlCountOffset + 1;//141;
        //private static int iSigDataOffset = iSigDataLengthOffset + 4;//145;
        //private static int indexbfData;//数据起始索引
        //private static int indexEndData;//一秒数据结束
        ///// <summary>
        ///// 解析云端保存的数据格式和回收保存的数据格式:时间+三十个特征值+三个通道数据
        ///// +采样频率+量程+采集模式+定时采集开始+结束时间+触发开始时间+触发结束时间（时间戳）+阈值长度+XYZ阈值三十个（字符串）
        ///// </summary>
        ///// <param name="buffer"></param>
        ///// <returns></returns>
        //public static DataTableM.AluData ReadServerDataBytes(byte[] buffer,DataTableM.AluData dt ,EquipStateData devData)
        //{
        //    //4+4+4+int +int + float*12 + channel byte+block(int)+float * channel*block
        //    try
        //    {
        //        if (buffer.Length < iSigDataOffset)
        //            return null;
        //        CmdType type = (CmdType)BitConverter.ToInt32(buffer, 4);
        //        if (type == CmdType.REBACK_DATA)
        //        {
        //            //回收数据
        //            iSigTimestOffset = 12;
        //            iSigCharactValOffset = iSigTimestOffset + 8;//20
        //            iSigChlCountOffset = iSigCharactValOffset + 120;// 140;
        //            iSigDataLengthOffset = iSigChlCountOffset + 1;//141;
        //            iSigDataOffset = iSigDataLengthOffset + 4;//145;
        //            AnalysisServiceData(buffer, dt, devData);
        //        }
        //        else if (type == CmdType.SIG_DATA)
        //        {
        //            //实时信号
        //            iSigTimestOffset = 12;
        //            iSigCharactValOffset = iSigTimestOffset + 8;//20
        //            iSigChlCountOffset = iSigCharactValOffset + 120;// 140;
        //            iSigDataLengthOffset = iSigChlCountOffset + 1;//141;
        //            iSigDataOffset = iSigDataLengthOffset + 4;//145;
        //            AnalysisServiceData(buffer, dt, devData);
        //        }
        //        else
        //        {
        //            iSigTimestOffset = 0;
        //            iSigCharactValOffset = iSigTimestOffset + 8;//20
        //            iSigChlCountOffset = iSigCharactValOffset + 120;// 140;
        //            iSigDataLengthOffset = iSigChlCountOffset + 1;//141;
        //            iSigDataOffset = iSigDataLengthOffset + 4;//145;
        //            AnalysisServiceData(buffer, dt, devData);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Error(ex.Message+"\r\n"+ex.StackTrace);
        //    }
        //    return dt;
        //}

        //private static bool AnalysisServiceData(byte[] buffer,DataTableM.AluData dt,EquipStateData devData)
        //{
        //    while(true)
        //    {

        //        if (iSigTimestOffset < buffer.Length)
        //        {
        //            DataRow dr1 = dt.dtOrignalMonitor.NewRow();//12307  101 146 223 68
        //            //string head = BitConverter.ToString(buffer, 0, 12);//55-AA-AA-55-0E-00-00-00-65-2F-00-00,云端数据包含头
        //            dr1[DataTableColumnName.SIG_TIME] = BitConverter.ToInt64(buffer, iSigTimestOffset);
        //            dr1[DataTableColumnName.SIG_X_CHARACTOR] += BitConverter.ToSingle(buffer, iSigCharactValOffset) + DevComand.SplitChar;
        //            dr1[DataTableColumnName.SIG_X_CHARACTOR] += BitConverter.ToSingle(buffer, iSigCharactValOffset + 4) + DevComand.SplitChar;
        //            dr1[DataTableColumnName.SIG_X_CHARACTOR] += BitConverter.ToSingle(buffer, iSigCharactValOffset + 8) + DevComand.SplitChar;
        //            dr1[DataTableColumnName.SIG_X_CHARACTOR] += BitConverter.ToSingle(buffer, iSigCharactValOffset + 12) + DevComand.SplitChar;
        //            dr1[DataTableColumnName.SIG_X_CHARACTOR] += BitConverter.ToSingle(buffer, iSigCharactValOffset + 16) + DevComand.SplitChar;
        //            dr1[DataTableColumnName.SIG_X_CHARACTOR] += BitConverter.ToSingle(buffer, iSigCharactValOffset + 20) + DevComand.SplitChar;
        //            dr1[DataTableColumnName.SIG_X_CHARACTOR] += BitConverter.ToSingle(buffer, iSigCharactValOffset + 24) + DevComand.SplitChar;
        //            dr1[DataTableColumnName.SIG_X_CHARACTOR] += BitConverter.ToSingle(buffer, iSigCharactValOffset + 28) + DevComand.SplitChar;
        //            dr1[DataTableColumnName.SIG_X_CHARACTOR] += BitConverter.ToSingle(buffer, iSigCharactValOffset + 32) + DevComand.SplitChar;
        //            dr1[DataTableColumnName.SIG_X_CHARACTOR] += BitConverter.ToSingle(buffer, iSigCharactValOffset + 36) + DevComand.SplitChar;

        //            dr1[DataTableColumnName.SIG_Y_CHARACTOR] += BitConverter.ToSingle(buffer, iSigCharactValOffset + 40) + DevComand.SplitChar;
        //            dr1[DataTableColumnName.SIG_Y_CHARACTOR] += BitConverter.ToSingle(buffer, iSigCharactValOffset + 44) + DevComand.SplitChar;
        //            dr1[DataTableColumnName.SIG_Y_CHARACTOR] += BitConverter.ToSingle(buffer, iSigCharactValOffset + 48) + DevComand.SplitChar;
        //            dr1[DataTableColumnName.SIG_Y_CHARACTOR] += BitConverter.ToSingle(buffer, iSigCharactValOffset + 52) + DevComand.SplitChar;
        //            dr1[DataTableColumnName.SIG_Y_CHARACTOR] += BitConverter.ToSingle(buffer, iSigCharactValOffset + 56) + DevComand.SplitChar;
        //            dr1[DataTableColumnName.SIG_Y_CHARACTOR] += BitConverter.ToSingle(buffer, iSigCharactValOffset + 60) + DevComand.SplitChar;
        //            dr1[DataTableColumnName.SIG_Y_CHARACTOR] += BitConverter.ToSingle(buffer, iSigCharactValOffset + 64) + DevComand.SplitChar;
        //            dr1[DataTableColumnName.SIG_Y_CHARACTOR] += BitConverter.ToSingle(buffer, iSigCharactValOffset + 68) + DevComand.SplitChar;
        //            dr1[DataTableColumnName.SIG_Y_CHARACTOR] += BitConverter.ToSingle(buffer, iSigCharactValOffset + 72) + DevComand.SplitChar;
        //            dr1[DataTableColumnName.SIG_Y_CHARACTOR] += BitConverter.ToSingle(buffer, iSigCharactValOffset + 76) + DevComand.SplitChar;

        //            dr1[DataTableColumnName.SIG_Z_CHARACTOR] += BitConverter.ToSingle(buffer, iSigCharactValOffset + 80) + DevComand.SplitChar;
        //            dr1[DataTableColumnName.SIG_Z_CHARACTOR] += BitConverter.ToSingle(buffer, iSigCharactValOffset + 84) + DevComand.SplitChar;
        //            dr1[DataTableColumnName.SIG_Z_CHARACTOR] += BitConverter.ToSingle(buffer, iSigCharactValOffset + 88) + DevComand.SplitChar;
        //            dr1[DataTableColumnName.SIG_Z_CHARACTOR] += BitConverter.ToSingle(buffer, iSigCharactValOffset + 92) + DevComand.SplitChar;
        //            dr1[DataTableColumnName.SIG_Z_CHARACTOR] += BitConverter.ToSingle(buffer, iSigCharactValOffset + 96) + DevComand.SplitChar;
        //            dr1[DataTableColumnName.SIG_Z_CHARACTOR] += BitConverter.ToSingle(buffer, iSigCharactValOffset + 100) + DevComand.SplitChar;
        //            dr1[DataTableColumnName.SIG_Z_CHARACTOR] += BitConverter.ToSingle(buffer, iSigCharactValOffset + 104) + DevComand.SplitChar;
        //            dr1[DataTableColumnName.SIG_Z_CHARACTOR] += BitConverter.ToSingle(buffer, iSigCharactValOffset + 108) + DevComand.SplitChar;
        //            dr1[DataTableColumnName.SIG_Z_CHARACTOR] += BitConverter.ToSingle(buffer, iSigCharactValOffset + 112) + DevComand.SplitChar;
        //            dr1[DataTableColumnName.SIG_Z_CHARACTOR] += BitConverter.ToSingle(buffer, iSigCharactValOffset + 116) + DevComand.SplitChar;
        //            byte[] channel = BitConverter.GetBytes(BitConverter.ToChar(buffer, iSigChlCountOffset)); //116+20+4
        //            if (channel[0] != 3)
        //                return false;
        //            int iDataNum = BitConverter.ToInt32(buffer, iSigDataLengthOffset);
        //            int i;
        //            for (i = 0; i < iDataNum; i++)
        //            {
        //                if (dr1 == null)
        //                {
        //                    dr1 = dt.dtOrignalMonitor.NewRow();
        //                }
        //                dr1[DataTableColumnName.SIG_X] = BitConverter.ToSingle(buffer, iSigDataOffset + 4 * i);
        //                dr1[DataTableColumnName.SIG_Y] = BitConverter.ToSingle(buffer, iSigDataOffset + 4 * (i + iDataNum));
        //                dr1[DataTableColumnName.SIG_Z] = BitConverter.ToSingle(buffer, iSigDataOffset + 4 * (i + 2 * iDataNum));

        //                dt.dtOrignalMonitor.Rows.Add(dr1);
        //                dr1 = null;
        //            }
        //            indexbfData = 4 * (i + 2 * iDataNum) + iSigDataOffset;
        //            ///
        //            ///
        //            int freq = BitConverter.ToInt32(buffer, indexbfData);
        //            int rangeLen = BitConverter.ToInt32(buffer, indexbfData + 4);
        //            string rangestr = Encoding.Default.GetString(buffer,indexbfData+8,rangeLen);
        //            int model = BitConverter.ToInt32(buffer, indexbfData + rangeLen+ 8);
        //            double timerStartTime = BitConverter.ToDouble(buffer, indexbfData + rangeLen+12);
        //            double timerEndTime = BitConverter.ToDouble(buffer, indexbfData + rangeLen+ 20);
        //            double triggerStartTime = BitConverter.ToDouble(buffer, indexbfData + rangeLen+28);
        //            double triggerEndTime = BitConverter.ToDouble(buffer, indexbfData + rangeLen+36);
        //            int throsldLen = BitConverter.ToInt32(buffer, indexbfData + rangeLen+44);

        //            string throsld = Encoding.Default.GetString(buffer, indexbfData + 48, throsldLen);

        //            indexEndData = indexbfData + 48 + rangeLen+throsldLen;

        //            iSigTimestOffset += indexEndData;
        //            iSigCharactValOffset = iSigTimestOffset + 8;
        //            iSigChlCountOffset = iSigCharactValOffset + 120;
        //            iSigDataLengthOffset = iSigChlCountOffset + 1;
        //            iSigDataOffset = iSigDataLengthOffset + 4;

        //            ///将参数放入对象
        //            ///
        //            devData.SampleFreq = freq+"";
        //            string[] rangexyz = rangestr.Split(',');
        //            devData.SampleRangeX = rangexyz[0];
        //            devData.SampleRangeY = rangexyz[1];
        //            devData.SampleRangeZ = rangexyz[2];
        //            devData.SampleModel = model + "";
        //            devData.TimerBeginTime = timerStartTime + "";
        //            devData.TimerEndTime = timerEndTime + "";
        //            devData.TriggerStartTime = triggerStartTime + "";
        //            devData.TriggerEndTime = triggerEndTime + "";
        //        }
        //        else
        //        {
        //            break;
        //        }
        //    }

        //    return true;
        //}
    }
}
